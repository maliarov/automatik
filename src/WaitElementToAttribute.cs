using System;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitElementToAttribute : WaitElementAttribute
    {
        public WaitElementToAttribute(Func<Func<IWebElement>, bool> Condition) 
            : base(Condition) { }
    }

    public class WaitElementToExistAttribute : WaitElementToAttribute
    {
        public WaitElementToExistAttribute() : base(To.Exist()) { }
    }

    public class WaitElementToBeEnabledAttribute : WaitElementToAttribute
    {
        public WaitElementToBeEnabledAttribute() : base(To.BeEnabled()) { }
    }

    public class WaitElementToBeDisplayedAttribute : WaitElementToAttribute
    {
        public WaitElementToBeDisplayedAttribute() : base(To.BeDisplayed()) { }
    }

    public class WaitElementToBeSelectedAttribute : WaitElementToAttribute
    {
        public WaitElementToBeSelectedAttribute() : base(To.BeSelected()) { }
    }

    public class WaitElementToBeClickableAttribute : WaitElementToAttribute
    {
        public WaitElementToBeClickableAttribute() : base(To.BeClickable()) { }
    }

    public class WaitElementToNotHaveAttributeAttribute : WaitElementToAttribute
    {
        public WaitElementToNotHaveAttributeAttribute(string AttributeName)
            : base(To.NotHaveAttribute(AttributeName)) { }
    }

    public class WaitElementToHaveAttributeAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveAttributeAttribute(string AttributeName)
            : base(To.HaveAttribute(AttributeName)) { }
    }

    public class WaitElementToHaveAttributeThatContainsAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveAttributeThatContainsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HabeAttributeThatContains(AttributeName, Value, ComparisonType)) { }
    }
    public class WaitElementToHaveAttributeThatNotContainsAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveAttributeThatNotContainsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeThatNotContains(AttributeName, Value, ComparisonType)) { }
    }
    public class WaitElementToHaveAttributeThatEqualsAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveAttributeThatEqualsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeThatEquals(AttributeName, Value, ComparisonType)) { }
    }
    public class WaitElementToHaveAttributeThatNotEqualsAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveAttributeThatNotEqualsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeThatNotEquals(AttributeName, Value, ComparisonType)) { }
    }


    public class WaitElementToHaveTextThatContainsAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveTextThatContainsAttribute(string Value, StringComparison comparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatContains(Value, comparisonType)) { }
    }
    public class WaitElementToHaveTextThatNotContainsAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveTextThatNotContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatNotContains(Value, ComparisonType)) { }
    }
    public class WaitElementToHaveTextThatEqualsAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveTextThatEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatEquals(Value, ComparisonType)) { }
    }
    public class WaitElementToHaveTextThatNotEqualsAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveTextThatNotEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatNotEquals(Value, ComparisonType)) { }
    }

    public class WaitElementToHaveNoClassAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveNoClassAttribute(string ClassName)
            : base(To.NotHaveClassAttribute(ClassName)) { }
    }

    public class WaitElementToHaveClassAttribute : WaitElementToAttribute
    {
        public WaitElementToHaveClassAttribute(string ClassName)
            : base(To.HaveClassAttribute(ClassName)) { }
    }
}