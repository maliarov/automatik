using System;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitForElementAttribute : WaitAttribute
    {
        public readonly Func<Func<IWebElement>, bool> Condition;

        public WaitForElementAttribute(Func<Func<IWebElement>, bool> Condition)
        {
            this.Condition = Condition;
        }

    }
    
}