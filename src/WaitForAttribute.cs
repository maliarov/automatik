using System;
using OpenQA.Selenium;

namespace Automatik
{
   public abstract class WaitForAttribute : WaitAttribute
    {
        public readonly Func<Func<IWebElement>, bool> Condition;

        public WaitForAttribute(Func<Func<IWebElement>, bool> Condition)
        {
            this.Condition = Condition;
        }
    }

    public class WaitForExistsAttribute : WaitForAttribute
    {
        public WaitForExistsAttribute() : base(For.Exists()) { }
    }

    public class WaitForNotExistsAttribute : WaitForAttribute
    {
        public WaitForNotExistsAttribute() : base(For.NotExists()) { }
    }

    public class WaitForEnabledAttribute : WaitForAttribute
    {
        public WaitForEnabledAttribute() : base(For.Enabled()) { }
    }

    public class WaitForDisplayedAttribute : WaitForAttribute
    {
        public WaitForDisplayedAttribute() : base(For.Displayed()) { }
    }

    public class WaitForSelectedAttribute : WaitForAttribute
    {
        public WaitForSelectedAttribute() : base(For.Selected()) { }
    }

    public class WaitForClickableAttribute : WaitForAttribute
    {
        public WaitForClickableAttribute() : base(For.Clickable()) { }
    }

    public class WaitForAttributeAbsentAttribute : WaitForAttribute
    {
        public WaitForAttributeAbsentAttribute(string AttributeName)
            : base(For.AttributeAbsent(AttributeName)) { }
    }

    public class WaitForAttributePresentAttribute : WaitForAttribute
    {
        public WaitForAttributePresentAttribute(string AttributeName)
            : base(For.AttributePresent(AttributeName)) { }
    }

    public class WaitForAttributeContainsAttribute : WaitForAttribute
    {
        public WaitForAttributeContainsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(For.AttributeContains(AttributeName, Value, ComparisonType)) { }
    }
    public class WaitForAttributeNotContainsAttribute : WaitForAttribute
    {
        public WaitForAttributeNotContainsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(For.AttributeNotContains(AttributeName, Value, ComparisonType)) { }
    }
    public class WaitForAttributeEqualsAttribute : WaitForAttribute
    {
        public WaitForAttributeEqualsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(For.AttributeEquals(AttributeName, Value, ComparisonType)) { }
    }
    public class WaitForAttributeNotEqualsAttribute : WaitForAttribute
    {
        public WaitForAttributeNotEqualsAttribute(string AttributeName, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(For.AttributeNotEquals(AttributeName, Value, ComparisonType)) { }
    }


    public class WaitForTextContainsAttribute : WaitForAttribute
    {
        public WaitForTextContainsAttribute(string Value, StringComparison comparisonType = StringComparison.Ordinal)
            : base(For.TextContains(Value, comparisonType)) { }
    }
    public class WaitForTextNotContainsAttribute : WaitForAttribute
    {
        public WaitForTextNotContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(For.TextNotContains(Value, ComparisonType)) { }
    }
    public class WaitForTextEqualsAttribute : WaitForAttribute
    {
        public WaitForTextEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(For.TextEquals(Value, ComparisonType)) { }
    }
    public class WaitForTextNotEqualsAttribute : WaitForAttribute
    {
        public WaitForTextNotEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(For.TextNotEquals(Value, ComparisonType)) { }
    }

    public class WaitForClassAbsentAttribute : WaitForAttribute
    {
        public WaitForClassAbsentAttribute(string ClassName)
            : base(For.ClassAbsentAttribute(ClassName)) { }
    }

    public class WaitForClassPresentAttribute : WaitForAttribute
    {
        public WaitForClassPresentAttribute(string ClassName)
            : base(For.ClassPresentAttribute(ClassName)) { }
    }
}