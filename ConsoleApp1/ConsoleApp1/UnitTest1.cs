using NUnit.Framework;
using System.Text;
using OpenQA.Selenium;
using System.Threading.Tasks;
using System;
using OpenQA.Selenium.Interactions;

namespace IbsQA 
{
    class Tests : AssemblyBase
    {
        public Tests(string browser)
        {
            this.browser = browser;
        }

        [Test]
        public async Task Test1()
        {
            await Call();
            _driver.Url = "https://www-stage.ibselectronics.com/account/login/";
            _driver.FindElement(By.Id("Email_Email")).SendKeys("bob@bigzeta.com");
            _driver.FindElement(By.Id("Password_Password")).SendKeys("B0bR0ss!");
            _driver.FindElement(By.CssSelector("[type='submit']")).Click();
            Assert.AreEqual(_driver.Url, "https://www-stage.ibselectronics.com/account/");
            
            
        }

        [Test]
        public async Task Test2()
        {
            await Call();
            _driver.Url = "https://www-stage.ibselectronics.com/account/login/";
            _driver.FindElement(By.Id("Email_Email")).SendKeys("bob@bigzeta.com");
            _driver.FindElement(By.Id("Password_Password")).SendKeys("B0bR0ss!");
                
        }

        public Task Call()
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

            return _network.StartMonitoring();
        }
    }
}