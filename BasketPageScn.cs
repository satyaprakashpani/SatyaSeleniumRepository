using CBApllications.GeneralLib;
using CBApllications.PurchasingPageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CBApllications.ScenarioLib
{
    class BasketPageScn
    {
        GenericLib generalLib = new GenericLib();

        /// <summary>
        /// This method verifies the navigation to Basket page
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public bool verifyNavigationToBsketOrCheckoutPage(IWebDriver driver, String pageName)
        {
            bool status = false;
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                try
                {
                    CommonPageWebElements commenPageElements = new CommonPageWebElements(driver);
                    switch (pageName.ToLower())
                    {
                        case "basket":
                            if (driver.Url.Contains("basket.uk-plc.net/Basket.aspx"))
                            {
                                if (generalLib.getTextFromElement(driver, commenPageElements
                                    .getBasketChkOutPageHeader()).Equals("Basket"))
                                {
                                    status = true;
                                }
                            }
                            break;
                        case "chechout":
                            if (driver.Url.Contains("secure.uk-plc.net/checkout/review.aspx"))
                            {
                                if (generalLib.getTextFromElement(driver, commenPageElements
                                    .getBasketChkOutPageHeader()).Equals("Checkout"))
                                {
                                    status = true;
                                }
                            }
                            break;
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
        /// This method gives the list of data like Item name, quantity and total price
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public List<String> getDataInBasketPage(IWebDriver driver)
        {
            BasketPage basketPageElements = new BasketPage(driver);
            List<String> basketData = new List<string>();
            try
            {
                basketData.Add(generalLib.getTextFromElement(driver, basketPageElements.getBasketItemName()));
                basketData.Add(basketPageElements.getBasketQuantityTextbox().GetAttribute("value"));
                basketData.Add(generalLib.getTextFromElement(driver, basketPageElements.
                    getBasketTotalPrice()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("HResult in basket :" + ex.HResult);
            }
            return basketData;
        }

    }
}
