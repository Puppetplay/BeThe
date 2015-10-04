using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeThe.Items;
using HtmlAgilityPack;

namespace BeThe.Parse
{
    internal class ParserShedule_W : ParserBase
    {
        #region Singleton

        private ParserShedule_W()
        {
            
        }

        public static ParserShedule_W Instance
        {
            get { return Nested.instance; }

        }

        private class Nested
        {
            static Nested()
            {
            }
            internal static readonly ParserShedule_W instance = new ParserShedule_W();
        }

        #endregion

        #region Public Functions

        public List<Schedule_W> Parse(String html, Int32 year, Int32 month)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("//tbody//tr");
            List<Schedule_W> schedule_Ws = new List<Schedule_W>();

            Int32 currentMonth = 0;
            Int32 currrntDay = 0;
            foreach (var node in nodes)
            {
                String day = GetDay(node);
                if (day != null)
                {
                    currentMonth = Convert.ToInt32(day.Substring(0, 2));
                    currrntDay = Convert.ToInt32(day.Substring(3, 2));
                    if (currentMonth != month)
                    {
                        throw new Exception("페이지가 완전히 로드되지 않았습니다.");
                    }
                }

                if (currentMonth != 0 && currrntDay != 0)
                {
                    Schedule_W schedule_W = CreateSchedule_WFromNode(node, year, currentMonth, currrntDay);
                    if (schedule_W != null)
                    {
                        schedule_Ws.Add(schedule_W);
                    }
                }
            }
            return schedule_Ws;

        }

        #endregion

        #region Private Functions

        private Schedule_W CreateSchedule_WFromNode(HtmlNode node, Int32 year, Int32 month, Int32 day)
        {
            try
            {
                if (GetInnerHtml(node, "none") != null)
                {
                    return null;
                }
                
               return new Schedule_W
               {
                   Year = year,
                   Month = month,
                   Day = day,
                   Time = GetInnerHtml(node, "time"),
                   Play = GetInnerHtml(node, "play"),
                   Relay = GetInnerHtml(node, "relay"),
                   BallPark = GetInnerHtml(node, "ballpark"),
                   Etc = GetInnerHtml(node, "etc"),
               };
            }
            catch(Exception exception)
            {
                throw exception;
            }
                   
        }

        private String GetDay(HtmlNode node)
        {
            return GetInnerHtmlFromPath(node, "td [@class='day']//a//span");
        }


        #endregion
    }
}
