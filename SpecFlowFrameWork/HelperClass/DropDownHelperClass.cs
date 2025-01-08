using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowFrameWork.HelperClass
{
    public class DropDownHelperClass
    {
        private IWebDriver _driver;
        private SelectElement _element;
       
        public DropDownHelperClass(IWebDriver driver) 
        {
              _driver = driver;
             
        }

        public void drpDwnSelectByIndex(IWebElement element, int index) 
        {

            _element = new SelectElement(element);
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    _element.SelectByIndex(index);
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void drpDwnSelectByText(IWebElement element, string value)
        {

            _element = new SelectElement(element);
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    _element.SelectByText(value);
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void drpDwnSelectByValue(IWebElement element, string value)
        {

            _element = new SelectElement(element);
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    _element.SelectByValue(value);
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


    }
}
