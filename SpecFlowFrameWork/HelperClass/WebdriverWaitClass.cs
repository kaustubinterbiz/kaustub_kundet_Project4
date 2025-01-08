using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace SpecFlowFrameWork.HelperClass
{
    public class WebdriverWaitClass
    {
        private const int TimeOutInSeconds = 60;  
        private readonly WebDriverWait _wait;   
        private readonly IWebDriver _driver;

       
        public WebdriverWaitClass(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(TimeOutInSeconds));
        }

       
        public void WaitTillVisibilityOfTheElement(By element)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(element));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public void ElementToBeClickable(By element)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void TextPresentInElement(IWebElement element, string text)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, text));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void ElementIsExist(By element)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(element));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
