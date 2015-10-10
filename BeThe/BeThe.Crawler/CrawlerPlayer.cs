using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BeThe.Crawler
{
    internal class CrawlerPlayer : CrawlerBase
    {
        #region Property & Values

        private readonly String URL = "http://www.koreabaseball.com";
        private String address;

        #endregion

        #region Constructor

        public CrawlerPlayer(ChromeDriver chromeDriver)
            : base(chromeDriver)
        {

        }

        #endregion

        #region public Functions

        public void Init(String href)
        {
            address = (URL + href);
        }

        public override String GetHTML()
        {
            driver.Navigate().GoToUrl(address);
            Sleep(1000);
            try
            {
                return base.GetHTML();
            }
            catch
            {
                throw new OpenQA.Selenium.StaleElementReferenceException("페이지 로드오류");
            }
        }

        #endregion
    }
}
