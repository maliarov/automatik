using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Automatik
{
    public delegate void WaitForElementFunction(Func<IWebElement> resolve, TimeSpan? Timeout = null);

    public delegate void WaitForElementsFunction(Func<IEnumerable<IWebElement>> resolve, TimeSpan? Timeout = null);


    public enum TimeoutDimension
    {
        Minutes,
        Seconds,
        Miliseconds
    }
}