using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;
using WebDriverManager.Clients;
using WebDriverManager.DriverConfigs;
using WebDriverManager.Helpers;
using WebDriverManager.Services.Impl;
using WebDriverManager.Services;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace TFLInterviewScenarios.BrowserFactories
{
    internal class ChromeBrowserFactory
    {
        public string ChromeDriverPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public TimeSpan WebDriverTimeout { get; set; } = TimeSpan.FromSeconds(60);
        public bool Incognito { get; set; } = false;
        public bool Headless { get; set; } = false;
        public bool DisableGpu { get; set; } = false;
        public bool NoSandBox { get; set; } = false;
        public bool StartMaximised { get; set; } = true;
        public IEnumerable<string> AdditionalArguments { get; set; } = new List<string>();
        public IDictionary<string, string> AdditionalUserProfilePreferences { get; set; } = new Dictionary<string, string>();
        private readonly IObjectContainer objectContainer;
        public string DownloadsFolderPath { get; set; } = null;

        public ChromeBrowserFactory(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        public IWebDriver Create(IObjectContainer objectContainer)
        {
            IWebDriver driver;
            var options = new ChromeOptions();
            var arguments = new List<string>();

            if (!string.IsNullOrEmpty(DownloadsFolderPath))
            {
                options.AddUserProfilePreference("download.default_directory", DownloadsFolderPath);
            }

            if (Incognito)
            {
                arguments.Add("incognito");
            }


            if (StartMaximised)
            {
                arguments.Add("start-maximized");
            }

            if (Headless)
            {
                arguments.Add("headless");
            }

            if (DisableGpu)
            {
                arguments.Add("disable-gpu");
            }

            if (NoSandBox)
            {
                arguments.Add("no-sandbox");
            }

            if (AdditionalArguments != null)
            {
                arguments = arguments.Union(AdditionalArguments).ToList();
            }

            options.AddArguments(arguments);

            if (AdditionalUserProfilePreferences != null)
            {
                foreach (var additionalUserProfilePreference in AdditionalUserProfilePreferences)
                {
                    options.AddUserProfilePreference(additionalUserProfilePreference.Key,
                        additionalUserProfilePreference.Value);
                }
            }

            options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            options.LeaveBrowserRunning = false;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string path = new DriverManager().SetUpDriver(new ChromeConfig());
            string driverPath = new string(path.Take(path.Length - 16).ToArray());

            var chromeDriverService = ChromeDriverService.CreateDefaultService(driverPath);
            chromeDriverService.HideCommandPromptWindow = true;

            driver = new ChromeDriver(chromeDriverService, options, WebDriverTimeout);
            objectContainer.RegisterInstanceAs<IWebDriver>(driver);
            return driver;
        }
    }

    public class DriverManager
    {


        private static readonly object Object = new object();


        private IBinaryService _binaryService;
        private readonly IVariableService _variableService;
        private string _downloadDirectory = Directory.GetCurrentDirectory();


        public DriverManager()
        {
            _binaryService = new BinaryService();
            _variableService = new VariableService();
        }


        public DriverManager(string downloadDirectory) : this()
        {
            _downloadDirectory = downloadDirectory;
        }


        public DriverManager(IBinaryService binaryService, IVariableService variableService)
        {
            _binaryService = binaryService;
            _variableService = variableService;
        }


        public DriverManager WithProxy(IWebProxy proxy)
        {
            _binaryService = new BinaryService { Proxy = proxy };
            ChromeForTestingClient.Proxy = proxy;
            WebRequest.DefaultWebProxy = proxy;
            return this;
        }



        public string SetUpDriver(string url, string binaryPath)
        {
            lock (Object)
            {
                return SetUpDriverImpl(url, binaryPath);
            }
        }


        public string SetUpDriver(IDriverConfig config, string version = VersionResolveStrategy.MatchingBrowser,
            WebDriverManager.Helpers.Architecture architecture = WebDriverManager.Helpers.Architecture.Auto)
        {
            lock (Object)
            {
                architecture = architecture.Equals(WebDriverManager.Helpers.Architecture.Auto)
                    ? ArchitectureHelper.GetArchitecture()
                    : architecture;
                version = GetVersionToDownload(config, version);
                var url = architecture.Equals(WebDriverManager.Helpers.Architecture.X32) ? config.GetUrl32() : config.GetUrl64();
                url = UrlHelper.BuildUrl(url, version);
                var binaryPath = Path.Combine(_downloadDirectory, config.GetName(), version, architecture.ToString(), config.GetBinaryName());
                return SetUpDriverImpl(url, binaryPath);
            }
        }


        private string SetUpDriverImpl(string url, string binaryPath)
        {
            var zipPath = WebDriverManager.Helpers.FileHelper.GetZipDestination(url);
            binaryPath = _binaryService.SetupBinary(url, zipPath, binaryPath);
            _variableService.SetupVariable(binaryPath);
            return binaryPath;
        }


        private static string GetVersionToDownload(IDriverConfig config, string version)
        {
            switch (version)
            {
                case VersionResolveStrategy.MatchingBrowser: return config.GetMatchingBrowserVersion();
                case VersionResolveStrategy.Latest: return config.GetLatestVersion();
                default: return version;
            }
        }
    }
}
