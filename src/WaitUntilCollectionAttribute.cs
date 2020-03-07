using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitUntilCollectionAttribute : WaitAttribute
    {
        public Func<Func<IEnumerable<IWebElement>>, bool> Condition;

        public WaitUntilCollectionAttribute(Func<Func<IEnumerable<IWebElement>>, bool> Condition)
        {
            this.Condition = Condition;
        }
    }

    public class WaitUntilCollectionHasAnyAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionHasAnyAttribute()
            : base((webElements) => Wait.Until.CollectionHasCount(null, 1)(webElements)) { }
    }

    public class WaitUntilCollectionHasAtLeastAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionHasAtLeastAttribute(int Elements)
            : base((webElements) => Wait.Until.CollectionHasCount(null, Elements)(webElements)) { }
    }

    public class WaitUntilCollectionHasLessThenAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionHasLessThenAttribute(int Elements)
            : base((webElements) => Wait.Until.CollectionHasCount(Elements, null)(webElements)) { }
    }

    public class WaitUntilCollectionCountInRangeAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionCountInRangeAttribute(int Min, int Max)
            : base((webElements) => Wait.Until.CollectionHasCount(Min, Max)(webElements)) { }
    }

}