using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;

namespace IbsQA
{
    sealed class Driver
    {
        public static IWebDriver GenerateWebDriver(string browser, string environment)
        {
            if (environment.Equals("local"))
            {
                switch (browser)
                {
                    case "chrome":
                        return GenerateChromeDriverLocal();
                    case "firefox":
                        return GenerateFirefoxDriverLocal();
                    case "edge":
                        return GenerateEdgeDriverLocal();
                    default:
                        throw new SystemException("No driver class provided.");
                }
            }
            else
            {
                throw new SystemException("Unidentified environment.");
            }
        }
        private static IWebDriver GenerateChromeDriverLocal()
        {
            IWebDriver driver;
            ChromeOptions options = new ChromeOptions();
            //options.AddArguments(new string[] { "--headless", "--window-size=1920,1080" });
            driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(180));
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(30));
            return driver;
        }
        private static IWebDriver GenerateFirefoxDriverLocal()
        {
            IWebDriver driver;
            Environment.SetEnvironmentVariable("MOZ_HEADLESS_WIDTH", "1920");
            Environment.SetEnvironmentVariable("MOZ_HEADLESS_HEIGHT", "1080");
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("--headless");
            options.AcceptInsecureCertificates = true;
            driver = new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(180));
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(30));
            return driver;
        }
        private static IWebDriver GenerateEdgeDriverLocal()
        {
            IWebDriver driver;
            EdgeOptions options = new EdgeOptions();
            options.AddArguments(new string[] { "--headless", "--window-size=1920,1080" });
            driver = new EdgeDriver(EdgeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(180));
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(30));
            return driver;
        }
    }
}
