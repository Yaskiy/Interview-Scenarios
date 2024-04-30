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
    internal class JourneyPage
    {
        IWebDriver _driver;
        public JourneyPage(IWebDriver driver)
        {
            _driver = driver;
        }
        By confirmJourneyPg = By.CssSelector("span[class='jp-results-headline']");
        By locationSeen = By.XPath("//*[@id='full-width-content']/div/div[8]");
        By noJourneyMarching = By.CssSelector("li[class='field-validation-error']");
        By editJourneyLnk = By.XPath("//span[text()='Edit journey']");
        By clearFromField = By.XPath("//a[text()='Clear From location']");
        By clearToField = By.XPath("//a[text()='Clear To location']");
        By planAJourneyLink = By.XPath("//*[@id='mainnav']/div[2]/div/div[2]/ul/li[1]/a");
        By recentjourneys = By.CssSelector("div[id='recent-journeys']");

        public string ConfirmJourneyPage()
        {
           return _driver.FindElement(confirmJourneyPg).Text.Trim();
        }

        public string FromAndToLocationAppears()
        {
           return _driver.FindElement(locationSeen).Text.Trim();
        }

        public string NoMatchingLocation()
        {
           return _driver.FindElement(noJourneyMarching).Text.Trim();
        }

        public void ClickEditJourneyLnk()
        {
            _driver.Click(editJourneyLnk);
        }

        public void ClearFromTextFields()
        {
            _driver.Click(clearFromField);
           
        }

        public void ClearToTextFields()
        {
            
            _driver.Click(clearToField);
        }

        public void ClickPlanAjourney()
        {
            _driver.Click(planAJourneyLink);
        }

        public string ListOfRecentJourneys()
        {
           return _driver.FindElement(recentjourneys).Text.Trim();
        }
    }
}
