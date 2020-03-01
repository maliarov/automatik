using System;
using System.Reflection;
using OpenQA.Selenium;

namespace Automatik
{

    public class ResolverDecorator<TValue> : DispatchProxy
        where TValue : class
    {
        private TValue resolvedValueInstance;

        private Func<object> resolver;

        public void Init(Func<object> resolver) =>
            this.resolver = resolver;

        public void Reset() =>
            this.resolvedValueInstance = null;
 
        private TValue Resolve()
        {
            if (resolvedValueInstance != null)
                return resolvedValueInstance;

            this.resolvedValueInstance = (TValue)resolver();

            return this.resolvedValueInstance;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args) =>
            targetMethod.Invoke(Resolve(), args);
    }



}