using BoDi;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFLInterviewScenarios.BrowserFactories;

namespace TFLInterviewScenarios.SetUp
{
    internal class Context
    {
        ChromeBrowserFactory _chromeBrowserFactory;
        FirefoxBrowserFactory _firefoxBrowserFactory;
        InternetExplorerBrowserFactory _internetExplorerBrowserFactory;
        private readonly IObjectContainer objectContainer;
        private IWebDriver driver;
        string baseUrl = EnvironmentData.baseUrl;
        string browser = EnvironmentData.browser;

        public Context(IObjectContainer objectContainer
                      , ChromeBrowserFactory chromeBrowserFactory
                      , FirefoxBrowserFactory firefoxBrowserFactory
                      , InternetExplorerBrowserFactory internetExplorerBrowserFactory)
        {
            this.objectContainer = objectContainer;
            _firefoxBrowserFactory = firefoxBrowserFactory;
            _chromeBrowserFactory = chromeBrowserFactory;
            _internetExplorerBrowserFactory = internetExplorerBrowserFactory;
        }

        public void LoadApplicationUnderTest()
        {
            switch (browser.ToLower())
            {
                case "firefox":
                    driver = _firefoxBrowserFactory.Create(objectContainer);
                    break;

                case "chrome":
                    driver = _chromeBrowserFactory.Create(objectContainer);
                    break;

                case "ie":
                    driver = _internetExplorerBrowserFactory.Create(objectContainer);
                    break;

                default:
                    driver = _chromeBrowserFactory.Create(objectContainer);
                    break;
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            driver.Navigate().GoToUrl(baseUrl);
        }

        public void ShutDownApplicationUnderTest()
        {
            driver?.Quit();
        }

        public void TakeScreenshotAtThePointOfTestFailure(string directory, string scenarioName)
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string path = directory + scenarioName + DateTime.Now.ToString("yyyy-MM-dd") + ".png";
            string Screenshot = screenshot.AsBase64EncodedString;
            byte[] screenshotAsByteArray = screenshot.AsByteArray;
            //screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);

        }
    }
}
