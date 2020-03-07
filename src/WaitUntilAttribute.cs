using System;
using OpenQA.Selenium;

namespace Automatik
{
   public abstract class WaitUntilAttribute : WaitAttribute
    {
        public readonly Func<Func<IWebElement>, bool> Condition;

        public WaitUntilAttribute(Func<Func<IWebElement>, bool> Condition)
        {
            this.Condition = Condition;
        }
    }

    public class WaitUntilExistsAttribute : WaitUntilAttribute
    {
        public WaitUntilExistsAttribute() : base(Wait.Until.Exists()) { }
    }

    public class WaitUntilNotExistsAttribute : WaitUntilAttribute
    {
        public WaitUntilNotExistsAttribute() : base(Wait.Until.NotExists()) { }
    }

    public class WaitUntilEnabledAttribute : WaitUntilAttribute
    {
        public WaitUntilEnabledAttribute() : base(Wait.Until.Enabled()) { }
    }

    public class WaitUntilDisplayedAttribute : WaitUntilAttribute
    {
        public WaitUntilDisplayedAttribute() : base(Wait.Until.Displayed()) { }
    }

    public class WaitUntilSelectedAttribute : WaitUntilAttribute
    {
        public WaitUntilSelectedAttribute() : base(Wait.Until.Selected()) { }
    }

    public class WaitUntilClickableAttribute : WaitUntilAttribute
    {
        public WaitUntilClickableAttribute() : base(Wait.Until.Clickable()) { }
    }

    public class WaitUntilAttributeAbsentAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeAbsentAttribute(string AttributeName)
            : base((webElement) => Wait.Until.AttributeAbsent(AttributeName)(webElement)) { }
    }

    public class WaitUntilAttributePresentAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributePresentAttribute(string AttributeName)
            : base((webElement) => Wait.Until.AttributePresent(AttributeName)(webElement)) { }
    }

    public class WaitUntilAttributeContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeContainsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.Until.AttributeContains(AttributeName, Value, ComparisonType)(webElement)) { }
    }
    public class WaitUntilAttributeNotContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeNotContainsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.Until.AttributeNotContains(AttributeName, Value, ComparisonType)(webElement)) { }
    }
    public class WaitUntilAttributeEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeEqualsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.Until.AttributeEquals(AttributeName, Value, ComparisonType)(webElement)) { }
    }
    public class WaitUntilAttributeNotEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeNotEqualsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.Until.AttributeNotEquals(AttributeName, Value, ComparisonType)(webElement)) { }
    }


    public class WaitUntilTextContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextContainsAttribute(string Value, StringComparison comparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.Until.TextContains(Value, comparisonType)(webElement)) { }
    }
    public class WaitUntilTextNotContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextNotContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.Until.TextNotContains(Value, ComparisonType)(webElement)) { }
    }
    public class WaitUntilTextEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.Until.TextEquals(Value, ComparisonType)(webElement)) { }
    }
    public class WaitUntilTextNotEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextNotEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.Until.TextNotEquals(Value, ComparisonType)(webElement)) { }
    }

    public class WaitUntilClassAbsentAttribute : WaitUntilAttribute
    {
        public WaitUntilClassAbsentAttribute(string ClassName)
            : base((webElement) => Wait.Until.ClassAbsentAttribute(ClassName)(webElement)) { }
    }

    public class WaitUntilClassPresentAttribute : WaitUntilAttribute
    {
        public WaitUntilClassPresentAttribute(string ClassName)
            : base((webElement) => Wait.Until.ClassPresentAttribute(ClassName)(webElement)) { }
    }
}