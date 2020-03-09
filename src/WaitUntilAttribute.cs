using System;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitUntilAttribute : WaitElementAttribute
    {
        public WaitUntilAttribute(Func<Func<IWebElement>, bool> Condition) 
            : base(Condition) { }
    }

    public class WaitUntilExistsAttribute : WaitUntilAttribute
    {
        public WaitUntilExistsAttribute() : base(Until.Exists()) { }
    }

    public class WaitUntilNotExistsAttribute : WaitUntilAttribute
    {
        public WaitUntilNotExistsAttribute() : base(Until.NotExists()) { }
    }

    public class WaitUntilEnabledAttribute : WaitUntilAttribute
    {
        public WaitUntilEnabledAttribute() : base(Until.Enabled()) { }
    }

    public class WaitUntilDisplayedAttribute : WaitUntilAttribute
    {
        public WaitUntilDisplayedAttribute() : base(Until.Displayed()) { }
    }

    public class WaitUntilSelectedAttribute : WaitUntilAttribute
    {
        public WaitUntilSelectedAttribute() : base(Until.Selected()) { }
    }

    public class WaitUntilClickableAttribute : WaitUntilAttribute
    {
        public WaitUntilClickableAttribute() : base(Until.Clickable()) { }
    }

    public class WaitUntilAttributeAbsentAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeAbsentAttribute(string AttributeName)
            : base(Until.AttributeAbsent(AttributeName)) { }
    }

    public class WaitUntilAttributePresentAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributePresentAttribute(string AttributeName)
            : base(Until.AttributePresent(AttributeName)) { }
    }

    public class WaitUntilAttributeContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeContainsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(Until.AttributeContains(AttributeName, Value, ComparisonType)) { }
    }
    public class WaitUntilAttributeNotContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeNotContainsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(Until.AttributeNotContains(AttributeName, Value, ComparisonType)) { }
    }
    public class WaitUntilAttributeEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeEqualsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(Until.AttributeEquals(AttributeName, Value, ComparisonType)) { }
    }
    public class WaitUntilAttributeNotEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeNotEqualsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(Until.AttributeNotEquals(AttributeName, Value, ComparisonType)) { }
    }


    public class WaitUntilTextContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextContainsAttribute(string Value, StringComparison comparisonType = StringComparison.Ordinal)
            : base(Until.TextContains(Value, comparisonType)) { }
    }
    public class WaitUntilTextNotContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextNotContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(Until.TextNotContains(Value, ComparisonType)) { }
    }
    public class WaitUntilTextEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(Until.TextEquals(Value, ComparisonType)) { }
    }
    public class WaitUntilTextNotEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextNotEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(Until.TextNotEquals(Value, ComparisonType)) { }
    }

    public class WaitUntilClassAbsentAttribute : WaitUntilAttribute
    {
        public WaitUntilClassAbsentAttribute(string ClassName)
            : base(Until.ClassAbsentAttribute(ClassName)) { }
    }

    public class WaitUntilClassPresentAttribute : WaitUntilAttribute
    {
        public WaitUntilClassPresentAttribute(string ClassName)
            : base(Until.ClassPresentAttribute(ClassName)) { }
    }
}