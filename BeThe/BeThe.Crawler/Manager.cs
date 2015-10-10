using System;
using System.Collections.Generic;
using System.Drawing;
using BeThe.Items;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.Linq;

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

        // Player_W 정보 얻기
        public List<Player_W> GetPlayer_W(String teamName)
        {
            try
            {
                InitCromeDriver();
                String teamInitial = Util.Util.ConvertTeam(teamName);
                List<Player_W> players = new List<Player_W>();
                for (Int32 i = 1; i < 7; ++i)
                {
                    CrawlerPlayer_W crawler = new CrawlerPlayer_W(chromeDriver);
                    crawler.Init(teamName, i);
                    String html = crawler.GetHTML();
                    if (html != null)
                    {
                        players = players.Concat(BeThe.Parse.Manager.Instance.ParsePlayer_W(html, teamInitial)).ToList();
                    }
                }
                return players;
            }
            finally
            {
                DisposeDriver();
            }
           
        }

        // Player 정보 얻기
        public Player GetPlayer(Player_W player_W)
        {
            InitCromeDriver();
            CrawlerPlayer crawler = new CrawlerPlayer(chromeDriver);
            crawler.Init(player_W.Href);
            
            String html = crawler.GetHTML();

            String[] items = player_W.Href.Split(new String[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
            Int32 playerId = Convert.ToInt32(items[items.Length - 1]);
            var player = BeThe.Parse.Manager.Instance.ParsePlayer(html, player_W.Team, playerId);
            return player;
        }

        public void Dispose()
        {
            DisposeDriver();
        }

        private void DisposeDriver()
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
