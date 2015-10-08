using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BeThe.Crawler
{
    internal class CrawlerPlayer: CrawlerBase
    {
        #region Property & Values

        private String m_Team;
        private Int32 m_Page;
        private readonly String URL = "http://www.koreabaseball.com/Player/Search.aspx";

        #endregion

        #region Constructor

        public CrawlerPlayer(ChromeDriver chromeDriver)
            : base(chromeDriver)
        {

        }

        #endregion

        #region public Functions

        public void Init(String teamName, Int32 page)
        {
            m_Team = teamName;
            m_Page = page;
        }

        public override String GetHTML()
        {
            driver.Navigate().GoToUrl(URL);
            Sleep(1000);
            try
            {
                SetComboBox("cphContainer_cphContents_ddlTeam", m_Team);
                Sleep(2000);

                if (m_Page > 5)
                {
                    // 버튼이 있는지 확인한후 클릭한다.
                    // 다음페이지로 넘기기
                    ClickButton("cphContainer_cphContents_ucPager_btnNext");
                    Sleep(2000);
                }
                String buttonId = String.Format("cphContainer_cphContents_ucPager_btnNo{0}", m_Page);
                ClickButton(buttonId);

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
