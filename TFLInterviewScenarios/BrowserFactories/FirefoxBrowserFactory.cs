using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFLInterviewScenarios.BrowserFactories
{
    internal class FirefoxBrowserFactory
    {
        private readonly IObjectContainer objectContainer;
        public string GeckoDriverPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public TimeSpan WebDriverTimeout { get; set; } = TimeSpan.FromSeconds(60);
        public bool Headless { get; set; } = false;

        public FirefoxBrowserFactory(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        public IWebDriver Create(IObjectContainer objectContainer)
        {
            IWebDriver driver;

            var firefoxOptions = new FirefoxOptions
            {
                Profile = new FirefoxProfile
                {
                    DeleteAfterUse = true
                }
            };

            if (Headless)
            {
                firefoxOptions.AddArguments("--headless");
            }

            driver = new FirefoxDriver(FirefoxDriverService.CreateDefaultService(GeckoDriverPath), firefoxOptions, WebDriverTimeout);
            driver.Manage().Window.Maximize();
            objectContainer.RegisterInstanceAs<IWebDriver>(driver);
            return driver;
        }
    }
}
