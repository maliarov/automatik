using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automatik
{
    public static class WaitExtensions
    {

        public static Action<Func<IWebElement>, TimeSpan> WaitWithTimeout(
            this IWebDriver webDriver, 
            params Func<Func<IWebElement>, bool>[] conditions
        ) =>
            (Func<IWebElement> resolve, TimeSpan timeout) => {
                var wait = new WebDriverWait(webDriver, timeout);
                wait.Until((_) =>
                    conditions.All(condition => condition(resolve))
                );
            };

        public static Action<Func<IWebElement>> Wait(
            this IWebDriver webDriver, 
            params Func<Func<IWebElement>, bool>[] conditions
        ) {
            var wait = webDriver.WaitWithTimeout(conditions);
            return (Func<IWebElement> resolve) => 
                wait(resolve, webDriver.Manage().Timeouts().ImplicitWait);
        }


        // public static Func<Func<IEnumerable<IWebElement>>, IEnumerable<IWebElement>> For(
        //     this IWebDriver webDriver, 
        //     params Func<Func<IEnumerable<IWebElement>>, bool>[] conditions
        // ) =>
        //     (Func<Func<IEnumerable<IWebElement>>, IEnumerable<IWebElement>>)((Func<IEnumerable<IWebElement>> resolve) => {
        //         var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
                
        //         return wait.Until((d) => {
        //             var isValid = conditions.All(condition => condition(resolve));
        //             return isValid ? null : resolve();
        //         });
        //     });


    }
}