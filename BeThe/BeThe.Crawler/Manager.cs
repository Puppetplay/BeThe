using System;
using System.Collections.Generic;
using System.Drawing;
using BeThe.Items;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;

namespace BeThe.Crawler
{
    public class Manager : IDisposable
    {
        #region Property & Values

        private ChromeDriver chromeDriver;

        #endregion

        #region Singleton

        private Manager()
        {
        }

        public static Manager Instance
        {
            get { return Nested.instance; }

        }

        private class Nested
        {
            static Nested()
            {
            }
            internal static readonly Manager instance = new Manager();
        }

        #endregion

        #region Public Functions

        // 스케줄 정보 얻기
        public List<Schedule> GetSchedule(Int32 year, Int32 month)
        {
            InitCromeDriver();
            CrawlerSchedule crawler = new CrawlerSchedule(chromeDriver);
            crawler.Init(year, month);
            String html = null;
            html = crawler.GetHTML();
            return BeThe.Parse.Manager.Instance.ParseSchedule(html, year, month);
        }

        // 문자중계정보 얻기
        public String GetRelay(String gameId)
        {
            InitCromeDriver();
            CrawlerRelay crawler = new CrawlerRelay(chromeDriver);
            crawler.Init(gameId);
            return crawler.GetHTML();
           
        }

        public void Dispose()
        {
            if (chromeDriver != null)
            {
                chromeDriver.Dispose();
                chromeDriver = null;
            }
        }


        #endregion

        #region Private Functions

        private void InitCromeDriver()
        {
            if (chromeDriver == null)
            {
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                var chromeOptions = new ChromeOptions();
                chromeDriverService.HideCommandPromptWindow = true;
                chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);
                chromeDriver.Manage().Window.Size = new Size(0, 0);
            }
        }

        #endregion
    }
}
