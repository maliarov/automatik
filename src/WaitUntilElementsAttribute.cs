using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Automatik
{
    public abstract class WaitUntilElementsAttribute : WaitElementsAttribute
    {
        public WaitUntilElementsAttribute(Func<Func<IEnumerable<IWebElement>>, bool> Condition) 
            : base(Condition) { }
    }

    public class WaitUntilElementsHasAnyAttribute : WaitUntilElementsAttribute
    {
        public WaitUntilElementsHasAnyAttribute()
            : base(Until.ElementsHaveCount(null, 1)) { }
    }

    public class WaitUntilElementsHasAtLeastAttribute : WaitUntilElementsAttribute
    {
        public WaitUntilElementsHasAtLeastAttribute(int Elements)
            : base(Until.ElementsHaveCount(null, Elements)) { }
    }

    public class WaitUntilElementsHaveLessThen : WaitUntilElementsAttribute
    {
        public WaitUntilElementsHaveLessThen(int Elements)
            : base(Until.ElementsHaveCount(Elements, null)) { }
    }

    public class WaitUntilElemetsCountInRangeAttribute : WaitUntilElementsAttribute
    {
        public WaitUntilElemetsCountInRangeAttribute(int Min, int Max)
            : base(Until.ElementsHaveCount(Min, Max)) { }
    }

}