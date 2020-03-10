using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitForElementsToAttribute : WaitForElementsAttribute
    {
        public WaitForElementsToAttribute(Func<Func<IEnumerable<IWebElement>>, bool> Condition) 
            : base(Condition) { }
    }

    public class WaitForElementsToHaveCountAttribute : WaitForElementsToAttribute
    {
        public WaitForElementsToHaveCountAttribute(int Elements)
            : base(To.HaveCountInRange(Elements, Elements)) { }
    }

    public class WaitForElementsToHaveAtLeastOneAttribute : WaitForElementsToAttribute
    {
        public WaitForElementsToHaveAtLeastOneAttribute()
            : base(To.HaveCountInRange(1, null)) { }
    }

    public class WaitForElementsToHaveCountLessThenAttribute : WaitForElementsToAttribute
    {
        public WaitForElementsToHaveCountLessThenAttribute(int Elements)
            : base(To.HaveCountInRange(null, Elements)) { }
    }

    public class WaitForElementsToHaveCountMoreThenAttribute : WaitForElementsToAttribute
    {
        public WaitForElementsToHaveCountMoreThenAttribute(int Elements)
            : base(To.HaveCountInRange(Elements, null)) { }
    }

    public class WaitForElementsToHaveCountInRangeAttribute : WaitForElementsToAttribute
    {
        public WaitForElementsToHaveCountInRangeAttribute(int Min, int Max)
            : base(To.HaveCountInRange(Min, Max)) { }
    }
}