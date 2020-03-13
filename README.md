# Automatik

Yet another Selenium PageObject help library

## Install

Visit [Automatik NuGet](https://www.nuget.org/packages/automatik) 

Or simply clone [Automatik GitHub](https://github.com/maliarov/automatik) repository and reference it to your .Net Core project.

## Overview

This is not a framework or bootstraper with strict rules pushing you to align mandatory guidlines. But it is rather a simple class library with some "magic" inside that can help you to build more rapid workflow with minimum boilerplate code and rules.

The Core concept of library is PageObject. So to start use it you simply need to define your class that will represent elements on a page with the help of attributes.


```
[Page]
public class TestPage
{
    [FindByCssSelector("input#first-name")]
    public IWebElement FirstNameInput;
    
    [FindByCssSelector("input#last-name")]
    public IWebElement LastNameInput { get; set; }
}
```

It supports public fields and properties for mapping web elements. 

Now we can go further and do some actions with your new page class. 
We will use ChromeDriver for that.

```
// construct your web driver instance
using (var webDriver = new ChromeDriver())
{
    // and set basic options for it
    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

    // ask driver to navigate to needed url
    webDriver.Navigate().GoToUrl("http://my-test-page.com");

    // and create binded instance of your page class
    var testPage = webDriver.Bind<TestPage>();

    // do some work and check results :)
    testPage.FirstNameInput.SendKeys("Oleksii");
    testPage.LastNameInput.SendKeys("Maliarov");

    if (testPage.FirstNameInput.Text != "Oleksii")
        throw new Exception("Oops... Wrong first name.");

    if (testPage.LastNameInput.Text != "Maliarov")
        throw new Exception("Oops... Wrong last name.");
}
```

As you can see it is pretty simple, but "Devil in details" (c) as it usually ;)

Do some homework and review following attributes and their variants:

For classes:
```
[Page]
[Page(Url="http://my-page-url")]
[Page(Url="http://my-page-url/{type}?{skip}&{limit}")] // check IWebDriver.Navigate<TPage>(object? options = null) for that
[Page(UrlProvider=typeof(MyUrlProvider))] // where MyUrlProvider implments IUrlProvider
```

For fields and properties:
```
[FindByCssSelector("div>span")]
[FindByXPath("//*")]
[FindByClassName("my-class")]
[FindById("firstname")]
[FindByName("lastname")]
[FindByTagName("h1")]
```

Also pay some attention for IWebDriver extensions methods:

```
    TPage IWebDriver.Bind<TPage>();
    TPage IWebDriver.Bind<TPage>(TPage page);

    TPage IWebDriver.Navigate<TPage>(string url);
    TPage IWebDriver.Navigate<TPage>(object? options = null); // new { type = "cars", skip = 1, limit = 14 }
```

But real magic comes to stage when you start using more complex types for mapping:

```
public class MyPage
{
    [FindByCssSelector("input#first-name")]
    IWebElement FirstNameInput;

    [FindByCssSelector("div#some-popup")]
    PopupContainer MyPopup;

    [FindByCssSelector("table#my-table")]
    Table<StrictRow> MyStrictTable;

    [FindByCssSelector("table#my-table")]
    Table<DynamicRow<CellWithButton>> MyDynamicTable;

    [FindByCssSelector("input")]
    IEnumerable<IWebElement> AllInputsOnPage;

    [FindByCssSelector("ul#my-list>li")]
    IEnumerable<ListOption> MyListOptions;
}

public class PopupContainer
{
    // for inject root web element use public property with:
    // public setter 
    // exact name WebElement 
    // exact type IWebElement
    public IWebElement WebElement { get; set; }

    // or you can have it injected in constructor
    public PopupContainer(IWebElement webElement) 
    {
        // here you can save and access reference to a root web element
    }

    // note that both variants are optional, you can still have no or default constructor for class if you do not need it
}


public class Table<TRow>
{
    [FindByCssSelector("tbody>tr")]
    public IEnumerable<TRow> Rows;
}

public class StrictRow
{
    [FindByCssSelector("td:nth-of-type(1)")]
    public IWebElement Cell1;

    [FindByCssSelector("td:nth-of-type(2)")]
    public IWebElement Cell2;
}

public class DynamicRow<TCell>
{
    [FindByCssSelector("td")]
    public IEnumerable<TCell> Cells;
}

public class CellWithButton
{
    [FindByCssSelector("input[type='button']")]
    public IWebElement Button;

    public void ClickOnMySuperButton() => 
        Button.Click();
}

public class ListOption
{
    IWebElement WebElement { get; set; }

    public bool IsTextMatches(Regex matcher) =>
        matcher.IsMatch(WebElement.Text);
}
```

All previous examples rely on web driver implicit timeout of elements resolving. Lets suppose you have more complex case when element needs to be resolved on a fly with late binding?

In  our next example we will have license accept modal that will popups on out page after load in ~10 seconds after some video done. It will expects us to click Yes button and wait till modal gone.


```
[Page]
public class MyPage 
{
    [FindByCssSelector("div.modal#license-agreement")]
    [WaitForElementBecomeDisplayed(TimeoutInterval = 10)]
    public Modal<TextBody, YesNoFooter> LicenseAgreementModal;
}

public class Modal<TBody, TFooter>
{
    public IWebElement WebElement { get; set; }

    [FindByCssSelector("div.modal-header>h2")]
    public IWebElement Title;    

    [FindByCssSelector("div.modal-body")]
    public TBody Body;    

    [FindByCssSelector("div.modal-footer")]
    public TFooter Footer;    
}

public class TextBody
{
    [FindByCssSelector("p")]
    IWebElement Text;
}

public class YesNoFooter
{
    [FindByCssSelector("input[type='button']#yes")]
    IWebElement YesButton;

    [FindByCssSelector("input[type='button']#no")]
    IWebElement NoButton;
}
```

Let's now try it in action!

```
using (var webDriver = new ChromeDriver()) 
{
    // our avarage wait is 5 sec, so no need to do 10 secs 
    // only because one exceptional case with license accept modal
    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

    var myPage = webDriver.Navigate<MyPage>();

    // because of some magic inside background code 
    // during access to LicenseAgreementModal code will wait for 10 sec 
    // while modal appears on page and only then resolve our binding 
    myPage.LicenseAgreementModal.Footer.YesButton.Click();

    // now lets wait till modal gone...
    var wait = webDriver.WaitAll(ElementTo.NotExist());

    wait(() => myPage.LicenseAgreementModal.WebELement);

    // we done!
}
```

Now make some more homework and review semantics of following attributes

```
[WaitForElements...] // for IEnumerable<> mapping
[WaitForElement...] // element specific
```

You also can combine them for IEnumerable mapping, then binding will wait till [WaitForElements...] resolves and each element satisfy [WaitForElement...]

In following example we will wait till all 5 inputs appears on page and become displayed

```
public class MyPage
{
    [FindByTabName("input"))]
    [WaitForElementsToHaveCount(5)]
    [WaitForElementToBecomeDisplayed]
    IEnumerable<IWebElement> Inputs;
}

// web driver initialization
// ...

var myPage = webDriver.Bind<Page>();

var text = string.Join(", ", myPage.Inputs.Select(input => input.Text));

if (text != "1, 2, 3, 4, 5")
    throw new Exception("Opps! Wrong text value.");

```

Feel free to experiment with IWebDriver.WaitAll or IWebDriver.WaitAny extensions methods and conditions provided by ElementTo and ElementsTo classes.


___
May the Force be with you!
