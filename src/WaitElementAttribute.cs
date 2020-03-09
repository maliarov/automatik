using System;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitElementAttribute : WaitAttribute
    {
        public readonly Func<Func<IWebElement>, bool> Condition;

        public WaitElementAttribute(Func<Func<IWebElement>, bool> Condition)
        {
            this.Condition = Condition;
        }

    }
    
}