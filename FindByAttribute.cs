using System;
using OpenQA.Selenium;

namespace Automatik
{
    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Property, 
        AllowMultiple = false
    )]
    public abstract class FindByAttribute : System.Attribute
    {
        public readonly By By;

        public FindByAttribute(By By)
        {
            this.By = By;
        }
    }

    public class FindByCssSelectorAttribute : FindByAttribute
    {
        public FindByCssSelectorAttribute(string CssSelector)
            : base(By.CssSelector(CssSelector))
        {
        }
    }

    public class FindByXPathAttribute : FindByAttribute
    {
        public FindByXPathAttribute(string XPath)
            : base(By.XPath(XPath))
        {
        }
    }

    public class FindByClassNameAttribute : FindByAttribute
    {
        public FindByClassNameAttribute(string ClassName)
            : base(By.ClassName(ClassName))
        {
        }
    }

    public class FindByIdAttribute : FindByAttribute
    {
        public FindByIdAttribute(string Id)
            : base(By.Id(Id))
        {
        }
    }

    public class FindByNameAttribute : FindByAttribute
    {
        public FindByNameAttribute(string Name)
            : base(By.Name(Name))
        {
        }
    }

    public class FindByTagNameAttribute : FindByAttribute
    {
        public FindByTagNameAttribute(string TagName)
            : base(By.TagName(TagName))
        {
        }
    }

}
