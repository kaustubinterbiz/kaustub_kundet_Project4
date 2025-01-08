using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace SpecFlowFrameWork.HelperClass
{
    public class SeleniumActionHelper
    {
        private IWebDriver _driver;
        private Actions _action;
        public SeleniumActionHelper(IWebDriver driver)
        {
            _driver = driver;
            _action = new Actions(_driver);
        }

        public void elementClickAndHold(IWebElement element)
        {
            try
            {
                _action.ClickAndHold(element).Build().Perform();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public void elementDoubleClick()
        {
            try
            {
                _action.DoubleClick().Build().Perform();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void elementDragAndDrop(IWebElement sourceElement, IWebElement targetElement) 
        {
            try
            {
                _action.DragAndDrop(sourceElement, targetElement).Build().Perform();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public void moveToElement(IWebElement element)
        {
            try
            {
                _action.MoveToElement(element).Build().Perform();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void contextClick(IWebElement element)
        {
            try
            {
                _action.ContextClick(element).Build().Perform();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
