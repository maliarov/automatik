using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace Automatik
{
    public static class WebDriverExtensions
    {
        public static TPage Navigate<TPage>(this IWebDriver webDriver, string url) where TPage : class
        {
            webDriver.Navigate().GoToUrl(url);

            return webDriver.Bind<TPage>();
        }

        public static TPage Navigate<TPage>(this IWebDriver webDriver) where TPage : class
        {
            var attr = typeof(TPage).GetCustomAttribute<PageAttribute>();
            if (attr == null)
                throw new Exception("Navigation url not found. Provide [HttpPage(Url)] attribute for page class or use Navigation<...>(string url) method.");

            webDriver.Navigate().GoToUrl(attr.Uri.ToString());

            return webDriver.Bind<TPage>();
        }


        private static TPage Bind<TPage>(this IWebDriver webDriver) where TPage : class
        {
            var page = (TPage)Activator.CreateInstance(typeof(TPage));

            Bind(
                page,
                webDriver,
                () => webDriver.FindElement(By.TagName("html"))
            );

            return page;
        }



        private static IWebElement FindElement(
            IWebDriver webDriver, 
            IWebElement webElement, 
            By by, 
            IEnumerable<Func<IWebElement, bool>> conditions
        )
        {
            if (!conditions.Any())
                return webElement.FindElement(by);

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

            wait.IgnoreExceptionTypes(
                typeof(ElementNotInteractableException),
                typeof(NoSuchElementException)
            );

            return wait.Until(webDriver =>
            {
                var localWebElement = webElement.FindElement(by);
                var isValid = conditions.All(validator => validator(localWebElement));

                return isValid ? localWebElement : null;
            });
        }

        private static IEnumerable<IWebElement> FindElements(
            IWebDriver webDriver, 
            IWebElement webElement, 
            By by, 
            IEnumerable<Func<IWebElement, bool>> conditions
        )
        {
            if (!conditions.Any())
                return webElement.FindElements(by);

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

            wait.IgnoreExceptionTypes(
                typeof(ElementNotInteractableException),
                typeof(NoSuchElementException)
            );

            return wait.Until(webDriver =>
            {
                var localWebElements = webElement.FindElements(by);
                var isValid = conditions.All(validator => localWebElements.All(validator));

                return isValid ? localWebElements : null;
            });
        }


        private static void Bind(object obj, IWebDriver webDriver, Func<IWebElement> getParentWebElement)
        {
            if (obj == null)
                return;

            var objType = obj.GetType();

            var fields =
                objType
                    .GetFields()
                    .Where(f => f.IsDefined(typeof(FindByAttribute)))
                    .Select(f =>
                        new
                        {
                            Name = f.Name,
                            MemberType = f.FieldType,
                            FindBy = f.GetCustomAttributes<FindByAttribute>(),
                            WaitUntil = f.GetCustomAttributes<WaitUntilAttribute>(),
                            SetValue = (Action<object>)((object val) => f.SetValue(obj, val))
                        });

            var properties =
                objType
                    .GetProperties()
                    .Where(p => p.IsDefined(typeof(FindByAttribute)))
                    .Select(p =>
                        new
                        {
                            Name = p.Name,
                            MemberType = p.PropertyType,
                            FindBy = p.GetCustomAttributes<FindByAttribute>(),
                            WaitUntil = p.GetCustomAttributes<WaitUntilAttribute>(),
                            SetValue = (Action<object>)((object val) => p.SetValue(obj, val))
                        }
                    );

            var members = fields.Union(properties);

            foreach (var member in members)
            {
                if (member.FindBy.Count() > 1)
                    throw new Exception($"Ambiguous amount ({member.FindBy.Count()}) of [FindBy...] attributes for [{member.Name}] field.");

                var currentFindBy = member.FindBy.First().By;
                var currentWaitConditions = member.WaitUntil.Select(a => a.Condition);

                if (member.MemberType == typeof(IWebElement))
                {
                    var webElement = DispatchProxy.Create<IWebElement, ResolverDecorator<IWebElement>>();
                    var decorator = (ResolverDecorator<IWebElement>)webElement;

                    decorator.Init(() => FindElement(webDriver, getParentWebElement(), currentFindBy, currentWaitConditions));

                    member.SetValue(webElement);
                    continue;
                }

                if (member.MemberType == typeof(IEnumerable<IWebElement>))
                {
                    var webElement = DispatchProxy.Create<IEnumerable<IWebElement>, ResolverDecorator<IEnumerable<IWebElement>>>();
                    var decorator = (ResolverDecorator<IEnumerable<IWebElement>>)webElement;

                    decorator.Init(() => FindElements(webDriver, getParentWebElement(), currentFindBy, currentWaitConditions));

                    member.SetValue(webElement);
                    continue;
                }

                if (
                    member.MemberType.IsGenericType &&
                    member.MemberType.GetGenericTypeDefinition() == typeof(IEnumerable<>) &&
                    member.MemberType.GenericTypeArguments[0].IsClass == true && 
                    (
                       member.MemberType.GenericTypeArguments[0].GetConstructor(Type.EmptyTypes) != null ||
                       member.MemberType.GenericTypeArguments[0].GetConstructor(new [] { typeof(IWebElement) }) != null
                    )
                )
                {
                    var resolverDecoratorType = typeof(ResolverDecorator<>).MakeGenericType(member.MemberType);
                    var Create = typeof(DispatchProxy).GetMethod("Create").MakeGenericMethod(member.MemberType, resolverDecoratorType);
                    var Init = resolverDecoratorType.GetMethod("Init");

                    Func<object> resolver = () =>
                    {
                        var collection = Activator.CreateInstance(typeof(List<>).MakeGenericType(member.MemberType.GenericTypeArguments[0]));
                        var webElements = FindElements(webDriver, getParentWebElement(), currentFindBy, currentWaitConditions);
                        var array = Array.CreateInstance(member.MemberType.GenericTypeArguments[0], webElements.Count());
                        var index = 0;

                        foreach (var webElement in webElements)
                        {
                            object[] constructorParams = null;

                            if (member.MemberType.GenericTypeArguments[0].GetConstructor(new[] { typeof(IWebElement) }) != null)
                                constructorParams  = new object[] { webElement };

                            var element = Activator.CreateInstance(member.MemberType.GenericTypeArguments[0], constructorParams);

                            Bind(element, webDriver, () => webElement);

                            array.SetValue(element, index);

                            index += 1;
                        }

                        return array;
                    };

                    var decorator = Create.Invoke(null, null);

                    Init.Invoke(decorator, new[] { resolver });

                    member.SetValue(decorator);
                    continue;
                }

                if (
                    member.MemberType.IsClass &&
                    (
                        member.MemberType.GetConstructor(Type.EmptyTypes) != null ||
                        member.MemberType.GetConstructor(new[] { typeof(IWebElement) }) != null
                    )
                )
                {
                    object[] constructorParams = null;

                    if (member.MemberType.GetConstructor(new[] { typeof(IWebElement) }) != null)
                    {
                        var webElement = DispatchProxy.Create<IWebElement, ResolverDecorator<IWebElement>>();
                        var decorator = (ResolverDecorator<IWebElement>)webElement;

                        decorator.Init(() => FindElement(webDriver, getParentWebElement(), currentFindBy, currentWaitConditions));

                        constructorParams  = new object[] { webElement };
                    }

                    var element = Activator.CreateInstance(member.MemberType, constructorParams);

                    Bind(
                        element,
                        webDriver,
                        () => FindElement(webDriver, getParentWebElement(), currentFindBy, currentWaitConditions)
                    );

                    member.SetValue(element);
                    continue;
                }

                throw new Exception($"[{member.Name}] field can not be mapped because it is not: IWebElement, IEnumerable<IWebElement>. Class or IEnumerable<Class> with default constructor or IWebElement constructor param.");
            }
        }

    }


}