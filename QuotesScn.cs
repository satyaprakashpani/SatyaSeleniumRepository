using CBApllications.GeneralLib;
using CBApllications.PurchasingPageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CBApllications.ScenarioLib
{
    class QuotesScn
    {
        GenericLib generalLib = new GenericLib();
        ApplicationRelatedLib applicationLib = new ApplicationRelatedLib();

        /// <summary>
        /// This method creates quote and returns quote id
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="quoteType">This is the type of quote to be configured 
        /// (Open/Sealed/Negotiated)</param>
        /// <param name="quoteDescription">The text to enter in your reference textbox which appears
        /// in quote description</param>
        /// <returns></returns>
        public String createQuote(IWebDriver driver, String quoteType, String quoteDescription)
        {
            ConfigureQuotePage configQuotePageElements = new ConfigureQuotePage(driver);
            CommonPageWebElements commonPgElements = new CommonPageWebElements(driver);
            String quoteId = "";
            // String quoteText = "Automation script "+quoteType+" quote test " 
            String quoteText = quoteDescription + "-"  + generalLib.getRandomNo(1, 1000);
            generalLib.waitForPageLoad(driver);
            configureQuote(driver,quoteType);
            generalLib.waitForPageLoad(driver);
            generalLib.normalWait(2000);
            searchAndAddSupplierToQuote(driver, "demo");
            generalLib.waitForPageLoad(driver);
            if (applicationLib.verifySuccessMessage(driver, "Suppliers added to quote."))
            {
                generalLib.moveToElementAndClick(driver, configQuotePageElements
                    .getQtAddSupplierSaveAndContinueBtn());

                selectQuoteTemplateAndEnterQuoteText(driver, "quote", quoteText);
                generalLib.waitForPageLoad(driver);

                generalLib.clickOnWebElement(driver, configQuotePageElements
                    .getSaveAndContinueToQuoteSummaryBtn());
                generalLib.waitForPageLoad(driver);

                quoteId = generalLib.getTextFromElement(driver, configQuotePageElements
                    .getConfirmAndSendPgQuoteId());

                generalLib.clickOnWebElement(driver, configQuotePageElements.getSendQuoteBtn());
                generalLib.waitForPageLoad(driver);

            }
            return quoteId;
        }

        public bool mailVerificationForCreateQuote(IWebDriver driver, String quoteId)
        {
                bool status = false;
                generalLib.loginToOutlook(driver);
                generalLib.waitForPageLoad(driver);
                generalLib.searchInOutlookMail(driver, quoteId);
                if (driver.FindElement(By.XPath("//div[contains" +
                    "(text(),'Top results')]/following-sibling::div")).Displayed)
                {
                    status = true;
                    generalLib.clickOnWebElement(driver, driver.FindElement(By.XPath("//div[contains" +
                    "(text(),'Top results')]/following-sibling::div")));
                }

            
            generalLib.normalWait();
            generalLib.logOutFromOutLook(driver);
            generalLib.normalWait();

            return status;
        }

        /// <summary>
        /// This method configures quote by selecting type of quote, entering close by date and click on
        /// Save and continue button
        /// </summary>
        /// <param name="driver">IWebdriver Object to drive the execution</param>
        /// <param name="quoteType">Type of quote</param>
        public void configureQuote(IWebDriver driver, String quoteType)
        {
            ConfigureQuotePage configQuotePageElements = new ConfigureQuotePage(driver);
            String currentDateTime = DateTime.Now.AddDays(2).ToString("MM/dd/yyyy") + " 08:00";
            generalLib.waitForPageLoad(driver);
            switch (quoteType.ToLower())
            {
                case "open":
                    generalLib.clickOnWebElement(driver, configQuotePageElements.getOpenQuoteRadioBtn());
                    break;

                case "sealed":
                    generalLib.clickOnWebElement(driver, configQuotePageElements.getSealedQuoteRadioBtn());
                    break;

                case "negotiated":
                    generalLib.clickOnWebElement(driver, configQuotePageElements.getNegotiatedQuoteRadioBtn());
                    break;

                default:
                    Console.WriteLine("'"+quoteType+"' is not a valid type");
                    break;
            }
                    generalLib.enterDataInTextBox(driver, configQuotePageElements.getcloseByDtDatePicker(),
                     currentDateTime);
                    generalLib.normalWait(2000);
                    generalLib.clickOnWebElement(driver, new CommonPageWebElements(driver).getSubmitBtn());
                  
        }

        /// <summary>
        /// This method search and select the supplier provided in supplier argument and 
        ///  click on Add selected supplier button
        /// </summary>
        /// <param name="driver">IWebdriver Object to drive the execution</param>
        /// <param name="supplier">Full Name or partial name of the supplier</param>
        public void searchAndAddSupplierToQuote(IWebDriver driver, String supplier)
        {
            QuoteSupplierSearchPage qtSupSearchPgElements = new QuoteSupplierSearchPage(driver);
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                  generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                generalLib.enterDataInTextBox(driver, qtSupSearchPgElements.getSearchTermTextBox(), supplier);
                generalLib.clickOnWebElement(driver, qtSupSearchPgElements.getSearchBtn());
                generalLib.waitForPageLoad(driver);
                generalLib.clickOnWebElement(driver,qtSupSearchPgElements.getAddThisCompanyToQUoteRadioBtn());
                generalLib.clickOnWebElement(driver,qtSupSearchPgElements.getAddSelectedSuppliersBtn());
            }
                
        }

        /// <summary>
        /// This method selects the required type of template and clicks on submit button
        /// </summary>
        /// <param name="driver">IWebdriver Object to drive the execution</param>
        /// <param name="templateType">This is the template type (Quote/List)</param>
        public void chooseTemplate(IWebDriver driver, String templateType)
        {
            generalLib.waitForPageLoad(driver);
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
          generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                ConfigureQuotePage configQuotePageElements = new ConfigureQuotePage(driver);
                switch (templateType.ToLower())
                {
                    case "quote":
                        generalLib.clickOnWebElement(driver,configQuotePageElements
                            .getChooseTemplateQuoteTempRadioBtn());
                        generalLib.clickOnWebElement(driver, new CommonPageWebElements(driver).getSubmitBtn());
                        break;

                    case "list":
                        generalLib.clickOnWebElement(driver, configQuotePageElements
                            .getChooseTemplateListTempRadioBtn());
                        generalLib.clickOnWebElement(driver, new CommonPageWebElements(driver).getSubmitBtn());
                        break;
                }
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="templateType"></param>
        /// <param name="quoteText"></param>
        public void selectQuoteTemplateAndEnterQuoteText(IWebDriver driver, String templateType,
            String quoteText)
        {
            chooseTemplate(driver, templateType);
            generalLib.enterDataInTextBox(driver, new ConfigureQuotePage(driver).getQuotTextArea(), quoteText);
            generalLib.clickOnWebElement(driver, new CommonPageWebElements(driver).getSubmitBtn());
        }

        /// <summary>
        /// This method clicks on Create quote template in Quote history page, then enter template name in 
        /// your reference text box and click on submit button
        /// </summary>
        /// <param name="driver">IWebdriver Object to drive the execution</param>
        /// <param name="descriptionText">Create quote template button is selected based on this
        /// String in description of the quote</param>
        /// <param name="templateName">Template name to enter in your reference text box</param>
        public void createQuoteTemplate(IWebDriver driver, String descriptionText, String templateName)
        {
            
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                QuoteHistoryPage qtHistoryPgElements = new QuoteHistoryPage(driver);
                try
                {
                    generalLib.clickOnWebElement(driver, qtHistoryPgElements
                        .getCreateQtTemplateForRequiredDescription(descriptionText));
                    generalLib.waitForPageLoad(driver);
                    generalLib.enterDataInTextBox(driver, qtHistoryPgElements.getQuoteReferenceTextBox(),
                        templateName);
                    generalLib.clickOnWebElement(driver,new CommonPageWebElements(driver).getSubmitBtn());
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
           
        }
        /// <summary>
        /// This method verifies that if it is redirected to SavedQuoteTemplates.aspx page after creating quote 
        /// tempalate and returns true if entered tempalate name appears in List of saved templates
        /// </summary>
        /// <param name="driver">IWebdriver Object to drive the execution</param>
        /// <returns></returns>
        public bool verifyCreateQuoteTemplate(IWebDriver driver)
        {
            bool status = false;
            String templateName = "Automation Create Quote Template Test " + generalLib.getRandomNo(1, 999);
            createQuoteTemplate(driver, "Open Quote to test Create template", templateName);
            Console.WriteLine("Template name : "+templateName);
            driver.Navigate().Refresh();
            //driver.Url = "https://purchasing.uk-plc.net/BuyerDotNet2/requestquote/savedquotetemplates.aspx";
            generalLib.normalWait(6000);
            try
            {
                generalLib.waitForPageLoad(driver);
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                        generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    if (applicationLib.verifyNavigationToPage(driver, "SavedQuoteTemplates.aspx",
                        "Saved quote templates"))
                    {
                        IReadOnlyCollection <IWebElement> yourRefCollection =  new QuoteHistoryPage(driver)
                            .getYourReferenceInSavedQtTemplate("Automation Create Quote Template Test");
                        Console.WriteLine("No of elements in yourRefCollection : "+
                            yourRefCollection.Count);
                        foreach(var refCollection in yourRefCollection)
                        {
                            Console.WriteLine("Template --> "+refCollection.Text);
                            if (refCollection.Text.Equals(templateName))
                            {
                                status = true;
                                break;
                            }
                            Console.WriteLine("Status-->"+status);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Navigation to Saved quote templates page failed");
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

        public void deleteTemplateInSavedRFQPage(IWebDriver driver, String templateName)
        {
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                QuoteHistoryPage qtHistPageElements= new QuoteHistoryPage(driver);
                try
                {
                    IReadOnlyCollection<IWebElement> yourReferenceCollection = qtHistPageElements
                        .getYourReferenceInSavedQtTemplate(templateName);
                    foreach (var refCollection in yourReferenceCollection)
                    {
                        if (generalLib.verifyPresenceOfElement(driver,refCollection))
                        {
                            generalLib.clickOnWebElement(driver, qtHistPageElements
                                .getDeleteBtnForRequiredTemplate(templateName));
                            generalLib.clickOKForBrowserPopup(driver);
                            generalLib.normalWait(2000);
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

        public void cancelQuoteWithReason(IWebDriver driver, String reason, String quoteId)
        {
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
               generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                 //generalLib.clickOnDesiredLink(driver, "New request for quote");
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    try
                    {
                    generalLib.waitForPageLoad(driver);
                    generalLib.clickOnWebElement(driver, new QuoteHistoryPage(driver)
                        .getCancelBtnForRequiredQuoteId(quoteId));
                    generalLib.normalWait(2000);
                    AjaxPopUpElements ajaxElements =  new AjaxPopUpElements(driver);
                    if (generalLib.verifyPresenceOfElement(driver, ajaxElements.getAjaxSubmitBtn()))
                    {
                        generalLib.enterDataInTextBox(driver,ajaxElements.
                            getQuoteSearchPageCancelReasonTextbox(),reason);
                        generalLib.clickOnWebElement(driver,ajaxElements.getAjaxSubmitBtn());
                        

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

        public bool verifyCancelQuote(IWebDriver driver, String quoteId)
        {
            bool status = false;
            driver.Navigate().Refresh();
            generalLib.filterDataWithDifferentOption(driver, quoteId, "Equals...", "Quote ID");
            generalLib.waitForPageLoad(driver);

            if(generalLib.getTextFromElement(driver,new QuoteHistoryPage(driver)
                .getColumnTextAgainstQuoteId(quoteId,"7")).Equals("Quote rejected."))
            {
                status = true;
            }

            return status;
        }

    }
}
