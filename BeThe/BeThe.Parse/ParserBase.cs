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

        #endregion
    }
}
