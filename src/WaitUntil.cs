using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Automatik
{
    public static class Wait
    {
        public static bool UntilExists(IWebElement webElement) => 
            webElement != null;
        public static bool UntilNotExists(IWebElement webElement) => 
            webElement == null;
 
        public static bool UntilSelected(IWebElement webElement) => 
            webElement.Selected;
        public static bool UntilDisplayed(IWebElement webElement) => 
            webElement.Displayed;
        public static bool UntilEnabled(IWebElement webElement) => 
            webElement.Enabled;
        public static bool UntilClickable(IWebElement webElement) => 
            UntilDisplayed(webElement) && UntilEnabled(webElement);

        public static bool UntilAttributeAbsent(IWebElement webElement, string AttributeName) => 
            webElement.GetAttribute(AttributeName) == null;
        public static bool UntilAttributePresent(IWebElement webElement, string AttributeName) => 
            webElement.GetAttribute(AttributeName) != null;
        public static bool UntilAttributeContains(IWebElement webElement, string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) => 
            webElement.GetAttribute(AttributeName)?.Contains(Value, comparisonType) == true;
        public static bool UntilAttributeNotContains(IWebElement webElement, string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) => 
            webElement.GetAttribute(AttributeName)?.Contains(Value, comparisonType) == false;
        public static bool UntilAttributeEquals(IWebElement webElement, string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) => 
            webElement.GetAttribute(AttributeName)?.Equals(Value, comparisonType) == true;
        public static bool UntilAttributeNotEquals(IWebElement webElement, string AttributeName, string Value, StringComparison comparisonType = StringComparison.Ordinal) => 
            webElement.GetAttribute(AttributeName)?.Equals(Value, comparisonType) == false;


        public static bool UntilTextContains(IWebElement webElement, string Value, StringComparison comparisonType = StringComparison.Ordinal) => 
            webElement.Text?.Contains(Value, comparisonType) == true;
        public static bool UntilTextNotContains(IWebElement webElement, string Value, StringComparison comparisonType = StringComparison.Ordinal) => 
            webElement.Text?.Contains(Value, comparisonType) == false;

        public static bool UntilTextEquals(IWebElement webElement, string Value, StringComparison comparisonType = StringComparison.Ordinal) => 
            webElement.Text?.Equals(Value, comparisonType) == true;
        public static bool UntilTextNotEquals(IWebElement webElement, string Value, StringComparison comparisonType = StringComparison.Ordinal) => 
            webElement.Text?.Equals(Value, comparisonType) == false;


        public static bool WaitUntilClassAbsentAttribute(IWebElement webElement, string ClassName) => 
            Array.IndexOf(webElement.GetAttribute("class").ToString().Split(" "), ClassName) == -1;
        public static bool WaitUntilClassPresentAttribute(IWebElement webElement, string ClassName) => 
            Array.IndexOf(webElement.GetAttribute("class").ToString().Split(" "), ClassName) != -1;



        public static bool UntilCollectionHasCount<TElement>(IEnumerable<TElement> collection, int? Min, int? Max) 
        {
            var count = collection.Count();
    
            if (Min.HasValue && count <= Min.Value)
                return false;

            if (Max.HasValue && Max.Value >= count)
                return false;

            return true;           
        }
    }



}