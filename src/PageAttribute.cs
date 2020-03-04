using System;

namespace Automatik
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PageAttribute : Attribute
    {
        public readonly string Url;

        public Type ParentPage { get; set; }

        public PageAttribute(string Url)
        {
            this.Url = Url;
        }
    }

}