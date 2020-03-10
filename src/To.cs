using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace Automatik
{
    public static class To
    {
        public static Func<Func<IWebElement>, bool> Exist() =>
            (resolve) => Utils.safeResolve(() => resolve() != null && resolve().TagName != null, false);
        public static Func<Func<IWebElement>, bool> NotExist() =>
            (resolve) => Utils.safeResolve(() => resolve() == null || resolve().TagName == null, true);



        public static Func<Func<IWebElement>, bool> BecomeSelected() =>
            (resolve) => Utils.safeResolve(() => resolve().Selected, false);
        public static Func<Func<IWebElement>, bool> BecomeNotSelected() =>
            (resolve) => Utils.safeResolve(() => !resolve().Selected, false);

        public static Func<Func<IWebElement>, bool> BecomeDisplayed() =>
            (resolve) => Utils.safeResolve(() => resolve().Displayed, false);
        public static Func<Func<IWebElement>, bool> BecomeHidden() =>
            (resolve) => Utils.safeResolve(() => !resolve().Displayed, false);

        public static Func<Func<IWebElement>, bool> BecomeEnabled() =>
            (resolve) => Utils.safeResolve(() => resolve().Enabled, false);
        public static Func<Func<IWebElement>, bool> BecomeDisabled() =>
            (resolve) => Utils.safeResolve(() => !resolve().Enabled, false);

        public static Func<Func<IWebElement>, bool> BecomeClickable() =>
            (resolve) => BecomeDisplayed()(resolve) && BecomeEnabled()(resolve);




        public static Func<Func<IWebElement>, bool> HaveAttributeWithName(string name) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttribute(name) != null, false);
        public static Func<Func<IWebElement>, bool> NotHaveAttributeWithName(string name) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttribute(name) == null, false);

        public static Func<Func<IWebElement>, bool> HaveAttributeWithNameThatContains(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttributesNames().Any((name) => name.Contains(value, comparisonType)), false);
        public static Func<Func<IWebElement>, bool> NotHaveAttributeWithNameThatContains(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttributesNames().All((name) => !name.Contains(value, comparisonType)), false);

        public static Func<Func<IWebElement>, bool> HaveAttributeWithNameThatStartsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttributesNames().Any((name) => name.StartsWith(value, comparisonType)), false);
        public static Func<Func<IWebElement>, bool> NotHaveAttributeWithNameThatStartsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttributesNames().All((name) => !name.StartsWith(value, comparisonType)), false);

        public static Func<Func<IWebElement>, bool> HaveAttributeWithNameThatEndsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttributesNames().Any((name) => name.EndsWith(value, comparisonType)), false);
        public static Func<Func<IWebElement>, bool> NotHaveAttributeWithNameThatEndsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttributesNames().All((name) => !name.EndsWith(value, comparisonType)), false);

        public static Func<Func<IWebElement>, bool> HaveAttributeWithNameThatMatches(Regex matcher) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttributesNames().Any((name) => matcher.IsMatch(name)), false);
        public static Func<Func<IWebElement>, bool> NotHaveAttributeWithNameThatMatches(Regex matcher) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttributesNames().All((name) => !matcher.IsMatch(name)), false);



        public static Func<Func<IWebElement>, bool> HaveAttributeWithValueThatContains(string name, string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttribute(name)?.Contains(value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveAttributeWithValueThatNotContains(string name, string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttribute(name)?.Contains(value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveAttributeWithValueThatStartsWith(string name, string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttribute(name)?.StartsWith(value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveAttributeWithValueThatNotStartsWith(string name, string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttribute(name)?.StartsWith(value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveAttributeWithValueThatEndsWith(string name, string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttribute(name)?.EndsWith(value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveAttributeWithValueThatNotEndsWith(string name, string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttribute(name)?.EndsWith(value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveAttributeWithValueThatEquals(string name, string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttribute(name)?.Equals(value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveAttributeWithValueThatNotEquals(string name, string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetAttribute(name)?.Equals(value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveAttributeWithValueThatMatches(string name, Regex matcher) =>
            (resolve) => Utils.safeResolve(() => matcher.IsMatch(resolve().GetAttribute(name) ?? string.Empty), false);
        public static Func<Func<IWebElement>, bool> HaveAttributeWithValueThatNotMatches(string name, Regex matcher) =>
            (resolve) => Utils.safeResolve(() => !matcher.IsMatch(resolve().GetAttribute(name) ?? string.Empty), false);


        public static Func<Func<IWebElement>, bool> HaveText() =>
            (resolve) => Utils.safeResolve(() => resolve().Text != null, false);
        public static Func<Func<IWebElement>, bool> HaveTextNotEmptyOrWhiteSpaces() =>
            (resolve) => Utils.safeResolve(() => !string.IsNullOrWhiteSpace(resolve().Text ?? string.Empty), false);
        public static Func<Func<IWebElement>, bool> NotHaveText() =>
            (resolve) => Utils.safeResolve(() => resolve().Text == null, false);

        public static Func<Func<IWebElement>, bool> HaveTextThatEquals(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().Text?.Equals(value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> NotHaveTextThatEquals(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().Text?.Equals(value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveTextThatContains(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().Text?.Contains(value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveTextThatNotContains(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().Text?.Contains(value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveTextThatStartsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().Text?.StartsWith(value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveTextThatNotStartsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().Text?.StartsWith(value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveTextThatEndsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().Text?.EndsWith(value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveTextThatNotEndsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().Text?.EndsWith(value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveTextThatMatches(string value, Regex matcher) =>
            (resolve) => Utils.safeResolve(() => matcher.IsMatch(resolve().Text ?? string.Empty), false);
        public static Func<Func<IWebElement>, bool> HaveTextThatNotMatches(string value, Regex matcher) =>
            (resolve) => Utils.safeResolve(() => !matcher.IsMatch(resolve().Text ?? string.Empty), false);



        public static Func<Func<IWebElement>, bool> HaveClass(string name, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetCssClasses().Any(className => className.Equals(name, comparisonType)), false);
        public static Func<Func<IWebElement>, bool> NotHaveClass(string name, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetCssClasses().All(className => !className.Equals(name, comparisonType)), false);

        public static Func<Func<IWebElement>, bool> HaveClassThatContains(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetCssClasses().Any(className => className.Contains(value, comparisonType)), false);
        public static Func<Func<IWebElement>, bool> NotHaveClassThatContains(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetCssClasses().All(className => !className.Contains(value, comparisonType)), false);

        public static Func<Func<IWebElement>, bool> HaveClassThatStartsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetCssClasses().Any(className => className.StartsWith(value, comparisonType)), false);
        public static Func<Func<IWebElement>, bool> NotHaveClassThatStartsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetCssClasses().All(className => !className.StartsWith(value, comparisonType)), false);

        public static Func<Func<IWebElement>, bool> HaveClassThatEndsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetCssClasses().Any(className => className.EndsWith(value, comparisonType)), false);
        public static Func<Func<IWebElement>, bool> NotHaveClassThatEndsWith(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => Utils.safeResolve(() => resolve().GetCssClasses().All(className => !className.EndsWith(value, comparisonType)), false);

        public static Func<Func<IWebElement>, bool> HaveClassThatMatches(Regex matcher) =>
            (resolve) => Utils.safeResolve(() => resolve().GetCssClasses().Any(className => matcher.IsMatch(className)), false);
        public static Func<Func<IWebElement>, bool> NotHaveClassThatMatches(Regex matcher) =>
            (resolve) => Utils.safeResolve(() => resolve().GetCssClasses().All(className => !matcher.IsMatch(className)), false);



        public static Func<Func<IEnumerable<IWebElement>>, bool> HaveCountInRange(int? Min, int? Max) {
            if (Min.HasValue && Max.HasValue && Min > Max)
                throw new Exception("[Min] param should be less or equal to [Max] param");
            
            return (resolve) => Utils.safeResolve(() =>             
            {
                var count = resolve().Count();

                if (Min.HasValue && count < Min.Value)
                    return false;

                if (Max.HasValue && Max.Value < count)
                    return false;

                return true;
            }, false);
        }
    }
}