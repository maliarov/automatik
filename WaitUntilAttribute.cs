using System;
using OpenQA.Selenium;

namespace Automatik
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public abstract class WaitUntilAttribute : Attribute
    {
        public readonly Func<IWebElement, bool> Condition;

        public TimeSpan? Timeout { get; set; }

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

    public class WaitUntilAttributeAbsentAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeAbsentAttribute(string AttributeName)
            : base((webElement) => webElement.GetAttribute(AttributeName) == null)
        { 
        }
    }

    public class WaitUntilAttributePresentAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributePresentAttribute(string AttributeName)
            : base((webElement) => webElement.GetAttribute(AttributeName) != null)
        { 
        }
    }

    public class WaitUntilClassAbsentAttribute : WaitUntilAttribute
    {
        public WaitUntilClassAbsentAttribute(string ClassName)
            : base((webElement) => Array.IndexOf(webElement.GetAttribute("class").ToString().Split(" "), ClassName) == -1)
        { 
        }
    }

    public class WaitUntilClassPresentAttribute : WaitUntilAttribute
    {
        public WaitUntilClassPresentAttribute(string ClassName)
            : base((webElement) => Array.IndexOf(webElement.GetAttribute("class").ToString().Split(" "), ClassName) > -1)
        { 
        }
    }

}