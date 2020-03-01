using System;
using OpenQA.Selenium;

namespace Automatik
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public abstract class WaitUntilAttribute : Attribute
    {
        public Func<IWebElement, bool> Condition;

        public WaitUntilAttribute(Func<IWebElement, bool> Condition)
        {
            this.Condition = Condition;
        }
    }

    public class WaitUntilDisplayedAttribute : WaitUntilAttribute
    {
        public WaitUntilDisplayedAttribute()
            : base((webElement) => webElement.Displayed)
        { }

    }

    public class WaitUntilSelectedAttribute : WaitUntilAttribute
    {
        public WaitUntilSelectedAttribute()
            : base((webElement) => webElement.Displayed)
        { }
    }

    public class WaitUntilEnabledAttribute : WaitUntilAttribute
    {
        public WaitUntilEnabledAttribute()
            : base((webElement) => webElement.Enabled)
        { }
    }

}