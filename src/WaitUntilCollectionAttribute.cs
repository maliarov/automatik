using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitUntilCollectionAttribute : WaitAttribute
    {
        public Func<IEnumerable<IWebElement>, bool> Condition;

        public WaitUntilCollectionAttribute(Func<IEnumerable<IWebElement>, bool> Condition)
        {
            this.Condition = Condition;
        }
    }

    public class WaitUntilCollectionHasAnyAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionHasAnyAttribute()
            : base((webElements) => Wait.UntilCollectionHasCount(webElements, null, 1)) { }
    }

    public class WaitUntilCollectionHasAtLeastAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionHasAtLeastAttribute(int NElements)
            : base((webElements) => Wait.UntilCollectionHasCount(webElements, null, NElements)) { }
    }

    public class WaitUntilCollectionHasLessThenAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionHasLessThenAttribute(int NElements)
            : base((webElements) => Wait.UntilCollectionHasCount(webElements, NElements, null)) { }
    }

    public class WaitUntilCollectionCountInRangeAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionCountInRangeAttribute(int Min, int Max)
            : base((webElements) => Wait.UntilCollectionHasCount(webElements, Min, Max)) { }
    }

}