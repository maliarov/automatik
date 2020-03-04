using System;

namespace Automatik
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PageAttribute : Attribute
    {
        public string Url { get; set; }

        public Type UrlProvider { get; set; }

        public Type ParentPage { get; set; }

        public PageAttribute()
        {
        }
    }
}