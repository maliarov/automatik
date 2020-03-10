using System;
using OpenQA.Selenium;

namespace Automatik
{

    internal static class Utils
    {
        public static bool safeResolve(Func<bool> fn, bool defaultResult)
        {
            try
            {
                return fn();
            }
            catch (NoSuchElementException)
            {
                return defaultResult;
            }
            catch (ElementNotInteractableException)
            {
                return defaultResult;
            }
            catch (StaleElementReferenceException)
            {
                return defaultResult;
            }
        }
    }

}