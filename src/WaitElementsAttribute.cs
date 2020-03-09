using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitElementsAttribute : WaitAttribute
    {
        public Func<Func<IEnumerable<IWebElement>>, bool> Condition;

        public WaitElementsAttribute(Func<Func<IEnumerable<IWebElement>>, bool> Condition)
        {
            this.Condition = Condition;
        }

    }

}