using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Automatik
{
    public static class ElementsTo
    {
        public static Func<Func<IEnumerable<IWebElement>>, bool> Exist() =>
            (resolve) => Utils.safeResolve(() => (
                    resolve() != null &&
                    resolve().Count() >= 0 &&
                    resolve().All(webElement => ElementTo.Exist()(() => webElement))
                ),
                false
            );
        public static Func<Func<IEnumerable<IWebElement>>, bool> NotExist() =>
            (resolve) => Utils.safeResolve(() => (
                    resolve() == null || 
                    resolve().All(webElement => ElementTo.NotExist()(() => webElement))
                ),
                true
            );

        public static Func<Func<IEnumerable<IWebElement>>, bool> HaveCountInRange(int? Min, int? Max)
        {
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