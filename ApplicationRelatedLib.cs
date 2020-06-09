using CBApllications.PurchasingPageObjects;
using CBApllications.ScenarioLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CBApllications.GeneralLib
{
    class ApplicationRelatedLib
    {
        GenericLib generalLib = new GenericLib();
       
        /// <summary>
        /// It is dsigned to verify presence of topAppButtons (Dashboard, Logout and View basket buttontns)
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns></returns>
        //This method returns true if Dashboard, Log out and View basket button appears
        public bool verifyTopAppButtons(IWebDriver driver)
        {
            bool status = false;
            foreach (IWebElement wb in new CommonPageWebElements(driver).getTopAppBtns())
            {
                if (wb.Text.Contains("Dashboard") || wb.Text.Contains("Log Out")
                   || wb.Text.Contains("View Basket"))
                {
                    Console.WriteLine("-->" + wb.Text);
                    status = true;
                }
            }
            return status;
        }

        /// <summary>
        /// This method retrns username from app header
        /// </summary>
        /// <param name="driver"></param>
        /// <returns>Returns username in appHeaderDetails</returns>
        public String getUserName(IWebDriver driver)
        {
            String UN = generalLib.getTextFromElement(driver, new CommonPageWebElements(driver)
                .getAppHeaderUserName());
            String userName = UN.Split(':').Last().Trim();
            Console.WriteLine("userName--> " + userName);
            return userName;
        }

        /// <summary>
        /// This mehod verifies the presence of header element of the page like: Logo, mydetails section,
        /// Language and timezone dropdown etc.
        /// </summary>
        /// <param name="driver">Webdriver object to drive execution</param>
        //This method verifies the presence of header elements
        public void verifyPresenceOfPageHedearElements(IWebDriver driver)
        {
            CommonPageWebElements commonElements = new CommonPageWebElements(driver);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(generalLib.verifyPresenceOfElement(driver, commonElements.getLogoImg()),
                    "Logo is not appeared");

                Assert.IsTrue(generalLib.verifyPresenceOfElement(driver, commonElements.getMyDetailsLink()),
                    "My details link in header section is not appeared");

                Assert.IsTrue(generalLib.verifyPresenceOfElement(driver, commonElements.
                    getAppHeaderUserName()),  "Username in header section is not appeared");

                Assert.IsTrue(generalLib.verifyPresenceOfElement(driver, commonElements.
                    getAppHeaderCompanyName()), "Company Name in header section is not appeared");

                Assert.IsTrue(generalLib.verifyPresenceOfElement(driver, commonElements.getDashboardBtn()),
                    "Dashboard btn in header section is not appeared");

                Assert.IsTrue(generalLib.verifyPresenceOfElement(driver, commonElements.getLogoutBtn()),
                    "LogOut btn in header section is not appeared");

                Assert.IsTrue(generalLib.verifyPresenceOfElement(driver, commonElements.getViewBasketBtn()),
                    "View Basket btn in header section is not appeared");

                Assert.IsTrue(generalLib.verifyPresenceOfElement(driver, commonElements.getTimeZoneDropdown()),
                    "Timezone drpdown in header section is not appeared");

                Assert.IsTrue(generalLib.verifyPresenceOfElement(driver, commonElements.getLanguageSelectionDropdown()),
                    "language selection drpdown in header section is not appeared");
                Assert.IsTrue(generalLib.verifyPresenceOfElement(driver, commonElements.getMenuSwipeBtn()), "Menu " +
                    "Swipe button is not appeared");
            });
        }

        /// <summary>
        /// It returns true if download buttons (excel and csv) are available
        /// </summary>
        /// <param name="driver"></param>
        /// <returns>true if both buttons are present </returns>
        public bool verifyPresenceOfDownlodBtns(IWebDriver driver)
        {
            bool status = false;
            try
            { 
                if (new CommonPageWebElements(driver).getDownloadBtns().Count == 2)
                {
                    status = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return status;
        }

     
        /// <summary>
        /// It clicks the expand btn(+) for the linktext provided
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="linkTxt">The linktext for which expand btn need to click</param>
        public void clickOnExpandBtnForGivenLink(IWebDriver driver, String linkTxt)
        {
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    //clickOnLeftSideMenu(driver);
                    Console.WriteLine("------>" + linkTxt);
                    generalLib.clickOnWebElement(driver, new CommonPageWebElements(driver).
                        getMenuExpandBtnForLink(linkTxt));
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

        

        /// <summary>
        /// This method clicks on Left Hand side menu
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void clickOnMenuSwipeBtn(IWebDriver driver)
        {
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    new CommonPageWebElements(driver).getMenuSwipeBtn().Click();
                    generalLib.normalWait(2000);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

        public int getTableRowCount(IWebDriver driver)
        {
            int rowCount = 0;
            try
            {
                rowCount = new CommonPageWebElements(driver)
                    .getRowCollection().Count;
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and save in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return rowCount;
        }

        /// <summary>
        /// This method Verifies the breadcrumb
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void verifyBreadCrumbs(IWebDriver driver)
        {
            String url = driver.Url;

            foreach (IWebElement link in new CommonPageWebElements(driver).getListOfBrdCumbLinks())
            {
                String linkText = link.Text;
                Console.WriteLine("BrdCmb---->" + linkText);
                //link.Click();
                generalLib.waitForPageLoad(driver);
                if (!String.IsNullOrEmpty(link.GetAttribute("href")))
                {
                    Console.WriteLine("Href-->" + link.GetAttribute("href"));
                }
                else
                    Console.WriteLine("Breadcrumb link is null or empty -->" + link.GetAttribute("href"));
            }
        }


        /// <summary>
        /// This method is to verify the URL contains homepage.aspx or not
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public bool verifyNavigationToDashboardPage(IWebDriver driver)
        {
            bool status = false;
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    if (driver.Url.Contains("homepage.aspx"))
                    {
                        status = true;
                    }
                }
            }
            catch (Exception exp)
            {
                status = false;
                Console.WriteLine("`````Exception caught`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp);
            }
            return status;
        }

   
        /// <summary>
        /// This method verifies the left hand side menu if it is opened and returns true if 
        /// margin-left value is 0
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>Returns true if side menu is opened</returns>
        public bool verifyLeftSideMenuOpen(IWebDriver driver)
        {
            bool status = false;
            generalLib.normalWait(1000);
            String mrgLeft = new CommonPageWebElements(driver).getMenuAppSidebar()
                .GetCssValue("margin-left");
            Console.WriteLine("Margin Left : " + mrgLeft);
            if (mrgLeft.Equals("0px"))
            {
                status = true;
            }

            return status;
        }

        /// <summary>
        /// This method verifies the left hand side menu if it is cloesed and returns true if 
        /// margin-left value is -290
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns></returns>
        public bool verifyLeftSideMenuClose(IWebDriver driver)
        {
            bool status = false;
            generalLib.normalWait(1000);
            String mrgLeft = new CommonPageWebElements(driver).getMenuAppSidebar()
                .GetCssValue("margin-left");
            Console.WriteLine("Margin Left : " + mrgLeft);
            if (mrgLeft.Equals("-290px"))
            {
                status = true;
            }
            return status;
        }

        /// <summary>
        /// This method verifies the Navigation to a page by taking argument as URL content and
        /// Page header
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="urlcontent">Url content means the portion of url which identifies to
        /// the page</param>
        /// <param name="pageHeader">Page header text to verify with actual page header</param>
        /// <returns></returns>
        public bool verifyNavigationToPage(IWebDriver driver, String urlcontent,
            String pageHeader)
        {
            bool status = false;
            generalLib.waitForPageLoad(driver);
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                try
                {
                    if (driver.Url.Contains(urlcontent))
                    {
                        if (generalLib.getTextFromElement(driver, new CommonPageWebElements(driver)
                            .getPageHeader()).Equals(pageHeader))
                        {
                            status = true;
                        }
                    }

                }
                catch (Exception exp)
                {
                    Console.WriteLine("`````Exception caught`````");
                    generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                    generalLib.PrintAllLogs(driver);
                    Console.WriteLine(exp.Message);
                }
            }
            return status;
        }

        /// <summary>
        /// This method returns Order No from Order refs field text in order display page
        /// </summary>
        /// <param name="ordRefsText"></param>
        /// <returns></returns>
        public String getOrderIdFromDisplayOrdPage(String ordRefsText)
        {
            String ordId = "";
            String[] ordRefsTextArr = ordRefsText.Split(':');
            if (ordRefsTextArr[1].Contains("\r"))
            {
                String[] ordRefsTextArr1 = ordRefsTextArr[1].Split('\r');
                ordId = ordRefsTextArr1[0];
                Console.WriteLine(".....................order no -->" + ordId);
                return ordRefsTextArr1[0].Trim();
            }
            else
            {
                String[] ordRefsTextArr1 = ordRefsTextArr[1].Split('\n');
                ordId = ordRefsTextArr1[0];
                Console.WriteLine(".....................order no -->" + ordId);
                return ordRefsTextArr1[0].Trim();
            }
        }
        /// <summary>
        /// This method returns Purchase order Id from Order refs field text in order display page
        /// </summary>
        /// <param name="ordRefsText"></param>
        /// <returns></returns>
        public String getPurchaseIdFromDisplayOrdPage(String ordRefsText)
        {
            String[] ordRefsTextArr = ordRefsText.Split(':');
            Console.WriteLine("->->->->PurchaseId = " + ordRefsTextArr[ordRefsTextArr.Length - 1]);
            return ordRefsTextArr[ordRefsTextArr.Length - 1].Trim();
        }



       

       

        /// <summary>
        /// This method clicks on Add To Basket btn and then clicks on Proceed btn
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void clickOnAddToBasketBtnInFabOrdPage(IWebDriver driver)
        {
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                try
                {
                    generalLib.clickOnWebElement(driver, new FavouritesOrdersPage(driver)
                        .getFavOrdPageAddToBasketBtn());
                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                        generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                    {
                       AddTobasketPage addToBsktPageElements =  new AddTobasketPage(driver);
                        if (generalLib.verifyPresenceOfElement(driver, addToBsktPageElements.getProceedBtn()))
                        {
                            generalLib.clickOnWebElement(driver, addToBsktPageElements.getProceedBtn());
                        }
                    }
                }
                catch (Exception exp)
                {
                    Console.WriteLine("`````Exception caught`````");
                    generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                    generalLib.PrintAllLogs(driver);
                    Console.WriteLine(exp.Message);
                }
            }
        }

        /// <summary>
        /// This method verifies actual success message text apeared in successMessage div with
        /// expected success msg text
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="expectedSuccessMsg">Expected success message text to compare with actual
        /// success message text </param>
        /// <returns>It returns true if expected success message text is equals with actual text</returns>
        public bool verifySuccessMessage(IWebDriver driver, String expectedSuccessMsg)
        {
            bool status = false;
            generalLib.waitForPageLoad(driver);
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                MessageCollectionPage msgCollectionElements = new MessageCollectionPage(driver);
                try
                {
                    if(msgCollectionElements.getSuccessMsgDiv().Displayed)
                    {
                        Console.WriteLine("Success Message : "+ generalLib.getTextFromElement(driver,
                            msgCollectionElements.getSuccessMsgText()));
                        if (expectedSuccessMsg.Equals(generalLib.getTextFromElement(driver,
                            msgCollectionElements.getSuccessMsgText())))
                        {
                            status = true;
                        }
                    }
                    
                }
                catch (Exception exp)
                {
                    Console.WriteLine("`````Exception caught`````");
                    generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                    generalLib.PrintAllLogs(driver);
                    Console.WriteLine(exp.Message);
                }
            }
                return status;
        }
    }
}
