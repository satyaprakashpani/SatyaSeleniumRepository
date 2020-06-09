using CBApllications.GeneralLib;
using CBApllications.PurchasingPageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CBApllications.ScenarioLib
{
    class SearchProductsScn
    {
         
        GenericLib generalLib = new GenericLib();
        ApplicationRelatedLib applicationRelatedLib = new ApplicationRelatedLib();
        CheckoutPageScn chkoutPgScn = new CheckoutPageScn();
        BasketPageScn basketPgScn = new BasketPageScn();

        /// <summary>
        /// This method returns the total product count of the search result
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>Total product count of search result</returns>
        public int getTotalProductsForSearchResult(IWebDriver driver)
        {
            int totalProdCount = 0;
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    totalProdCount = int.Parse(generalLib.getTextFromElement(driver,
                        new SearchProductsPage(driver).getNoOfSearchResult()));
                    Console.WriteLine("Total Prod count: "+totalProdCount);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return totalProdCount;
        }

        /// <summary>
        /// This method returns total product count in supplier section
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>Total no of product count in supplier section</returns>
        public int getProdCountInSupplierSection(IWebDriver driver)
        {
            int prodCount = 0;
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    
                    foreach (IWebElement ele in new SearchProductsPage(driver).
                        getsupplierListItemCount())
                    {
                        String prodStr = ele.Text;
                        prodCount = prodCount + int.Parse(prodStr);
                        Console.WriteLine("Total product in supplier section : "+prodCount);
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
            return prodCount;
        }
       

        /// <summary>
        /// This method is designed to verify search result in search product page via Dashboard
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void verifyKeywordSearchResult(IWebDriver driver, String keyword)
        {
            SearchProductsPage searchProdPageElements = new SearchProductsPage(driver);
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    generalLib.clickOnWebElement(driver, new HomePage(driver).getKeywordSearchBtn());
                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                        generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                    {
                        generalLib.normalWait();
                        Assert.Multiple(() =>
                        {
                            Assert.AreEqual(searchProdPageElements.getSearchTextBox().GetAttribute
                                ("value"), keyword, "Text in keyword textbox is not empty");
                           
                            Assert.AreEqual(getProdCountInSupplierSection(driver),
                                getTotalProductsForSearchResult(driver), "Total no of " +
                                "product is not equal wih total product in supplier section ");
                        });
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


        /// <summary>
        /// This method updates the quantity in searchProduct page and clicks on add to basket button
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="quantity"></param>
        public void updateQuantityInSearchProdctAndAddToBasket(IWebDriver driver, String quantity)
        {
            SearchProductsPage srchProdPageElements = new SearchProductsPage(driver);
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    generalLib.waitForPageLoad(driver);
                    generalLib.enterDataInTextBox(driver, srchProdPageElements
                        .getSearchPageQuantityTextBox(), quantity);
                    generalLib.clickOnWebElement(driver, srchProdPageElements.getSearchPageAddToBasketBtn());
                    generalLib.waitForPageLoad(driver);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp);
            }
        }

        /// <summary>
        /// This method search the keyword through keyword search functionality via swipe menu
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="keyword">Keyword provided to search</param>
        public void keyWordSearch(IWebDriver driver, String keyword)
        {
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                try
                {
                    applicationRelatedLib.clickOnMenuSwipeBtn(driver);
                    applicationRelatedLib.clickOnExpandBtnForGivenLink(driver,"Orders");
                    applicationRelatedLib.clickOnExpandBtnForGivenLink(driver, "New order");
                    generalLib.clickOnDesiredLink(driver, "Keyword search");
                    generalLib.waitForPageLoad(driver);

                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                         generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                    {
                        generalLib.enterDataInTextBox(driver, new SearchProductsPage(driver)
                            .getSearchTextBox(), keyword+Keys.Enter);
                        generalLib.waitForPageLoad(driver);
                    }
                        
                    }
                catch (Exception exp)
                {
                    Console.WriteLine("`````Exception caught`````");
                    generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name);//Take screenshot and saved in SreenShot folder
                    generalLib.PrintAllLogs(driver);
                    Console.WriteLine(exp);
                }
            }
        }

       

    }
}
