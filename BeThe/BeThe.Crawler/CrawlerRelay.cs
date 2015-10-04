using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BeThe.Crawler
{
    internal class CrawlerRelay : CrawlerBase
    {
        #region Property & Values

        private String itemGameId;
        private String itemDate;
        private readonly String BASE_URL = "http://sports.news.naver.com/gameCenter/miniTextRelay.nhn?category=kbo&date={0}&gameId={1}";

        #endregion

        #region Constructor

        public CrawlerRelay(ChromeDriver chromeDriver)
            : base(chromeDriver)
        {
        }

        #endregion

        #region public Functions

        public void Init(String gameId)
        {
            itemGameId = gameId;
            itemDate = gameId.Substring(0, 8);
        }

        public override String GetHTML()
        {
            String url = String.Format(BASE_URL, itemDate, itemGameId);
            driver.Navigate().GoToUrl(url);

            Sleep(100);
            try
            {
                ClickButton("inning_tab_all");
                Sleep(2000);
                return driver.FindElementById("relay_text").GetAttribute("outerHTML");
            }
            catch
            {
                throw new OpenQA.Selenium.StaleElementReferenceException("페이지 로드오류 GAMEID:" + itemGameId);
            }
        }
        
        #endregion
    }
}
