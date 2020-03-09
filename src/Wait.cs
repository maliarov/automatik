using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automatik
{
    public static class WaitExtensions
    {
        public static Action<Func<IWebElement>, TimeSpan> WaitAllWithTimeout(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IWebElement>, bool>> conditions
        ) =>
            (resolve, timeout) =>
            {
                var wait = new WebDriverWait(webDriver, timeout);
                wait.Until((_) =>
                    conditions.All(condition => condition(resolve))
                );
            };

        public static Action<Func<IWebElement>, TimeSpan> WaitAllWithTimeout(
            this IWebDriver webDriver,
            params Func<Func<IWebElement>, bool>[] conditions
        ) => webDriver.WaitAllWithTimeout((IEnumerable<Func<Func<IWebElement>, bool>>)conditions);

        public static Action<Func<IWebElement>> WaitAll(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IWebElement>, bool>> conditions
        )
        {
            var wait = webDriver.WaitAllWithTimeout(conditions);
            return (resolve) => wait(resolve, webDriver.Manage().Timeouts().ImplicitWait);
        }

        public static Action<Func<IWebElement>> WaitAll(
            this IWebDriver webDriver,
            params Func<Func<IWebElement>, bool>[] conditions
        ) => webDriver.WaitAll((IEnumerable<Func<Func<IWebElement>, bool>>)conditions);


        public static Action<Func<IEnumerable<IWebElement>>, TimeSpan> WaitAllWithTimeout(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IEnumerable<IWebElement>>, bool>> groupConditions,
            IEnumerable<Func<Func<IWebElement>, bool>> individualConditions
        ) =>
            (resolve, timeout) =>
            {
                var wait = new WebDriverWait(webDriver, timeout);
                wait.Until((_) =>
                    groupConditions.All(condition => condition(resolve)) &&
                    resolve().All(webElement => individualConditions.All(condition => condition(() => webElement)))
                );
            };

        public static Func<IEnumerable<Func<Func<IWebElement>, bool>>, Action<Func<IEnumerable<IWebElement>>, TimeSpan>> WaitAllWithTimeout(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IEnumerable<IWebElement>>, bool>> groupConditions
        ) =>
            (IEnumerable<Func<Func<IWebElement>, bool>> individualConditions) =>
                (resolve, timeout) =>
                {
                    var wait = new WebDriverWait(webDriver, timeout);
                    wait.Until((_) =>
                        groupConditions.All(condition => condition(resolve)) &&
                        resolve().All(webElement => individualConditions.All(condition => condition(() => webElement)))
                    );
                };


        public static Action<Func<IEnumerable<IWebElement>>> WaitAll(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IEnumerable<IWebElement>>, bool>> groupConditions,
            IEnumerable<Func<Func<IWebElement>, bool>> individualConditions
        )
        {
            var wait = webDriver.WaitAllWithTimeout(groupConditions, individualConditions);
            return (resolve) => wait(resolve, webDriver.Manage().Timeouts().ImplicitWait);
        }

        public static Func<IEnumerable<Func<Func<IWebElement>, bool>>, Action<Func<IEnumerable<IWebElement>>>> WaitAll(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IEnumerable<IWebElement>>, bool>> groupConditions
        ) =>
            (IEnumerable<Func<Func<IWebElement>, bool>> individualConditions) =>
            {

                var wait = webDriver.WaitAllWithTimeout(groupConditions, individualConditions);
                return (resolve) => wait(resolve, webDriver.Manage().Timeouts().ImplicitWait);
            };


    }
}