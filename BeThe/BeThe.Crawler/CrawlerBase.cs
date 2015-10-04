using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BeThe.Crawler
{
    abstract class CrawlerBase
    {
        #region Property & Values

        protected ChromeDriver driver;

        #endregion

        #region Constructor

        public CrawlerBase(ChromeDriver chromeDriver)
        {
            this.driver = chromeDriver;
        }

        #endregion

        #region Abstract Functions

        public virtual String GetHTML()
        {
            try
            {
                return driver.FindElementByXPath("//HTML").GetAttribute("outerHTML");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Protected Functions

        protected void SetComboBox(String id, String value)
        {
            var combo = driver.FindElementById(id);
            var options = combo.FindElements(By.TagName("option"));
            var option = (from o in options
                          where o.Text == value
                          select o).First();
            if (option == null)
            {
                throw new Exception();
            }
            else
            {
                option.Click();
            }
        }

        protected void ClickButton(String id)
        {
            var nextButton = driver.FindElementById(id);
            nextButton.Click();
        }

        protected void Sleep(Int32 tick)
        {
            System.Threading.Thread.Sleep(tick);
        }

        #endregion

    }
}
