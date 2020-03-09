using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Automatik
{
    public static class To
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

        public static Func<Func<IWebElement>, bool> Exist() =>
            (resolve) => h(() => resolve() != null && resolve().TagName != null, false);
        public static Func<Func<IWebElement>, bool> NotExist() =>
            (resolve) => h(() => resolve() == null || resolve().TagName == null, true);

        public static Func<Func<IWebElement>, bool> BeSelected() =>
            (resolve) => h(() => resolve().Selected, false);
        public static Func<Func<IWebElement>, bool> BeNotSelected() =>
            (resolve) => h(() => !resolve().Selected, false);

        public static Func<Func<IWebElement>, bool> BeDisplayed() =>
            (resolve) => h(() => resolve().Displayed, false);
        public static Func<Func<IWebElement>, bool> BeHidden() =>
            (resolve) => h(() => !resolve().Displayed, false);

        public static Func<Func<IWebElement>, bool> BeEnabled() =>
            (resolve) => h(() => resolve().Enabled, false);
        public static Func<Func<IWebElement>, bool> BeDisabled() =>
            (resolve) => h(() => !resolve().Enabled, false);

        public static Func<Func<IWebElement>, bool> BeClickable() =>
            (resolve) => BeDisplayed()(resolve) && BeEnabled()(resolve);

        public static Func<Func<IWebElement>, bool> HaveAttribute(string AttributeName) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName) != null, false);
        public static Func<Func<IWebElement>, bool> NotHaveAttribute(string AttributeName) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName) == null, false);

        public static Func<Func<IWebElement>, bool> HabeAttributeThatContains(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName)?.Contains(Value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveAttributeThatNotContains(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName)?.Contains(Value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveAttributeThatEquals(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName)?.Equals(Value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveAttributeThatNotEquals(string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().GetAttribute(AttributeName)?.Equals(Value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveTextThatContains(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().Text?.Contains(Value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveTextThatNotContains(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().Text?.Contains(Value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveTextThatEquals(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().Text?.Equals(Value, comparisonType) == true, false);
        public static Func<Func<IWebElement>, bool> HaveTextThatNotEquals(string Value, StringComparison comparisonType = StringComparison.Ordinal) =>
            (resolve) => h(() => resolve().Text?.Equals(Value, comparisonType) == false, false);

        public static Func<Func<IWebElement>, bool> HaveClassAttribute(string ClassName) =>
            (resolve) => h(() => Array.IndexOf((resolve().GetAttribute("class") ?? "").Split(" "), ClassName) != -1, false);
        public static Func<Func<IWebElement>, bool> NotHaveClassAttribute(string ClassName) =>
            (resolve) => h(() => Array.IndexOf((resolve().GetAttribute("class") ?? "").Split(" "), ClassName) == -1, false);


        public static Func<Func<IEnumerable<IWebElement>>, bool> HaveCountInRange(int? Min, int? Max) {
            if (Min.HasValue && Max.HasValue && Min > Max)
                throw new Exception("[Min] param should be less or equal to [Max] param");
            
            return (resolve) => h(() =>             
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