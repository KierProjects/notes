using NUnit.Framework;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;
using System;

namespace TestProject
{
    public class Tests
    {
        protected string browser = null!;
        protected string baseUrl = "https://www-stage.ibselectronics.com/";
        protected IWebDriver _driver = null!;
        protected INetwork _network = null!;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            _driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(180));
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(30));
            _network = _driver.Manage().Network;
        }

        [Test]
        public async void Test1()
        {
            await call();
            await _network.StartMonitoring();
            Task.Run(() => this.Test1()).Wait();
            _driver.Url = "https://www-stage.ibselectronics.com/account/login/";
            _driver.FindElement(By.Id("Email_Email")).SendKeys("bob@bigzeta.com");
                _driver.FindElement(By.Id("Password_Password")).SendKeys("B0bR0ss!");
            _driver.FindElement(By.CssSelector("[type='submit']")).Click();
            
        }

        public async Task call()
        {
            _network.AddRequestHandler(new NetworkRequestHandler
            {
                RequestMatcher = data => data.Url.Contains("www-stage.ibselectronics.com"),
                RequestTransformer = data =>
                {
                    if (data.Method == "POST")
                    {
                        var plainTextBytes = Encoding.UTF8.GetBytes(data.PostData);
                        var base64String = Convert.ToBase64String(plainTextBytes);
                        data.PostData = base64String;
                    }

                    data.Headers["CF-Access-Client-Id"] = "76d1e45b7ac5bcbeae810cdf65c0e129.access";
                    data.Headers["CF-Access-Client-Secret"] = "d68dfeb72eb7f92e8d75860c1fb5ab25400c42da7c15c0e77e56c1372814c242";
                    return data;
                },
                ResponseSupplier = null
            });
            await _network.StartMonitoring();
        }
    }
}