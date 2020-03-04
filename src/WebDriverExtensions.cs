using System;
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

                            if (queryParamValue is string && ((string)queryParamValue == "" || (string)queryParamValue == string.Empty))
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
                    throw new Exception($"Navigation url not found. Provide [{typeof(PageAttribute).FullName}] attribute for [{pageType.Name}] class or use Navigation<...>(string url) method.");

                var url = attr.Url;

                if (url == null) {
                    if (attr.UrlProvider == null)
                        throw new Exception($"Provider [{nameof(PageAttribute.Url)}] or [{nameof(PageAttribute.UrlProvider)}] propertires for [{typeof(PageAttribute).FullName}] attribute.");

                    if (!typeof(IUrlProvider).IsAssignableFrom(attr.UrlProvider))
                        throw new Exception($"[{nameof(PageAttribute.UrlProvider)}] type in [{typeof(PageAttribute).FullName}] attribute must implements [{typeof(IUrlProvider).FullName}] interface.");

                    url = ((IUrlProvider)Activator.CreateInstance(attr.UrlProvider)).GetUrl(typeof(TPage), webDriver);
                }

                if (url == null)
                    throw new Exception($"Neither [{nameof(PageAttribute.Url)}] nor [{nameof(PageAttribute.UrlProvider)}] provided not null string.");

                return ((attr.ParentPage == null) ? "" : AssembleUrl(attr.ParentPage)) + url;
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
            FindByAttribute findByAttribute,
            IEnumerable<WaitUntilAttribute> waitUntilAttributes
        )
        {
            if (waitUntilAttributes == null || !waitUntilAttributes.Any())
                return findElementByContext();

            var timeout = waitUntilAttributes.Max(waitUntil => waitUntil.Timeout ?? webDriver.Manage().Timeouts().ImplicitWait);

            var wait = new WebDriverWait(webDriver, timeout);

            wait.IgnoreExceptionTypes(
                typeof(ElementNotInteractableException),
                typeof(NoSuchElementException)
            );

            return wait.Until(webDriver =>
            {
                var localWebElement = findElementByContext();
                var isValid = waitUntilAttributes.All(waitUntil => waitUntil.Condition(localWebElement));

                return isValid ? localWebElement : null;
            });

            IWebElement findElementByContext()
            {
                switch (findByAttribute.ContextType)
                {
                    case FindByContextType.Relative:
                        return webElement.FindElement(findByAttribute.By);
                    case FindByContextType.Absolute:
                        return webDriver.FindElement(findByAttribute.By);
                    default:
                        throw new Exception("Unknown find by context type.");
                }
            }
        }
        private static IEnumerable<IWebElement> FindElements(
            IWebDriver webDriver,
            IWebElement webElement,
            FindByAttribute findByAttribute,
            IEnumerable<WaitUntilAttribute> waitUntilAttributes
        )
        {
            if (waitUntilAttributes == null || !waitUntilAttributes.Any())
                return findElementsByContext();

            var timeout = waitUntilAttributes.Max(waitUntil => waitUntil.Timeout ?? webDriver.Manage().Timeouts().ImplicitWait);

            var wait = new WebDriverWait(webDriver, timeout);

            wait.IgnoreExceptionTypes(
                typeof(ElementNotInteractableException),
                typeof(NoSuchElementException)
            );

            return wait.Until(webDriver =>
            {
                var localWebElements = findElementsByContext();
                var isValid = waitUntilAttributes.All(waitUntil => localWebElements.All(waitUntil.Condition));

                return isValid ? localWebElements : null;
            });


            IEnumerable<IWebElement> findElementsByContext()
            {
                switch (findByAttribute.ContextType)
                {
                    case FindByContextType.Relative:
                        return webElement.FindElements(findByAttribute.By);
                    case FindByContextType.Absolute:
                        return webDriver.FindElements(findByAttribute.By);
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

                if (member.MemberType == typeof(IWebElement))
                {
                    var webElement = DispatchProxy.Create<IWebElement, ResolverDecorator<IWebElement>>();
                    var decorator = (ResolverDecorator<IWebElement>)webElement;

                    decorator.Init(() => FindElement(webDriver, getParentWebElement(), member.FindBy.First(), member.WaitUntil));

                    member.SetValue(webElement);
                    continue;
                }

                if (member.MemberType == typeof(IEnumerable<IWebElement>))
                {
                    var webElement = DispatchProxy.Create<IEnumerable<IWebElement>, ResolverDecorator<IEnumerable<IWebElement>>>();
                    var decorator = (ResolverDecorator<IEnumerable<IWebElement>>)webElement;

                    decorator.Init(() => FindElements(webDriver, getParentWebElement(), member.FindBy.First(), member.WaitUntil));

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
                        var webElements = FindElements(webDriver, getParentWebElement(), member.FindBy.First(), member.WaitUntil);
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

                        decorator.Init(() => FindElement(webDriver, getParentWebElement(), member.FindBy.First(), member.WaitUntil));

                        constructorParams = new object[] { webElement };
                    }

                    var element = Activator.CreateInstance(member.MemberType, constructorParams);

                    Bind(
                        element,
                        webDriver,
                        () => FindElement(webDriver, getParentWebElement(), member.FindBy.First(), member.WaitUntil)
                    );

                    member.SetValue(element);
                    continue;
                }

                throw new Exception($"[{member.Name}] field can not be mapped because it is not: IWebElement, IEnumerable<IWebElement>. Class or IEnumerable<Class> with default constructor or IWebElement constructor param.");
            }
        }
    }


}