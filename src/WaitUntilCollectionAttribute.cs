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
            : base(Until.CollectionHasCount(null, 1)) { }
    }

    public class WaitUntilCollectionHasAtLeastAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionHasAtLeastAttribute(int Elements)
            : base(Until.CollectionHasCount(null, Elements)) { }
    }

    public class WaitUntilCollectionHasLessThenAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionHasLessThenAttribute(int Elements)
            : base(Until.CollectionHasCount(Elements, null)) { }
    }

    public class WaitUntilCollectionCountInRangeAttribute : WaitUntilCollectionAttribute
    {
        public WaitUntilCollectionCountInRangeAttribute(int Min, int Max)
            : base(Until.CollectionHasCount(Min, Max)) { }
    }

}