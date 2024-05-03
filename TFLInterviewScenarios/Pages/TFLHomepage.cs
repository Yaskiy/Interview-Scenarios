using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TFLInterviewScenarios.SetUp;

namespace TFLInterviewScenarios.Pages
{
    internal class TFLHomepage
    {
        IWebDriver _driver;
        public TFLHomepage(IWebDriver driver)
        {
            _driver = driver;
        }
        By fromTextField = By.CssSelector("input[id='InputFrom']");
        By toTextField = By.CssSelector("input[id='InputTo']");
        By leavingNow = By.XPath("//span[text()='now']");
        By planJourneyBtn = By.XPath("//*[@id='plan-journey-button']");
        By clickPlane = By.CssSelector("div[id='main-hero']");
        By acceptCokies = By.XPath("//strong[text()='Accept only essential cookies']");
        By inputFromError = By.CssSelector("span[id='InputFrom-error']");
        By inputToError = By.CssSelector("span[id='InputTo-error']");
        By changeTimeLnk = By.CssSelector("a[class='change-departure-time']");
        By confirmArrivngOption = By.XPath("//label[text()='Arriving']");
        By dateDropdown = By.CssSelector("select[id='Date']");
        By timeDropdown = By.CssSelector("select[id='Time']");
        By selectDate = By.XPath("//select[@id='Date']/option[text()='Tomorrow']");
        By selectTime = By.CssSelector("select[id='Time'] [value='1430']");
        By recentTab = By.CssSelector("a[href='#jp-recent']");
        By functionalitycookies = By.XPath("//*[@id='jp-recent-content-home-']/ul/div/div[2]/a");
        By acceptAllCookies = By.CssSelector("button[onclick='acceptAllCookies(); return false;']");
        By recentJourneys = By.CssSelector("div[id='recent-journeys']");
        By newTab = By.CssSelector("a[href='#jp-new']");
        public void ClickEssentialCookies()
        {
            _driver.Click(acceptCokies);
        }

        public void InputFromLocation(string fromLcData)
        {
            _driver.FindElement(fromTextField).Clear();
            Thread.Sleep(1000);
            _driver.ClearAndSendKeys(fromTextField, fromLcData);
            //Thread.Sleep(8000);
            //_driver.Click(clickPlane);
        }

        public void InputToLocation(string toLcData)
        {
            Thread.Sleep(2000);
            _driver.FindElement(toTextField).Clear();
            Thread.Sleep(1000);
            _driver.ClearAndSendKeys(toTextField, toLcData);
            //Thread.Sleep(8000);
            //_driver.Click(clickPlane);
        }

        public string ConfirmLeavingIcon()
        {
            return _driver.FindElement(leavingNow).Text.Trim();
        }

        public JourneyPage ClickPlanJouneyBtn()
        {
            Thread.Sleep(3000);
            _driver.Click(planJourneyBtn);
            return new JourneyPage(_driver);
        }

        public string NullFromFieldError()
        {
           return _driver.FindElement(inputFromError).Text.Trim();
        }

        public string NullToFieldError()
        {
            return _driver.FindElement(inputToError).Text.Trim();
        }

        public void ClickChangeTimeLnk()
        {
            _driver.Click(changeTimeLnk);
        }

        public string ConfirmArrivingOption()
        {
           return _driver.FindElement(confirmArrivngOption).Text.Trim();  
        }

        public void ClickArrivingOption()
        {
            _driver.Click(confirmArrivngOption);
        }

        public void ClickAndSelectDate(string dateData)
        {
            _driver.Click(dateDropdown);
            _driver.SelectOptionByText(selectDate, dateData);
            _driver.Click(selectDate);
        }

        public void ClickAndSelectTime(string timeData)
        {
            _driver.Click(timeDropdown);
            _driver.SelectOptionByText(selectTime, timeData);
            _driver.Click(selectTime);
        }

        public void ClickRecentTab()
        {
            _driver.Click(recentTab);
        }

        public void ClickFunctionalityCookies()
        {
            _driver.Click(functionalitycookies);
        }

        public void ClickAcceptAllCookies()
        {
            _driver.Click(acceptAllCookies);
        }

        public string ListOfRecentJourney()
        {
           return _driver.FindElement(recentJourneys).Text.Trim();
        }

        public void ClickNewTab()
        {
            _driver.Click(newTab);
        }
    }
}
       

