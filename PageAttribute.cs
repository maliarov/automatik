using System;

namespace Automatik
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PageAttribute : Attribute
    {
        public readonly Uri Uri;

        public PageAttribute(string Url)
        {
            Uri = new Uri(Url);
        }
    }

}