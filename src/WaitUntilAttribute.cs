using System;
using OpenQA.Selenium;

namespace Automatik
{

    public enum TimeoutDimension
    {
        Minutes,
        Seconds,
        Miliseconds
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public abstract class WaitUntilAttribute : Attribute
    {
        public readonly Func<IWebElement, bool> Condition;

        public uint TimeoutInterval { get; set; }
        public TimeoutDimension TimeoutDimension { get; set; } = TimeoutDimension.Seconds;

        public TimeSpan? GetTimeout()
        {
            if (TimeoutInterval == 0)
                return null;

            switch (TimeoutDimension)
            {
                case TimeoutDimension.Minutes:
                    return TimeSpan.FromMinutes(TimeoutInterval);
                case TimeoutDimension.Seconds:
                    return TimeSpan.FromSeconds(TimeoutInterval);
                case TimeoutDimension.Miliseconds:
                    return TimeSpan.FromMilliseconds(TimeoutInterval);

                default:
                    return null;
            }
        }

        public WaitUntilAttribute(Func<IWebElement, bool> Condition)
        {
            this.Condition = Condition;
        }
    }

    public class WaitUntilExistsAttribute : WaitUntilAttribute
    {
        public WaitUntilExistsAttribute() : base(Wait.UntilExists) { }
    }

    public class WaitUntilNotExistsAttribute : WaitUntilAttribute
    {
        public WaitUntilNotExistsAttribute() : base(Wait.UntilNotExists) { }
    }

    public class WaitUntilEnabledAttribute : WaitUntilAttribute
    {
        public WaitUntilEnabledAttribute() : base(Wait.UntilEnabled) { }
    }

    public class WaitUntilDisplayedAttribute : WaitUntilAttribute
    {
        public WaitUntilDisplayedAttribute() : base(Wait.UntilDisplayed) { }
    }

    public class WaitUntilSelectedAttribute : WaitUntilAttribute
    {
        public WaitUntilSelectedAttribute() : base(Wait.UntilSelected) { }
    }

    public class WaitUntilClickableAttribute : WaitUntilAttribute
    {
        public WaitUntilClickableAttribute() : base(Wait.UntilClickable) { }
    }

    public class WaitUntilAttributeAbsentAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeAbsentAttribute(string AttributeName)
            : base((webElement) => Wait.UntilAttributeAbsent(webElement, AttributeName)) { }
    }

    public class WaitUntilAttributePresentAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributePresentAttribute(string AttributeName)
            : base((webElement) => Wait.UntilAttributePresent(webElement, AttributeName)) { }
    }

    public class WaitUntilAttributeContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeContainsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.UntilAttributeContains(webElement, AttributeName, Value, ComparisonType)) { }
    }
    public class WaitUntilAttributeNotContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeNotContainsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.UntilAttributeNotContains(webElement, AttributeName, Value, ComparisonType)) { }
    }
    public class WaitUntilAttributeEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeEqualsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.UntilAttributeEquals(webElement, AttributeName, Value, ComparisonType)) { }
    }
    public class WaitUntilAttributeNotEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilAttributeNotEqualsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.UntilAttributeNotEquals(webElement, AttributeName, Value, ComparisonType)) { }
    }


    public class WaitUntilTextContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextContainsAttribute(string Value, StringComparison comparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.UntilTextContains(webElement, Value, comparisonType)) { }
    }
    public class WaitUntilTextNotContainsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextNotContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.UntilTextNotContains(webElement, Value, ComparisonType)) { }
    }
    public class WaitUntilTextEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.UntilTextEquals(webElement, Value, ComparisonType)) { }
    }
    public class WaitUntilTextNotEqualsAttribute : WaitUntilAttribute
    {
        public WaitUntilTextNotEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base((webElement) => Wait.UntilTextNotEquals(webElement, Value, ComparisonType)) { }
    }

    public class WaitUntilClassAbsentAttribute : WaitUntilAttribute
    {
        public WaitUntilClassAbsentAttribute(string ClassName)
            : base((webElement) => Wait.WaitUntilClassAbsentAttribute(webElement, ClassName)) { }
    }

    public class WaitUntilClassPresentAttribute : WaitUntilAttribute
    {
        public WaitUntilClassPresentAttribute(string ClassName)
            : base((webElement) => Wait.WaitUntilClassPresentAttribute(webElement, ClassName)) { }
    }
}