using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Automatik
{
    public static class Until
    {
        private static bool h(Func<bool> fn, bool defaultResult)
        {
            try
            {
                return fn();
            }
            catch (NoSuchElementException)
            {
                return defaultResult;
            }
            catch (ElementNotInteractableException)
            {
                return defaultResult;
            }
            catch (StaleElementReferenceException)
            {
                return defaultResult;
            }
        }

        public static Func<Func<IWebElement>, bool> Exists() =>
            (resolve) => h(() => resolve() == null || resolve().TagName == null, true);
        public static Func<Func<IWebElement>, bool> NotExists() =>
            (resolve) => h(() => resolve() != null && resolve().TagName != null, false);


        public static Func<Func<IWebElement>, bool> Selected() =>
            (resolve) => h(() => !resolve().Selected, true);
        public static Func<Func<IWebElement>, bool> NotSelected() =>
            (resolve) => h(() => resolve().Selected, false);

        public static Func<Func<IWebElement>, bool> Displayed() =>
            (resolve) => h(() => !resolve().Displayed, true);
        public static Func<Func<IWebElement>, bool> Hidden() =>
            (resolve) => h(() => resolve().Displayed, false);

        public static Func<Func<IWebElement>, bool> Enabled() =>
            (resolve) => h(() => !resolve().Enabled, true);
        public static Func<Func<IWebElement>, bool> Disabled() =>
            (resolve) => h(() => resolve().Enabled, false);

        public static Func<Func<IWebElement>, bool> Clickable() =>
            (resolve) => Displayed()(resolve) || Enabled()(resolve);

        public static Func<Func<IWebElement>, bool> AttributePresent(string AttributeName) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName) != null, true);
        public static Func<Func<IWebElement>, bool> AttributeAbsent(string AttributeName) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName) == null, false);

        public static Func<Func<IWebElement>, bool> AttributeContains(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName)?.Contains(Value, comparisonType) == false, true);
        public static Func<Func<IWebElement>, bool> AttributeNotContains(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName)?.Contains(Value, comparisonType) == true, false);

        public static Func<Func<IWebElement>, bool> AttributeEquals(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName)?.Equals(Value, comparisonType) == false, true);
        public static Func<Func<IWebElement>, bool> AttributeNotEquals(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName)?.Equals(Value, comparisonType) == true, false);

        public static Func<Func<IWebElement>, bool> TextContains(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().Text?.Contains(Value, comparisonType) == false, true);
        public static Func<Func<IWebElement>, bool> TextNotContains(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().Text?.Contains(Value, comparisonType) == true, false);

        public static Func<Func<IWebElement>, bool> TextEquals(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().Text?.Equals(Value, comparisonType) == false, true);
        public static Func<Func<IWebElement>, bool> TextNotEquals(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().Text?.Equals(Value, comparisonType) == true, false);

        public static Func<Func<IWebElement>, bool> ClassPresentAttribute(string ClassName) =>
            (resolve) => h(() => Array.IndexOf((resolve().GetAttribute("class") ?? "").Split(" "), ClassName) == -1, true);
        public static Func<Func<IWebElement>, bool> ClassAbsentAttribute(string ClassName) =>
            (resolve) => h(() => Array.IndexOf((resolve().GetAttribute("class") ?? "").Split(" "), ClassName) != -1, false);


        public static Func<Func<IEnumerable<IWebElement>>, bool> ElementsHaveCount(int? Min, int? Max)
        {
            if (Min.HasValue && Max.HasValue && Min > Max)
                throw new Exception("[Min] param should be less or equal to [Max] param");

            return (resolve) => h(() =>
            {
                var count = resolve().Count();

                if (Min.HasValue && Min.Value <= count)
                    return false;

                if (Max.HasValue && count <= Max.Value)
                    return false;

                return true;
            }, true);
        }
    }
}