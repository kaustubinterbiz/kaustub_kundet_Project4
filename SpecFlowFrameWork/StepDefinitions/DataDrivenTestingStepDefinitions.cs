using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowFrameWork.HelperClass;
using SpecFlowProject1.Utility;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowFrameWork.StepDefinitions
{
    [Binding]
    public class DataDrivenTestingStepDefinitions
    {
        private IWebDriver _driver;
        private WebdriverWaitClass _waitHelper;
        private IJavaScriptExecutor jsExecutor;
        private static JSonReader _jsonReader = new JSonReader();

        public DataDrivenTestingStepDefinitions(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"Entered a valid ""(.*)"" and ""(.*)""")]
        public void GivenIHaveEnteredAValid(string Username, string Password)
        {
            Console.WriteLine($"I have entered a valid {Username} and {Password}");
            Thread.Sleep(5000);
            IWebElement username = _driver.FindElement(By.XPath("//*[@id='username']"));
            IWebElement password = _driver.FindElement(By.XPath("//*[@id='password']"));

            try
            {
                for(int i=0; i<=10; i++)
                {
                    if (username.Displayed)
                    {
                        username.SendKeys(Username + Keys.Tab);
                        password.SendKeys(Password + Keys.Tab);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            
             

            jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", username);
        }

        [Given(@"Enter the Credentials")]
        public void GivenEnterTheCredentials()
        {
            //_jsonReader.ReadJsonFile();
            var User = _jsonReader.TestDataURL("Username");
            var Passwd = _jsonReader.TestDataURL("Password");

            Console.WriteLine($"I have entered a valid {User} and {Passwd}");
            Thread.Sleep(5000);
            IWebElement username = _driver.FindElement(By.XPath("//*[@id='username']"));
            IWebElement password = _driver.FindElement(By.XPath("//*[@id='password']"));

            try
            {
                for (int i = 0; i <= 10; i++)
                {
                    if (username.Displayed)
                    {
                        username.SendKeys(User + Keys.Tab);
                        password.SendKeys(Passwd + Keys.Tab);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }



            jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", username);
        }

    }
}
