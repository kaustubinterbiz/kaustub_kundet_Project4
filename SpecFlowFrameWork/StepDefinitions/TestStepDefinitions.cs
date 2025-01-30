using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;
using SpecFlowFrameWork.HelperClass;
using NUnit.Framework;

namespace SpecFlowFrameWork.StepDefinitions
{
    [Binding]
    public class TestStepDefinitions
    {
        private readonly IWebDriver _driver;
        private WebdriverWaitClass _waitHelper;
        private IJavaScriptExecutor jsExecutor;
        public string actualTxt = "You logged into a secure area!";
        public string invalidLoginActualTxt = "Your password is invalid!";
        public TestStepDefinitions(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"I have entered a valid username and password")]
        public void GivenIHaveEnteredAValidUsernameAndPassword()
        {
            Console.WriteLine("I have entered a valid username and password");
            Thread.Sleep(5000);
            IWebElement username = _driver.FindElement(By.Id("username"));
            IWebElement password = _driver.FindElement(By.Id("password"));

            IWebElement usernameTxt = _driver.FindElement(By.XPath("//*[text()='Username: ']/b"));
            IWebElement passwordTxt = _driver.FindElement(By.XPath("//*[text()='Password: ']/b"));

            string Uname_Value = _driver.FindElement(By.XPath("//*[text()='Username: ']/b")).Text;
            string pwd_Value = _driver.FindElement(By.XPath("//*[text()='Password: ']/b")).Text;
            Console.WriteLine($" Username {Uname_Value} and Password {pwd_Value}");

            username.SendKeys(Uname_Value+Keys.Tab);
            password.SendKeys(pwd_Value+Keys.Tab);
          
            jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", username);
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            Console.WriteLine("I click the login button");
            Thread.Sleep(5000);
            IWebElement loginBtn = _driver.FindElement(By.XPath("//*[text()='Login']"));
            loginBtn.Click();

            IWebElement validateTxt = _driver.FindElement(By.XPath("//*[@id='flash']/b"));
            string expectedTxt = validateTxt.Text;
            
        }

[Then(@"Validation of Login Credentials and User Access")]
public void ThenValidateTheLoginIsWorking()
{
    IWebElement validateTxt = _driver.FindElement(By.XPath("//*[@id='flash']/b"));
    string expectedTxt = validateTxt.Text;
    for (int i = 0; i < 10; i++)
    {
        if (invalidLoginActualTxt.Equals(expectedTxt))
        { 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Not correct Login credential");
            string currentUrl = _driver.Url;
            if (currentUrl.Contains("https://practice.expandtesting.com/secure"))
            { 
                Console.WriteLine("Login page is still showing. Test failed.");
                Assert.Fail("Login page was displayed with invalid credentials.");
            }
            break;
        }
        else if (actualTxt.Equals(expectedTxt))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Correct credential");
            Console.WriteLine(" Validate succesfully! ");
            break;
        }

    }
}

        [Then(@"I should be redirected to the dashboard")]
        public void ThenIShouldBeRedirectedToTheDashboard()
        {
            Console.WriteLine("I should be redirected to the dashboard");
              Thread.Sleep(5000);
            _waitHelper = new WebdriverWaitClass(_driver);
            By element = By.XPath("//*[text()=' Logout']");
            _waitHelper.WaitTillVisibilityOfTheElement(element);
            _driver.FindElement(element).Click();
           
             
        }
    }
}
