using System;
using System.Collections.Generic;
using BeThe.Items;
using HtmlAgilityPack;

namespace BeThe.Parse
{
    internal class ParserPlayer_W : ParserBase
    {
        #region Singleton

        private ParserPlayer_W()
        {

        }

        public static ParserPlayer_W Instance
        {
            get { return Nested.instance; }

        }

        private class Nested
        {
            static Nested()
            {
            }
            internal static readonly ParserPlayer_W instance = new ParserPlayer_W();
        }

        #endregion

        #region Public Functions

        public List<Player_W> Parse(String html, String team)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("//tbody//tr");
            List<Player_W> players = new List<Player_W>();
            foreach (var node in nodes)
            {
                var items = node.SelectNodes("td");

                Player_W player = new Player_W
                {
                    Team = team,
                    Href = items[1].SelectSingleNode("a").GetAttributeValue("href", ""),
                };
                players.Add(player);
            }

            return players;
        }

        #endregion

        #region Private Functions

        // Player 얻기
        private Player CreatePlayer_FromNode(HtmlNode node)
        {
            String temp = node.InnerHtml;
            try
            {
                return new Player
                {
                    
                };
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        private Int32? GetBackNumber(String number)
        {
            Int32 temp = 0;
            if(Int32.TryParse(number, out temp))
            {
                return temp;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
