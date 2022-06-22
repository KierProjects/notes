using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;

namespace IbsQA
{
    // Run test case on all three browsers - Chrome, Edge, Firefox
    [NonParallelizable]
    [TestFixture(new object[] { "chrome" }, Category= "Chrome")]
    [TestFixture(new object[] { "edge" }, Category = "Edge")]
    [TestFixture(new object[] { "firefox" }, Category = "Firefox")]
    abstract class AssemblyBase
    {
        // Define webdriver variables
        protected string browser = null!;
        protected string baseUrl = "https://www-stage.ibselectronics.com/";
        protected IWebDriver _driver = null!;
        protected INetwork _network = null!;
        protected IDevTools _devTools = null!;

        // Generate webdriver for test
        [SetUp]
        public void Setup()
        {
            _driver = Driver.GenerateWebDriver(browser, "local");
            _network = _driver.Manage().Network;
        }
        // Quit webdriver after test is run
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
