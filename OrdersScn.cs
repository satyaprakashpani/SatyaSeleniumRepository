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
    class OrdersScn
    {
        GenericLib generalLib = new GenericLib();
        ApplicationRelatedLib applicationLib = new ApplicationRelatedLib();
        SearchProductsScn searchProdScn = new SearchProductsScn();
        BasketPageScn basketPgScn = new BasketPageScn();
        CheckoutPageScn chkoutPgScn = new CheckoutPageScn();

        /// <summary>
        /// This method adds order to favorite from order history page and then click on remove 
        /// favorites button
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>Returns true if successful message appears after removing favorite order</returns>
        public bool removeFavOrdFromFavOrdPage(IWebDriver driver)
        {
            DisplayOrdersPage dispOrdPageElements = new DisplayOrdersPage(driver);
            bool status = false;

            try
            {
                generalLib.selectByValue(driver, dispOrdPageElements.getActionDropdown(), "Favourite");
                generalLib.clickOnWebElement(driver, dispOrdPageElements.getActionSelectBtn());
                generalLib.waitForPageLoad(driver);
                AddToFavouritesPage addToFavPageElements = new AddToFavouritesPage(driver);
                if (generalLib.verifyPresenceOfElement(driver, addToFavPageElements.getFavOrderNameTextBox()))
                {
                    generalLib.enterDataInTextBox(driver, addToFavPageElements.getFavOrderNameTextBox(),
                        "Automation Fav Ord Test To Remove");
                    generalLib.clickOnWebElement(driver, addToFavPageElements.getaddToFavouritesBtn());
                    generalLib.normalWait();

                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                    {
                        FavouritesOrdersPage favOrdPageElements = new FavouritesOrdersPage(driver);

                        if (generalLib.verifyPresenceOfElement(driver, favOrdPageElements.
                            getRemoveFavouriteBtn()))
                        {
                            Console.WriteLine("Remove Fav btn is present");
                            generalLib.clickOnWebElement(driver, favOrdPageElements.removeFavouriteBtn);
                            generalLib.normalWait(6000);

                            //MessageCollectionPage messageColPageElements = new 
                            //    MessageCollectionPage(driver);
                            //if (generalLib.verifyPresenceOfElement(driver,messageColPageElements.
                            //    getSuccessMsgDiv()))
                            //{
                            //    String successMsg = generalLib.getTextFromElement(driver,
                            //        messageColPageElements.getSuccessMsgText());
                            //    Console.WriteLine("Success Message : " + successMsg);
                            //    if (successMsg.Equals("Favourite order deleted."))
                            //    {
                            //        status = true;
                            //    }

                            status = applicationLib.verifySuccessMessage(driver, "Favourite order deleted.");
                        }
                        else
                            Console.WriteLine("Success Msg not appeared");
                    
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
            return status;
        }


        public String editFabOrdName(IWebDriver driver)
        {

            String editData = "Automation Test Edit " + generalLib.getRandomNo(1, 1000).ToString();
            FavouritesOrdersPage favOrderPageElements = new FavouritesOrdersPage(driver);
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))

                {
                    if (generalLib.verifyPresenceOfElement(driver, favOrderPageElements.getEditNameBtn()))
                    {
                        generalLib.clickOnWebElement(driver, favOrderPageElements.getEditNameBtn());
                        generalLib.normalWait(2000);
                        if (generalLib.verifyPresenceOfElement(driver, favOrderPageElements.
                            getEditAjaxFormConainerPopUp()))
                        {
                            Console.WriteLine("-------OK------");
                            generalLib.clearTextFromTextbox(driver, favOrderPageElements
                                .getEditFavNameTextbox());
                            generalLib.enterDataInTextBox(driver, favOrderPageElements.getEditFavNameTextbox(),
                                editData);
                            generalLib.clickOnWebElement(driver, favOrderPageElements.getEditFavSaveBtn());
                            generalLib.normalWait(4000);
                        }
                        else
                            Console.WriteLine("-------Element not appeared------");
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
            return editData;
        }
        /// <summary>
        /// This method verifies Fav order name text with entered text while edit
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="enteredTextWhileEdit">Entered text while edit</param>
        /// <returns>Returns true if Fav Order text is equal with entered text while edit</returns>
        public bool verifyEditFavOrdNameTextAfterEdited(IWebDriver driver, String enteredTextWhileEdit)
        {
            bool status = false;
            String actNameAfterEdit = generalLib.getTextFromElement(driver, new FavouritesOrdersPage(driver)
                .getFavOrderNameText());
            Console.WriteLine("Act name after edit :            " + actNameAfterEdit);
            if (enteredTextWhileEdit.Equals(actNameAfterEdit.Trim()))
            {
                status = true;
            }
            return status;
        }

        /// <summary>
        /// This method returns true if filtered Ord Id is equal with Ord no after filter
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="ordNo">Order no in ord history paased to compare with Ord no in
        /// purchasing view</param>
        /// <returns>Returns true if Ord no in both Display Orders page and
        /// Purch. view page are equal</returns>
        public bool verifyOrdNoINPurchOrderPage(IWebDriver driver, String ordNo)
        {
            bool status = false;
            try
            {
                generalLib.filterDataWithDifferentOption(driver, ordNo, "Contains...", "Order refs");
                generalLib.waitForPageLoad(driver);
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    DisplayOrdersPage dispOrderPageElements = new DisplayOrdersPage(driver);
                    generalLib.selectByValue(driver, dispOrderPageElements.getActionDropdown(),
                        "ViewPurchaseOrder");
                    generalLib.clickOnWebElement(driver, dispOrderPageElements.getActionSelectBtn());
                    generalLib.normalWait(1000);
                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                    {
                        if (driver.Url.Contains("htmlInvoice.aspx"))
                        {
                            if (ordNo.Equals(getOrdNoFromPurchOrderPage(driver)))
                            {
                                status = true;
                            }
                        }
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
            driver.Navigate().Back();
            generalLib.normalWait(1000);
            generalLib.openURL(driver, "https://purchasing.uk-plc.net/BuyerDotNet2/ordering/" +
                "displayorders.aspx");
            generalLib.waitForPageLoad(driver);
            return status;
        }

        public String placeNewOrder(IWebDriver driver, String keyword, String quantity, String glCode)
        {
            String ordNo_Chkout = "";
            generalLib.waitForPageLoad(driver);
            searchProdScn.keyWordSearch(driver, keyword);
            searchProdScn.updateQuantityInSearchProdctAndAddToBasket(driver, quantity);
            generalLib.waitForPageLoad(driver);
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                if (basketPgScn.verifyNavigationToBsketOrCheckoutPage(driver, "Basket"))
                {
                    generalLib.clickOnWebElement(driver, new BasketPage(driver)
                       .getBasketGoToCheckoutBtn());
                    generalLib.waitForPageLoad(driver);
                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                        generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                    {
                        ordNo_Chkout = chkoutPgScn.getOrderNoFromCheckoutPage(driver);
                        generalLib.clickOnWebElement(driver, new CheckoutReviewPage(driver)
                            .getCompleteOrderBtn());
                        Console.WriteLine("Order no : "+ ordNo_Chkout);
                        generalLib.waitForPageLoad(driver);
                        if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                            generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                        {
                            chkoutPgScn.selectGLCode(driver,glCode);
                            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                                generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                            {
                                new RequestApprovalPageScn().enterRequestApprovalMsgAndClickBtn(driver, 
                                    "Test Approval Message for order No : "+ ordNo_Chkout);
                            }
                        }
                    }
                }
            }
            return ordNo_Chkout;
        }

        /// <summary>
        /// This method returns true if filtered Ord Id is equal with Ord no after filter
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="purchId"></param>
        /// <returns></returns>
        public bool verifyPurchOrdINPurchOrderPage(IWebDriver driver, String purchId)
        {
            bool status = false;
            try
            {
                generalLib.filterDataWithDifferentOption(driver, purchId, "Contains...", "Order refs");
                generalLib.waitForPageLoad(driver);
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    DisplayOrdersPage dispOrderPageElements = new DisplayOrdersPage(driver);
                    generalLib.selectByValue(driver, dispOrderPageElements.getActionDropdown(),
                        "ViewPurchaseOrder");
                    generalLib.clickOnWebElement(driver, dispOrderPageElements.getActionSelectBtn());
                    generalLib.normalWait(1000);
                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                    {
                        if (driver.Url.Contains("htmlInvoice.aspx"))
                        {
                            if (purchId.Equals(getPurchIdFromPurchOrderPage(driver)))
                            {
                                status = true;
                            }
                        }
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
            driver.Navigate().Back();
            generalLib.normalWait(1000);
            return status;
        }

        /// <summary>
        /// This method returns true if filtered Purchase Id is equal with purchase id in Recpt order page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="purchId"></param>
        /// <returns></returns>
        public bool verifyPurchOrdInRecptOrderPage(IWebDriver driver, String purchId)
        {
            bool status = false;
            try
            {
                generalLib.filterDataWithDifferentOption(driver, purchId, "Contains", "Order refs");
                generalLib.waitForPageLoad(driver);
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    DisplayOrdersPage dispOrderPageElements = new DisplayOrdersPage(driver);
                    generalLib.selectByValue(driver, dispOrderPageElements.getActionDropdown(),
                        "ReceiptAll");
                    generalLib.clickOnWebElement(driver, dispOrderPageElements.getActionSelectBtn());
                    generalLib.normalWait(1000);
                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                    {
                        if (driver.Url.Contains("ActionReceiptWholeOrder.aspx"))
                        {
                            if (purchId.Equals(getPurchIdFromReceiptOrderPage(driver)))
                            {
                                status = true;
                            }
                        }
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
            driver.Navigate().Back();
            generalLib.normalWait(1000);
            return status;
        }

        /// <summary>
        /// This method returns ord no from Purchase order page 
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>Rturns order id from Purch Order page</returns>
        public String getOrdNoFromPurchOrderPage(IWebDriver driver)
        {
            String ordNoinPurchOrdPage = "";
            try
            {
                String ordNoTextInPurOrdPage = generalLib.getTextFromElement(driver, new PurchaseOrderPage(driver)
                    .getOrderNoTextInPurchaseOrder());
                String[] ordNoArr = ordNoTextInPurOrdPage.Split(':');
                Console.WriteLine("Ord no: " + ordNoArr[ordNoArr.Length - 1].Trim());
                ordNoinPurchOrdPage = ordNoArr[ordNoArr.Length - 1].Trim();
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught in getOrdNoFromPurchOrderPage()`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return ordNoinPurchOrdPage;
        }

        /// <summary>
        /// This method returns Purch Id from Purchase order page
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>Returns Purch Id from purch order page</returns>
        //
        public String getPurchIdFromPurchOrderPage(IWebDriver driver)
        {
            String purchIdInPurchOrdPage = "";
            try
            {
                String ordNoTextInPurOrdPage = generalLib.getTextFromElement(driver,
                    new PurchaseOrderPage(driver).getPurchageIdTextInPurchaseOrder());
                Console.WriteLine("~~~~~~~~" + ordNoTextInPurOrdPage);
                String[] purcIdArr = ordNoTextInPurOrdPage.Split(':');

                purchIdInPurchOrdPage = purcIdArr[purcIdArr.Length - 1].Trim();
                Console.WriteLine("Purch Id : " + purchIdInPurchOrdPage);
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught in getOrdNoFromPurchOrderPage()`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return purchIdInPurchOrdPage;
        }

        /// <summary>
        /// This method returns ord no from Receipt order page
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>Returns purchase Id from Recept order page</returns>
        public String getPurchIdFromReceiptOrderPage(IWebDriver driver)
        {
            String purchId = "";
            try
            {
                String purchIdText = generalLib.getTextFromElement(driver, new ReceiptOrderPage(driver)
                    .getReceiptOrderPgOrderNo());
                Console.WriteLine("~~~~~~~~" + purchIdText);
                String[] purchIdTextArr = purchIdText.Split('\n');
                foreach (String str in purchIdTextArr)
                {
                    Console.WriteLine(str);
                    if (str.Contains("Purchase Order"))
                    {
                        String[] purchIdTArr = str.Split(':');
                        purchId = purchIdTArr[purchIdTArr.Length - 1].Trim();
                    }
                }

                Console.WriteLine("Purch Id : " + purchId);
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught in getOrdNoFromPurchOrderPage()`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return purchId;
        }

    }
}
