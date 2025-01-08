using AventStack.ExtentReports;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecFlowFrameWork.Utility;
using SpecFlowProject1.Utility;

namespace SpecFlowProject1.Base
{
    public class BaseClass
    {
        public static JSonReader GetDataParser()
        {
            return new JSonReader();
        }



        public IWebDriver BrowserLaunch(IWebDriver driver, string? BrowserName = "chrome")
        {
            dynamic capability = GetBrowserOption(BrowserName);
            Console.WriteLine(capability);
            //capability.AddArgument("--headless=old");
            //capability.AddArgument("window-size=1700,900");
            //capability.AddArgument("window-size=1200,700");
            //forFileupload
            //capability.AddArgument("--disable-popup-blocking");
            //capability.AddArgument("--disable-extensions");
            //driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability.ToCapabilities());
            //driver = new RemoteWebDriver(new Uri("http://192.168.0.123:4444"), capability.ToCapabilities());
            if (BrowserName.ToLower().Contains("chrome"))
                driver = new ChromeDriver(capability);
            if (BrowserName.ToLower().Contains("firefox"))
                driver = new FirefoxDriver(capability);
            if (BrowserName.ToLower().Contains("edge"))
                driver = new EdgeDriver(capability);
            if (BrowserName.ToLower().Contains("safari"))
                driver = new SafariDriver(capability);

            
           

            return driver;
        }
        private dynamic GetBrowserOption(string BrowserName)
        {
            if (BrowserName.ToLower().Contains("firefox"))
                return new FirefoxOptions();
            if (BrowserName.ToLower().Contains("edge"))
                return new EdgeOptions();
            if (BrowserName.ToLower().Contains("safari"))
                return new SafariOptions();
            else
            {
                return new ChromeOptions();
            }
        }
        
    }
}
