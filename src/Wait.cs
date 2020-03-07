using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automatik
{
    public static partial class Wait
    {

        public static Func<Func<IWebElement>, IWebElement> WithConditions(this IWebDriver webDriver, params Func<Func<IWebElement>, bool>[] conditions) =>
            (Func<Func<IWebElement>, IWebElement>)((Func<IWebElement> resolve) => {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
                
                return wait.Until((d) => {
                    var isValid = conditions.All(condition => condition(resolve));
                    return isValid ? null : resolve();
                });
            });

    }
}