using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Automatik
{
    public static partial class Wait
    {
        public static class Until
        {
            public static Func<Func<IWebElement>, bool> Exists() =>
                (resolve) => resolve() != null;
            public static Func<Func<IWebElement>, bool> NotExists() =>
                (resolve) => resolve() == null;

            public static Func<Func<IWebElement>, bool> Selected() =>
                (resolve) => resolve()?.Selected == true;
            public static Func<Func<IWebElement>, bool> Displayed() =>
                (resolve) => resolve()?.Displayed == true;
            public static Func<Func<IWebElement>, bool> Enabled() =>
                (resolve) => resolve()?.Enabled == true;
            public static Func<Func<IWebElement>, bool> Clickable() =>
                (resolve) => Displayed()(resolve) && Enabled()(resolve);

            public static Func<Func<IWebElement>, bool> AttributeAbsent(string AttributeName) =>
                (resolve) => resolve()?.GetAttribute(AttributeName) == null;
            public static Func<Func<IWebElement>, bool> AttributePresent(string AttributeName) =>
                (resolve) => resolve()?.GetAttribute(AttributeName) != null;
            public static Func<Func<IWebElement>, bool> AttributeContains(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
                (resolve) => resolve()?.GetAttribute(AttributeName)?.Contains(Value, comparisonType) == true;
            public static Func<Func<IWebElement>, bool> AttributeNotContains(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
                (resolve) => resolve()?.GetAttribute(AttributeName)?.Contains(Value, comparisonType) == false;
            public static Func<Func<IWebElement>, bool> AttributeEquals(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
                (resolve) => resolve()?.GetAttribute(AttributeName)?.Equals(Value, comparisonType) == true;
            public static Func<Func<IWebElement>, bool> AttributeNotEquals(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
                (resolve) => resolve()?.GetAttribute(AttributeName)?.Equals(Value, comparisonType) == false;


            public static Func<Func<IWebElement>, bool> TextContains(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
                (resolve) => resolve()?.Text?.Contains(Value, comparisonType) == true;
            public static Func<Func<IWebElement>, bool> TextNotContains(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
                (resolve) => resolve()?.Text?.Contains(Value, comparisonType) == false;

            public static Func<Func<IWebElement>, bool> TextEquals(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
                (resolve) => resolve()?.Text?.Equals(Value, comparisonType) == true;
            public static Func<Func<IWebElement>, bool> TextNotEquals(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
                (resolve) => resolve()?.Text?.Equals(Value, comparisonType) == false;


            public static Func<Func<IWebElement>, bool> ClassAbsentAttribute(string ClassName) =>
                (resolve) => Array.IndexOf((resolve()?.GetAttribute("class").ToString() ?? "").Split(" "), ClassName) == -1;
            public static Func<Func<IWebElement>, bool> ClassPresentAttribute(string ClassName) =>
                (resolve) => Array.IndexOf((resolve()?.GetAttribute("class").ToString() ?? "").Split(" "), ClassName) != -1;



            public static Func<Func<IEnumerable<IWebElement>>, bool> CollectionHasCount(int? Min, int? Max) =>
                (Func<Func<IEnumerable<IWebElement>>, bool>)((resolve) =>
                {
                    var count = resolve()?.Count();

                    if (!count.HasValue)
                        return false;

                    if (Min.HasValue && count < Min.Value)
                        return false;

                    if (Max.HasValue && Max.Value > count)
                        return false;

                    return true;
                });
        }
    }
}