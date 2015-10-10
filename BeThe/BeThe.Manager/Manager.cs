using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeThe.Items;

namespace BeThe.Manager
{
    public class Manager
    {
        public void SelectAllSchedule()
        {
            // 일정불러오기 
            DateTime endDate = DateTime.Now;

            var crawlerMgr = BeThe.Crawler.Manager.Instance;
            var dbMgr = new BeThe.DataBase.Manager();

            var allSchedule = dbMgr.SelectAll<Schedule>();
            Int32 year = (from schedule in allSchedule select schedule.Year).Max();
            Int32 month = (from schedule in allSchedule where schedule.Year == year select schedule.Month).Max();
            DateTime startDate = new DateTime(year, month, 1);

            try
            {
                Int32 errorCount = 0;
                while (startDate <= endDate)
                {
                    try
                    {
                        var data = crawlerMgr.GetSchedule(startDate.Year, startDate.Month);

                        var scheduleTable = dbMgr.DataContext.GetTable<Schedule>();
                        var delSchedule = from schedule in allSchedule
                                          where schedule.Year == startDate.Year && schedule.Month == startDate.Month
                                          select schedule;

                        foreach (var schedule in delSchedule)
                        {
                            scheduleTable.DeleteOnSubmit(schedule);
                        }
                        dbMgr.DataContext.SubmitChanges();

                        dbMgr.Save(data);
                        startDate = startDate.AddMonths(1);
                        errorCount = 0;
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException exception)
                    {
                        if (errorCount < 10)
                        {
                            errorCount++;
                            continue;
                        }
                        else
                        {
                            throw exception;
                        }
                    }
                }
            }
            finally
            {
                crawlerMgr.Dispose();
            }
        }

        public void SelectRelay()
        {
            var crawlerMgr = BeThe.Crawler.Manager.Instance;
            var dbMgr = new BeThe.DataBase.Manager();

            var dataContext = dbMgr.DataContext;
            var querySchedule = dbMgr.SelectAll<Schedule>();
            var queryRelay = dbMgr.SelectAll<Relay_W>();

            var relayTable = dataContext.GetTable<Relay_W>();

            var schedules =
                    (from schedule in querySchedule
                     join relay in queryRelay
                     on schedule.GameId equals relay.GameId into ps
                     from relay in ps.DefaultIfEmpty()
                     where relay.Content == null
                     select schedule).ToList();

            try
            {
                foreach (var s in schedules)
                {
                    if (s.GameId == null) { continue; }

                    if (s.HomeTeam == "SS" || s.HomeTeam == "NC" || s.HomeTeam == "OB" || s.HomeTeam == "WO" || s.HomeTeam == "HH" ||
                        s.HomeTeam == "HT" || s.HomeTeam == "SK" || s.HomeTeam == "LT" || s.HomeTeam == "LG" || s.HomeTeam == "KT")
                    {
                        Boolean isSucess = false;

                        Int32 errorCount = 0;
                        while (isSucess == false)
                        {
                            try
                            {
                                Relay_W relay_W = new Relay_W();
                                relay_W.GameId = s.GameId;
                                relay_W.Content = crawlerMgr.GetRelay(s.GameId);
                                relayTable.InsertOnSubmit(relay_W);
                                isSucess = true;
                                dbMgr.Submit();
                                errorCount = 0;
                            }
                            catch (OpenQA.Selenium.StaleElementReferenceException exception)
                            {
                                if (errorCount < 10)
                                {
                                    errorCount++;
                                    continue;
                                }
                                else
                                {
                                    throw exception;
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                crawlerMgr.Dispose();
            }
        }

        public void SelectPlayer_W()
        {
            var crawlerMgr = BeThe.Crawler.Manager.Instance;
            var dbMgr = new BeThe.DataBase.Manager();
           
            var allPlayers = dbMgr.SelectAll<Player_W>();

            // 기존데이터 삭제
            var playerTable = dbMgr.DataContext.GetTable<Player_W>();
            foreach (var player in allPlayers)
            {
                playerTable.DeleteOnSubmit(player);
            }

            foreach(String team in Util.Util.Teams)
            {
                Int32 errorCount = 0;
                Boolean isError = true;
                String teamName = team;
                if(teamName == "KT")
                {
                    teamName = "kt";
                }
                while (isError)
                {
                    try
                    {
                        var players = crawlerMgr.GetPlayer_W(teamName);
                        dbMgr.Save(players);
                        isError = false;
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException exception)
                    {
                        if (errorCount < 10)
                        {
                            errorCount++;
                            continue;
                        }
                        else
                        {
                            throw exception;
                        }
                    }
                }

            }
        }

        public void SelectPlayer()
        {
            var crawlerMgr = BeThe.Crawler.Manager.Instance;
            var dbMgr = new BeThe.DataBase.Manager();
            var allPlayers_W = dbMgr.SelectAll<Player_W>();
            var allPlayers = dbMgr.SelectAll<Player>();

            // 기존데이터 삭제
            var playerTable = dbMgr.DataContext.GetTable<Player>();
            foreach (var player in allPlayers)
            {
                playerTable.DeleteOnSubmit(player);
            }

            List<Player> players = new List<Player>();
            foreach(var player_W in allPlayers_W)
            {
                Int32 errorCount = 0;
                Boolean isError = true;
                while (isError)
                {
                    try
                    {
                        var player = crawlerMgr.GetPlayer(player_W);
                        if(player != null)
                        {
                            players.Add(player);
                        }
                        isError = false;
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException exception)
                    {
                        if (errorCount < 10)
                        {
                            errorCount++;
                            continue;
                        }
                        else
                        {
                            throw exception;
                        }
                    }
                }
            }
            dbMgr.Save(players);
            crawlerMgr.Dispose();
        }
    }
}
