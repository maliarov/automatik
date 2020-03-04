using System;
using OpenQA.Selenium;

namespace Automatik
{
    public interface IUrlProvider
    {
        string GetUrl(Type PageType, IWebDriver webDriver);
    }
}