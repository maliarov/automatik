using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitForElementToAttribute : WaitForElementAttribute
    {
        public WaitForElementToAttribute(Func<Func<IWebElement>, bool> Condition)
            : base(Condition) { }
    }



    public class WaitForElementExistanceAttribute : WaitForElementToAttribute
    {
        public WaitForElementExistanceAttribute()
            : base(ElementTo.Exist()) { }
    }

    public class WaitForElementToBecomeSelectedAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeSelectedAttribute()
            : base(ElementTo.BecomeSelected()) { }
    }

    public class WaitForElementToBecomeNotSelectedAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeNotSelectedAttribute()
            : base(ElementTo.BecomeNotSelected()) { }
    }

    public class WaitForElementToBecomeDisplayedAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeDisplayedAttribute()
            : base(ElementTo.BecomeDisplayed()) { }
    }

    public class WaitForElementToBecomeHiddenAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeHiddenAttribute()
            : base(ElementTo.BecomeHidden()) { }
    }

    public class WaitForElementToBecomeEnabledAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeEnabledAttribute()
            : base(ElementTo.BecomeEnabled()) { }
    }

    public class WaitForElementToBecomeDisabledAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeDisabledAttribute()
            : base(ElementTo.BecomeDisabled()) { }
    }

    public class WaitForElementToBecomeClickableAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeClickableAttribute()
            : base(ElementTo.BecomeClickable()) { }
    }



    public class WaitForElementToHaveAttributeWithNameAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithNameAttribute(string Name)
            : base(ElementTo.HaveAttributeWithName(Name)) { }
    }

    public class WaitForElementToNotHaveAttributeWithNameAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveAttributeWithNameAttribute(string Name)
            : base(ElementTo.NotHaveAttributeWithName(Name)) { }
    }

    public class WaitForElementToHaveAttributeWithNameThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithNameThatContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithNameThatContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveAttributeWithNameThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveAttributeWithNameThatContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.NotHaveAttributeWithNameThatContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithNameThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithNameThatStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithNameThatStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveAttributeWithNameThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveAttributeWithNameThatStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.NotHaveAttributeWithNameThatStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithNameThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithNameThatEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithNameThatEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveAttributeWithNameThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveAttributeWithNameThatEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.NotHaveAttributeWithNameThatEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithNameThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithNameThatMatchesAttribute(Regex Matcher)
            : base(ElementTo.HaveAttributeWithNameThatMatches(Matcher)) { }
    }

    public class WaitForElementToNotHaveAttributeWithNameThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveAttributeWithNameThatMatchesAttribute(Regex Matcher)
            : base(ElementTo.NotHaveAttributeWithNameThatMatches(Matcher)) { }
    }



    public class WaitForElementToHaveAttributeWithValueThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatContainsAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithValueThatContains(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatNotContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatNotContainsAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithValueThatNotContains(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatStartsWithAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithValueThatStartsWith(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatNotStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatNotStartsWithAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithValueThatNotStartsWith(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatEndsWithAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithValueThatEndsWith(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatNotEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatNotEndsWithAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithValueThatNotEndsWith(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatEqualsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatEqualsAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithValueThatEquals(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatNotEqualsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatNotEqualsAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveAttributeWithValueThatNotEquals(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatMatchesAttribute(string Name, Regex Matcher)
            : base(ElementTo.HaveAttributeWithValueThatMatches(Name, Matcher)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatNotMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatNotMatchesAttribute(string Name, Regex Matcher)
            : base(ElementTo.HaveAttributeWithValueThatNotMatches(Name, Matcher)) { }
    }



    public class WaitForElementToHaveTextAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextAttribute()
            : base(ElementTo.HaveText()) { }
    }

    public class WaitForElementToNotHaveTextAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveTextAttribute()
            : base(ElementTo.NotHaveText()) { }
    }

    public class WaitForElementToHaveTextNotEmptyOrWhiteSpacesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextNotEmptyOrWhiteSpacesAttribute()
            : base(ElementTo.HaveTextNotEmptyOrWhiteSpaces()) { }
    }

    public class WaitForElementToHaveTextThatEqualsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveTextThatEquals(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveTextThatEqualsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveTextThatEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.NotHaveTextThatEquals(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveTextThatContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatNotContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatNotContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveTextThatNotContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveTextThatStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatNotStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatNotStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveTextThatNotStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveTextThatEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatNotEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatNotEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveTextThatNotEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatMatchesAttribute(string Value, Regex Matcher)
            : base(ElementTo.HaveTextThatMatches(Value, Matcher)) { }
    }

    public class WaitForElementToHaveTextThatNotMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatNotMatchesAttribute(string Value, Regex Matcher)
            : base(ElementTo.HaveTextThatNotMatches(Value, Matcher)) { }
    }



    public class WaitForElementToHaveClassAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveClassAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveClass(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveClassAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveClassAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.NotHaveClass(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveClassThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveClassThatContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveClassThatContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveClassThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveClassThatContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.NotHaveClassThatContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveClassThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveClassThatStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveClassThatStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveClassThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveClassThatStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.NotHaveClassThatStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveClassThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveClassThatEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.HaveClassThatEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveClassThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveClassThatEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(ElementTo.NotHaveClassThatEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveClassThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveClassThatMatchesAttribute(Regex Matcher)
            : base(ElementTo.HaveClassThatMatches(Matcher)) { }
    }

    public class WaitForElementToNotHaveClassThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveClassThatMatchesAttribute(Regex Matcher)
            : base(ElementTo.NotHaveClassThatMatches(Matcher)) { }
    }

}