using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitForElementsAttribute : WaitAttribute
    {
        public Func<Func<IEnumerable<IWebElement>>, bool> Condition;

        public WaitForElementsAttribute(Func<Func<IEnumerable<IWebElement>>, bool> Condition)
        {
            this.Condition = Condition;
        }

    }

}