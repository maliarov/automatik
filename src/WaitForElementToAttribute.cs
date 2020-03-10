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
            : base(To.Exist()) { }
    }

    public class WaitForElementToBecomeSelectedAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeSelectedAttribute()
            : base(To.BecomeSelected()) { }
    }

    public class WaitForElementToBecomeNotSelectedAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeNotSelectedAttribute()
            : base(To.BecomeNotSelected()) { }
    }

    public class WaitForElementToBecomeDisplayedAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeDisplayedAttribute()
            : base(To.BecomeDisplayed()) { }
    }

    public class WaitForElementToBecomeHiddenAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeHiddenAttribute()
            : base(To.BecomeHidden()) { }
    }

    public class WaitForElementToBecomeEnabledAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeEnabledAttribute()
            : base(To.BecomeEnabled()) { }
    }

    public class WaitForElementToBecomeDisabledAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeDisabledAttribute()
            : base(To.BecomeDisabled()) { }
    }

    public class WaitForElementToBecomeClickableAttribute : WaitForElementToAttribute
    {
        public WaitForElementToBecomeClickableAttribute()
            : base(To.BecomeClickable()) { }
    }



    public class WaitForElementToHaveAttributeWithNameAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithNameAttribute(string Name)
            : base(To.HaveAttributeWithName(Name)) { }
    }

    public class WaitForElementToNotHaveAttributeWithNameAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveAttributeWithNameAttribute(string Name)
            : base(To.NotHaveAttributeWithName(Name)) { }
    }

    public class WaitForElementToHaveAttributeWithNameThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithNameThatContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithNameThatContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveAttributeWithNameThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveAttributeWithNameThatContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.NotHaveAttributeWithNameThatContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithNameThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithNameThatStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithNameThatStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveAttributeWithNameThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveAttributeWithNameThatStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.NotHaveAttributeWithNameThatStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithNameThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithNameThatEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithNameThatEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveAttributeWithNameThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveAttributeWithNameThatEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.NotHaveAttributeWithNameThatEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithNameThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithNameThatMatchesAttribute(Regex Matcher)
            : base(To.HaveAttributeWithNameThatMatches(Matcher)) { }
    }

    public class WaitForElementToNotHaveAttributeWithNameThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveAttributeWithNameThatMatchesAttribute(Regex Matcher)
            : base(To.NotHaveAttributeWithNameThatMatches(Matcher)) { }
    }



    public class WaitForElementToHaveAttributeWithValueThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatContainsAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithValueThatContains(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatNotContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatNotContainsAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithValueThatNotContains(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatStartsWithAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithValueThatStartsWith(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatNotStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatNotStartsWithAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithValueThatNotStartsWith(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatEndsWithAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithValueThatEndsWith(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatNotEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatNotEndsWithAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithValueThatNotEndsWith(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatEqualsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatEqualsAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithValueThatEquals(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatNotEqualsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatNotEqualsAttribute(string Name, string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveAttributeWithValueThatNotEquals(Name, Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatMatchesAttribute(string Name, Regex Matcher)
            : base(To.HaveAttributeWithValueThatMatches(Name, Matcher)) { }
    }

    public class WaitForElementToHaveAttributeWithValueThatNotMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveAttributeWithValueThatNotMatchesAttribute(string Name, Regex Matcher)
            : base(To.HaveAttributeWithValueThatNotMatches(Name, Matcher)) { }
    }



    public class WaitForElementToHaveTextAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextAttribute()
            : base(To.HaveText()) { }
    }

    public class WaitForElementToNotHaveTextAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveTextAttribute()
            : base(To.NotHaveText()) { }
    }

    public class WaitForElementToHaveTextNotEmptyOrWhiteSpacesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextNotEmptyOrWhiteSpacesAttribute()
            : base(To.HaveTextNotEmptyOrWhiteSpaces()) { }
    }

    public class WaitForElementToHaveTextThatEqualsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatEquals(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveTextThatEqualsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveTextThatEqualsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.NotHaveTextThatEquals(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatNotContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatNotContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatNotContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatNotStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatNotStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatNotStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatNotEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatNotEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveTextThatNotEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveTextThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatMatchesAttribute(string Value, Regex Matcher)
            : base(To.HaveTextThatMatches(Value, Matcher)) { }
    }

    public class WaitForElementToHaveTextThatNotMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveTextThatNotMatchesAttribute(string Value, Regex Matcher)
            : base(To.HaveTextThatNotMatches(Value, Matcher)) { }
    }



    public class WaitForElementToHaveClassAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveClassAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveClass(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveClassAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveClassAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.NotHaveClass(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveClassThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveClassThatContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveClassThatContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveClassThatContainsAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveClassThatContainsAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.NotHaveClassThatContains(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveClassThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveClassThatStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveClassThatStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveClassThatStartsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveClassThatStartsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.NotHaveClassThatStartsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveClassThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveClassThatEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.HaveClassThatEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToNotHaveClassThatEndsWithAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveClassThatEndsWithAttribute(string Value, StringComparison ComparisonType = StringComparison.Ordinal)
            : base(To.NotHaveClassThatEndsWith(Value, ComparisonType)) { }
    }

    public class WaitForElementToHaveClassThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToHaveClassThatMatchesAttribute(Regex Matcher)
            : base(To.HaveClassThatMatches(Matcher)) { }
    }

    public class WaitForElementToNotHaveClassThatMatchesAttribute : WaitForElementToAttribute
    {
        public WaitForElementToNotHaveClassThatMatchesAttribute(Regex Matcher)
            : base(To.NotHaveClassThatMatches(Matcher)) { }
    }

}