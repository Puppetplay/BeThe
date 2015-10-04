using System;
using System.Collections.Generic;
using BeThe.Items;
using HtmlAgilityPack;

namespace BeThe.Parse
{
    public class Manager
    {
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

        public List<Schedule> ParseSchedule(String html, Int32 year, Int32 month)
        {
            return ParserShedule.Instance.Parse(html, year, month);
        }

        #endregion
    }
}
