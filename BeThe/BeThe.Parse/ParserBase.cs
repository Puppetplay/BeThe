using System;
using HtmlAgilityPack;

namespace BeThe.Parse
{
    public class ParserBase
    {
        #region Abstract Functions

        protected String GetInnerHtml(HtmlNode node, String className)
        {
            HtmlNode selectedNode = node.SelectSingleNode(String.Format("td [@class='{0}']", className));
            if(selectedNode == null || String.IsNullOrEmpty(selectedNode.InnerHtml.Trim()))
            { 
                return null; 
            }
            else
            {
                return selectedNode.InnerHtml.Trim();
            }
        }

        protected String GetInnerHtmlFromPath(HtmlNode node, String path)
        {
            HtmlNode selectedNode = node.SelectSingleNode(path);
            if (selectedNode == null || String.IsNullOrEmpty(selectedNode.InnerHtml.Trim()))
            {
                return null;
            }
            else
            {
                return selectedNode.InnerHtml.Trim();
            }
        }

        protected String ConvertTeam(String teamName)
        {
            switch (teamName)
            {
                case "삼성": return "SS";
                case "NC": return "NC";
                case "두산": return "OB";
                case "넥센": return "WO";
                case "한화": return "HH";
                case "KIA": return "HT";
                case "SK": return "SK";
                case "롯데": return "LT";
                case "LG": return "LG";
                case "KT": return "KT";
                default: return teamName;
            }
        }

        #endregion
    }
}
