using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace Automatik
{
    public static class WebDriverExtensions
    {
        private static readonly Regex UrlPlaceHoldersMatcher = new Regex(@"\{(?<placeholder>.[^}]*)\}");
        public static TPage Navigate<TPage>(this IWebDriver webDriver, string url) where TPage : class
        {
            webDriver.Navigate().GoToUrl(url);

            return webDriver.Bind<TPage>();
        }

        public static TPage Navigate<TPage>(this IWebDriver webDriver, object options = null) where TPage : class
        {
            var url = AssembleUrl(typeof(TPage));

            if (UrlPlaceHoldersMatcher.IsMatch(url))
            {

                var urlParts = url.Split("?");

                var pathParts =
                    urlParts.First()
                        .Split("/")
                        .Select((param) =>
                        {
                            var match = UrlPlaceHoldersMatcher.Match(param);

                            if (!match.Success)
                                return param;

                            var placeholderName = match.Groups["placeholder"].Value;
                            if (string.IsNullOrWhiteSpace(placeholderName))
                                throw new Exception("Invalid url param placeholder.");

                            var placeholderValues =
                                options?
                                    .GetType()
                                    .GetProperties()
                                    .Where(fi => fi.Name.Equals(placeholderName, StringComparison.OrdinalIgnoreCase))
                                    .Select(fi => fi.GetValue(options));

                            if (placeholderValues == null || placeholderValues.Any() == false)
                                throw new Exception($"Url param [{placeholderName}] has no value in options.");

                            return placeholderValues.First().ToString();
                        });

                var queryParams =
                    string.Join("", urlParts.Skip(1))
                        .Split("&")
                        .Select(q => q.Trim())
                        .Where(q => !string.IsNullOrWhiteSpace(q))
                        .Select((queryParam) =>
                        {
                            var match = UrlPlaceHoldersMatcher.Match(queryParam);

                            if (!match.Success)
                                return queryParam;

                            var placeholderName = match.Groups["placeholder"].Value;
                            if (string.IsNullOrWhiteSpace(placeholderName))
                                throw new Exception("Invalid url query param placeholder.");

                            var placeholderValues =
                                options?
                                    .GetType()
                                    .GetProperties()
                                    .Where(fi => fi.Name.Equals(placeholderName, StringComparison.OrdinalIgnoreCase))
                                    .Select(fi => fi.GetValue(options));

                            if (placeholderValues == null || placeholderValues.Any() == false)
                                throw new Exception($"Url query param [{placeholderName}] has no value in options.");

                            var queryParamValue = placeholderValues.First();

                            if (queryParamValue == null)
                                return null;

                            if (queryParamValue == "" || queryParam == string.Empty)
                                return placeholderName;

                            return $"{placeholderName}={Uri.EscapeDataString(queryParamValue.ToString())}";
                        })
                        .Where(qp => qp != null);

                var path = string.Join("/", pathParts);
                var query = string.Join("&", queryParams);

                url = string.Join("?", path, query);
            }

            webDriver.Navigate().GoToUrl(url);

            return webDriver.Bind<TPage>();


            string AssembleUrl(Type pageType)
            {
                var attr = pageType.GetCustomAttribute<PageAttribute>();
                if (attr == null)
                    throw new Exception($"Navigation url not found. Provide [Page(Url)] attribute for [{pageType.Name}] class or use Navigation<...>(string url) method.");

                return ((attr.ParentPage == null) ? "" : AssembleUrl(attr.ParentPage)) + attr.Url;
            }
        }

        public static TPage Bind<TPage>(this IWebDriver webDriver) where TPage : class
        {
            var page = (TPage)Activator.CreateInstance(typeof(TPage));

            Bind(
                page,
                webDriver,
                () => webDriver.FindElement(By.TagName("html"))
            );

            return page;
        }

        public static TPage Bind<TPage>(this IWebDriver webDriver, TPage page) where TPage : class
        {
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
            FindByContextType contextType,
            IEnumerable<Func<IWebElement, bool>> conditions
        )
        {
            if (!conditions.Any())
                return findElementByContext();

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

            wait.IgnoreExceptionTypes(
                typeof(ElementNotInteractableException),
                typeof(NoSuchElementException)
            );

            return wait.Until(webDriver =>
            {
                var localWebElement = findElementByContext();
                var isValid = conditions.All(validator => validator(localWebElement));

                return isValid ? localWebElement : null;
            });

            IWebElement findElementByContext()
            {
                switch (contextType)
                {
                    case FindByContextType.Relative:
                        return webElement.FindElement(by);
                    case FindByContextType.Absolute:
                        return webDriver.FindElement(by);
                    default:
                        throw new Exception("Unknown find by context type.");
                }
            }
        }
        private static IEnumerable<IWebElement> FindElements(
            IWebDriver webDriver,
            IWebElement webElement,
            By by,
            FindByContextType contextType,
            IEnumerable<Func<IWebElement, bool>> conditions
        )
        {
            if (!conditions.Any())
                return findElementsByContext();

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

            wait.IgnoreExceptionTypes(
                typeof(ElementNotInteractableException),
                typeof(NoSuchElementException)
            );

            return wait.Until(webDriver =>
            {
                var localWebElements = findElementsByContext();
                var isValid = conditions.All(validator => localWebElements.All(validator));

                return isValid ? localWebElements : null;
            });


            IEnumerable<IWebElement> findElementsByContext()
            {
                switch (contextType)
                {
                    case FindByContextType.Relative:
                        return webElement.FindElements(by);
                    case FindByContextType.Absolute:
                        return webDriver.FindElements(by);
                    default:
                        throw new Exception("Unknown find by context type.");
                }
            }

        }

        private static void Bind(object obj, IWebDriver webDriver, Func<IWebElement> getParentWebElement)
        {
            foreach (var member in MemberInfo.FindOn(obj))
            {
                if (member.FindBy.Count() > 1)
                    throw new Exception($"Ambiguous amount ({member.FindBy.Count()}) of [FindBy...] attributes for [{member.Name}] field.");

                var currentFindByAttr = member.FindBy.First();
                var currentFindBy = currentFindByAttr.By;
                var currentFindByContext = currentFindByAttr.ContextType;
                var currentWaitConditions = member.WaitUntil.Select(a => a.Condition);

                if (member.MemberType == typeof(IWebElement))
                {
                    var webElement = DispatchProxy.Create<IWebElement, ResolverDecorator<IWebElement>>();
                    var decorator = (ResolverDecorator<IWebElement>)webElement;

                    decorator.Init(() => FindElement(webDriver, getParentWebElement(), currentFindBy, currentFindByContext, currentWaitConditions));

                    member.SetValue(webElement);
                    continue;
                }

                if (member.MemberType == typeof(IEnumerable<IWebElement>))
                {
                    var webElement = DispatchProxy.Create<IEnumerable<IWebElement>, ResolverDecorator<IEnumerable<IWebElement>>>();
                    var decorator = (ResolverDecorator<IEnumerable<IWebElement>>)webElement;

                    decorator.Init(() => FindElements(webDriver, getParentWebElement(), currentFindBy, currentFindByContext, currentWaitConditions));

                    member.SetValue(webElement);
                    continue;
                }

                if (
                    member.MemberType.IsGenericType &&
                    member.MemberType.GetGenericTypeDefinition() == typeof(IEnumerable<>) &&
                    member.MemberType.GenericTypeArguments[0].IsClass == true &&
                    (
                       member.MemberType.GenericTypeArguments[0].GetConstructor(Type.EmptyTypes) != null ||
                       member.MemberType.GenericTypeArguments[0].GetConstructor(new[] { typeof(IWebElement) }) != null
                    )
                )
                {
                    var resolverDecoratorType = typeof(ResolverDecorator<>).MakeGenericType(member.MemberType);
                    var Create = typeof(DispatchProxy).GetMethod("Create").MakeGenericMethod(member.MemberType, resolverDecoratorType);
                    var Init = resolverDecoratorType.GetMethod("Init");

                    Func<object> resolver = () =>
                    {
                        var collection = Activator.CreateInstance(typeof(List<>).MakeGenericType(member.MemberType.GenericTypeArguments[0]));
                        var webElements = FindElements(webDriver, getParentWebElement(), currentFindBy, currentFindByContext, currentWaitConditions);
                        var array = Array.CreateInstance(member.MemberType.GenericTypeArguments[0], webElements.Count());
                        var index = 0;

                        foreach (var webElement in webElements)
                        {
                            object[] constructorParams = null;

                            if (member.MemberType.GenericTypeArguments[0].GetConstructor(new[] { typeof(IWebElement) }) != null)
                                constructorParams = new object[] { webElement };

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

                        decorator.Init(() => FindElement(webDriver, getParentWebElement(), currentFindBy, currentFindByContext, currentWaitConditions));

                        constructorParams = new object[] { webElement };
                    }

                    var element = Activator.CreateInstance(member.MemberType, constructorParams);

                    Bind(
                        element,
                        webDriver,
                        () => FindElement(webDriver, getParentWebElement(), currentFindBy, currentFindByContext, currentWaitConditions)
                    );

                    member.SetValue(element);
                    continue;
                }

                throw new Exception($"[{member.Name}] field can not be mapped because it is not: IWebElement, IEnumerable<IWebElement>. Class or IEnumerable<Class> with default constructor or IWebElement constructor param.");
            }
        }
    }


}