using CBApllications.GeneralLib;
using CBApllications.PurchasingPageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CBApllications
{
    class HomePageScn
    {
        GenericLib generalLib = new GenericLib();
        public void logOutFromPurchasing(IWebDriver driver)
        {
            PurchasingHomePage homePage = new PurchasingHomePage(driver);
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    homePage.logOutBtn.Click();                   
                    Console.WriteLine("------------------Logged out from application--------");
                }
                generalLib.waitForPageLoad(driver);
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
        /// This method enters keyword in Keyword search textbox and clicks on search button
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="keyword">Keyword to search</param>
        public void keywordSearchFromDasboard(IWebDriver driver, String keyword)
        {
            HomePage homePageElements = new HomePage(driver);
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    generalLib.clickOnWebElement(driver, new CommonPageWebElements(driver)
                        .getDashboardBtn());
                    generalLib.waitForPageLoad(driver);
                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                        generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                    {
                        generalLib.enterDataInTextBox(driver, homePageElements.
                            getKeyWordSearchTextBox(), keyword);
                        generalLib.clickOnWebElement(driver, homePageElements.getKeywordSearchBtn());
                        generalLib.waitForPageLoad(driver);
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
}