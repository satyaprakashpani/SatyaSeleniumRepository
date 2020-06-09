using CBApllications.PurchasingPageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CBApllications.GeneralLib
{
    class GenericLib
    {
        ///<Summary> 
        ///This method opens the provided URL 
        ///</Summary>
        ///<param name="driver">Driver object</param>
        ///<param name="URL">URL to navigate to</param>
        public void openURL(IWebDriver driver, String URL)
        {
            driver.Url = URL;
        }

        /// <summary>
        /// This method verifies the presence of Logo
        /// </summary>
        /// <param name="driver"></param>
        /// <returns>Returns true if Logo is present</returns>
        public bool verifyPresenceOfLogo(IWebDriver driver)
        {
            bool status = false;
            try
            {
                if (getStatusCode(driver.Url).Equals("OK") ||
                    getStatusCode(driver.Url).Equals("Redirect"))
                {
                    if (driver.FindElement(By.Id("appLogo")).Displayed)
                    {
                        if (new CommonPageWebElements(driver).getLogoImg().Displayed)
                        {
                            if (getLogoFileName(driver).Equals("cloudbuy-logo.png"))
                            {
                                status = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("App Logo div present but image not present ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("App Logo not present");
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }

            return status;
        }

        /// <summary>
        /// This method returns the file name of LOGO
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        //   
        public String getLogoFileName(IWebDriver driver)
        {
            String srcOfLogo = driver.FindElement(By.XPath("//div[@id='appLogo']/img")).
                GetAttribute("src");
            srcOfLogo.Split('/');
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^" + srcOfLogo.Split('/').Last());
            return srcOfLogo.Split('/').Last();
        }

        /// <summary>
        /// This method opens the Login URL of Purchasing
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void openLoginURLForPurchasing(IWebDriver driver)
        {
            driver.Url = "https://purchasing.uk-plc.net/BuyerDotNet2/Account/Account/LogIn";

        }

        /// <summary>
        /// This medhod click on Logout button
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void logOutFromPurchasing(IWebDriver driver)
        {
           
                if (getStatusCode(driver.Url).Equals("OK") ||
                    getStatusCode(driver.Url).Equals("Redirect"))
                {
                    try
                    {
                        clickOnWebElement(driver, new CommonPageWebElements(driver).getLogoutBtn());
                        Console.WriteLine("------------------Logged out from application--------");
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("`````Exception caught`````");
                        takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                        PrintAllLogs(driver);
                        Console.WriteLine(exp.Message);
                    }
                
                }
            
                waitForPageLoad(driver);
        }

        /// <summary>
        /// This mehod stops the excution for 4 seconds and then resumes
        /// </summary>
        public void normalWait()
        {
            System.Threading.Thread.Sleep(4000);
        }

        /// <summary>
        /// This mehod stops the excution given time seconds and then resumes
        /// </summary>
        /// <param name="timeInMiliSecond"></param>
        public void normalWait(int timeInMiliSecond)
        {
            System.Threading.Thread.Sleep(4000);
        }

        /// <summary>
        /// This method designed to wait till 50 seconds using implicitWait
        /// </summary>
        /// <param name="driver"></param>
        public void waitForPageLoad(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
        }

        /// <summary>
        /// This method designed to get status code of a URL
        /// </summary>
        /// <param name="URL"></param>
        /// <returns>Returns the status code of the given URL</returns>
        public String getStatusCode(String URL)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest
                                           .Create(URL);
            webRequest.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Console.WriteLine("Status code for The URL ---->" + response.StatusCode);
            return response.StatusCode.ToString();
        }

        /// <summary>
        /// This method opns the outlook
        /// </summary>
        /// <param name="driver"></param>
        public void openOutLookUrl(IWebDriver driver)
        {
            driver.Url = "https://outlook.office365.com/mail/inbox";
        }

        /// <summary>
        /// This method designed to login to outlook by using OutLook credential
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void loginToOutlook(IWebDriver driver)
        {
            Console.WriteLine("--------------------LoginToOutllok() starts....-------");
            openOutLookUrl(driver);
            normalWait(2000);
            try
            {
                
                    if (driver.FindElement(By.XPath("//div[contains(text(),'Use another account')]"))
                        .Displayed)
                    {
                        clickOnWebElement(driver, driver.FindElement(By.XPath("//div[contains(text()," +
                            "'Use another account')]")));
                        normalWait(2000);

                    }
                
                    
                    driver.FindElement(By.Id("i0116")).SendKeys("satyaprakash.pani@cloudbuy.com");
                    driver.FindElement(By.Id("idSIButton9")).Click();
                    normalWait(2000);
                    driver.FindElement(By.Id("i0118")).SendKeys("Kila@555!");
                    driver.FindElement(By.XPath("//input[@value='Sign in']")).Click();
                    waitForPageLoad(driver);
                    normalWait(2000);
                    //Actions act = new Actions(driver);
                    if (driver.FindElement(By.XPath("//div[contains(text(),'Stay signed in?')]"))
                        .Displayed)
                    {
                        IWebElement ysBtn = driver.FindElement(By.XPath("//div[@class='inner" +
                            " fade-in-lightbox']//input[@id='idBtn_Back']"));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", ysBtn);
                    }
                    //act.MoveToElement(driver.FindElement(By.Id("idSIButton9"))).Click();
                    else
                        Console.WriteLine("'Stay signed in?' window not appeard ");
                    normalWait(2000);
                    waitForPageLoad(driver);
                //driver.FindElement(By.XPath("//div[contains(text(),'Outlook')]")).Click();
                Console.WriteLine("-----------------LoginToOutlook() ends-----------");
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

        /// <summary>
        /// This method is designed to search 'Reset your password' in outlook mail 
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>Returns true if it found 'Reset your password' in outlook mail</returns>
        public bool verifyRSetPwdByUsrName(IWebDriver driver)
        {
            bool status = false;
            try
            {
                waitForPageLoad(driver);
                if (driver.FindElement(By.XPath("//span[contains(text(),'Reset your" +
                    " password')]")).Displayed)
                {
                    driver.FindElement(By.XPath("//span[contains(text(),'Reset your" +
                        " password')]")).Click();
                    status = true;
                }
                else
                {
                    status = false;
                    Console.WriteLine("Mail not received");
                }

            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return status;
        }
        /// <summary>
        /// This method mouse hover to the element and performs the click operation
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public void moveToElementAndClick(IWebDriver driver, IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Click().Perform();
        }

        /// <summary>
        /// This method is designed to verify reset password by email
        /// It searches the text 'You recently requested' in the outlook mail
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>It returns true if it can identify the text 'You recently requested' in the 
        /// received email</returns>
        public bool verifyRSetPwdByEmail(IWebDriver driver)
        {
            bool status = false;
            try
            {
                waitForPageLoad(driver);
                if (driver.FindElement(By.XPath("//span[contains(text(),'You recently" +
                    " requested')]")).Displayed)
                {
                    driver.FindElement(By.XPath("//span[contains(text(),'You recently" +
                        " requested')]")).Click();
                    status = true;
                }
                else
                {
                    status = false;
                    Console.WriteLine("EMail not received");
                }

            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return status;
        }

        /// <summary>
        /// This method select n'th option of dropdown if passed index is n
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="dropDown">The webelement dropdown</param>
        /// <param name="index">This is the index value of the option</param>
        public void selectByIndex(IWebDriver driver, IWebElement dropDown, int index)
        {
            waitForPageLoad(driver);
            try
            {
                SelectElement selectElement = new SelectElement(dropDown);
                selectElement.SelectByIndex(index);
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

        /// <summary>
        /// This method selects a option of dropdown using  visibile text 
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="dropDown">The webelement dropdown</param>
        /// <param name="index">Visible text of the option</param>
        public void selectByVisibleText(IWebDriver driver, IWebElement dropDown, String text)
        {
            waitForPageLoad(driver);
            try
            {
                SelectElement selectElement = new SelectElement(dropDown);
                selectElement.SelectByText(text);
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

        /// <summary>
        /// This method selects a option of dropdown using  value attribute of the option 
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="dropDown">The webelement dropdown</param>
        /// <param name="index">Value attribute of the option</param>
        public void selectByValue(IWebDriver driver, IWebElement dropDown, String value)
        {
            waitForPageLoad(driver);
            try
            {
                SelectElement selectElement = new SelectElement(dropDown);
                selectElement.SelectByValue(value);
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

        /// <summary>
        /// This method returns List of option available in the dropdown
        /// </summary>
        /// <param name="driver">Driver objct to drive the execution</param>
        /// <param name="dropdown">Dropdown element</param>
        /// <returns>List of dropdown options</returns>
        public IList<IWebElement> getDropdownOptions(IWebDriver driver, IWebElement dropdown)
        {
            IList<IWebElement> optnList = null;
            waitForPageLoad(driver);
            try
            {
                SelectElement selElement = new SelectElement(dropdown);
                optnList = selElement.Options;
                foreach (IWebElement ele in optnList)
                {
                    Console.WriteLine(ele.Text);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);

            }
            return optnList;
        }

        /// <summary>
        /// It returns the dropdown option which is selected currently
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="dropdown"></param>
        /// <returns></returns>
        //This method returns the selected option from a given dropdown list
        public String getSelectedOptionfromDropdown(IWebDriver driver, IWebElement dropdown)
        {
            String selectedOtn = "";
            waitForPageLoad(driver);
            try
            {
                SelectElement selectedValue = new SelectElement(dropdown);
                selectedOtn = selectedValue.SelectedOption.Text;
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return selectedOtn;
        }

        
        public int getRandomNo(int low, int high)
        {
            Random random = new Random();
            int randomNumber = random.Next(low, high);
            return randomNumber;
        }

        /// <summary>
        /// This method clicks on contains of filter option, enters data and click on Ok to filter
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="data">Data/Text to filter with contains option</param>
        public void filterDataWithDifferentOption(IWebDriver driver, String data, 
            String option, String filterBtnName)
        {
            FilterWebElement filterWbElements = new FilterWebElement(driver);
            try
            {
                clickOnWebElement(driver,filterWbElements.getFilterBtn(filterBtnName));
                Console.WriteLine(filterBtnName+" clicked ^^^^^^^^^");
                normalWait(2000);
                if (getStatusCode(driver.Url).Equals("OK") ||
                    getStatusCode(driver.Url).Equals("Redirect"))
                {
                    if (filterWbElements.gettableFilterBox().Enabled)
                    {
                        if (option.ToLower().Contains("remove"))
                        {
                            normalWait(10000);
                            clickOnWebElement(driver, filterWbElements.getRemoveFilterBtn());
                            normalWait(2000);
                        }
                        else if (option.Contains("A To Z"))
                        {
                            clickOnWebElement(driver, filterWbElements.getfilterAToZOption());
                            normalWait(2000);
                        }
                        else if (option.Contains("Z To A"))
                        {
                            clickOnWebElement(driver, filterWbElements.getfilterZToAOption());
                            normalWait(2000);
                        }
                        else
                        {
                            clickOnWebElement(driver, filterWbElements.getFilterMenu(option));
                            normalWait(1000);
                            enterDataInTextBox(driver, filterWbElements.getFilterTextBox1(), data);
                            clickOnWebElement(driver, filterWbElements.getFilterFormOkBtn());
                        }
                    }
                    else
                        Console.WriteLine("Table filter not enabled");
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

       
        /// <summary>
        /// This returns the list of pages available in pagination except currently selected page 
        /// that means those pages havinganchor tag
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>List of page numbers with links</returns>
        public IReadOnlyCollection<IWebElement> getPaginationList(IWebDriver driver)
        {
            IReadOnlyCollection<IWebElement> pageList = null;
            try
            {
                pageList = driver.FindElements(By.XPath("//span[contains(text(),'Pages')]/" +
                    "following-sibling::a"));
                Console.WriteLine("No of link : " + pageList.Count);

            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }

            return pageList;
        }

        /// <summary>
        /// it returns true if all filtered data are equals with the text provided to verify
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="dataToVerify">Text/Data to verify</param>
        /// <returns></returns>
        public bool verifyFilteredData(IWebDriver driver, String dataToVerify)
        {
            bool status = false;
            ReadOnlyCollection<IWebElement> filteredData = driver.FindElements(By.XPath(
                        "//span[contains(text(),'" + dataToVerify + "')]"));
            Console.WriteLine("No of Row : " + filteredData.Count);
            foreach (IWebElement element in filteredData)
            {
                if (element.Text.Equals(dataToVerify))
                {
                    status = true;
                    //Console.WriteLine("status" + status);
                }
                else
                {
                    Console.WriteLine(dataToVerify + "Not Equal with " + element.Text);
                    status = false;
                    break;
                }
            }
            return status;
        }


        /// <summary>
        /// This method checks all filtered data by navigating all pages in the navigation
        /// Returns true if all datas in all pages are equal with the data provided to verify
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="data">Text/Data to verify</param>
        /// <returns>Returns true if all datas in all pages are equal with the data/Text provided</returns>
        public bool verifyAllFilteredDataByNavigatingPagintaion(IWebDriver driver, String data)
        {
            bool status = false;
            verifyFilteredData(driver, data);
            IList<String> linkText = new List<String>();
            foreach (IWebElement wb in getPaginationList(driver))
            {
                linkText.Add(wb.Text);
            }
            foreach (String wb in linkText)
            {
                driver.FindElement(By.LinkText(wb)).Click();
                normalWait(2000);
                if (verifyFilteredData(driver, data))
                {
                    status = true;
                }
            }
            return status;
        }

        /// <summary>
        /// Logout from Outlook mail
        /// </summary>
        /// <param name="driver"></param>
        public void logOutFromOutLook(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.XPath("//div[@id='meInitialsButton']/following-sibling::" +
                    "div/img")).Click();
                normalWait(2000);
                if (driver.FindElement(By.Id("meControlSignoutLink")).Displayed)
                    driver.FindElement(By.Id("meControlSignoutLink")).Click();
                else
                    Console.WriteLine("Unable to click Signout-----------");
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

        /// <summary>
        /// It checks order approve mail in Outlook and returns true if found
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="purcId">Purchase Id</param>
        /// <returns>returns true if Approved mail found in outlook mail</returns>
        public bool checkForOrdApprovedMailConfirmation(IWebDriver driver, String purcId)
        {
            bool status = false;
            try
            {
                if (driver.FindElement(By.XPath("//span[contains(text(),'Order')]/../../..")).Displayed)
                {
                    IReadOnlyCollection<IWebElement> listOfEle = driver.FindElements(By.XPath
                        ("//span[contains(text(),'Order')]/../../.."));
                    listOfEle.Last().Click();
                    status = true;
                    normalWait();
                }
                else
                {
                    Console.WriteLine("Email not confirmed");
                    status = false;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return status;
        }

        /// <summary>
        /// Check Outlook mail and search for Approval Request For cloudBuy Purchase Order
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <returns>Returns true if it found 'Approval Request For cloudBuy Purchase Order'</returns>
        public bool checkForApprovalMailConfirmation(IWebDriver driver)
        {
            bool status = false;
            try
            {
                if (driver.FindElement(By.XPath("//span[contains(text(),'Approval Request For cloudBuy " +
                    "Purchase Order')]")).Displayed)
                {
                    status = true;
                    driver.FindElement(By.XPath("//span[contains(text(),'Approval Request For cloudBuy " +
                        "Purchase Order')]")).Click();
                    normalWait();
                }
                else
                {
                    status = false;

                }

            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return status;
        }

        /// <summary>
        /// This method clicks on the link passed as argument 'linkText'
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="linkText">Link Text on whick click operation need to perform </param>
        public void clickOnDesiredLink(IWebDriver driver, String linkText)
        {
            try
            {
                if (getStatusCode(driver.Url).Equals("OK") ||
                    getStatusCode(driver.Url).Equals("Redirect"))
                {
                    clickOnWebElement(driver, new CommonPageWebElements(driver).
                        getDesiredMenuLinkWebElemnt(linkText));
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

        /// <summary>
        /// This method clicks on a webelement
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="elementToClick">Webelement to click</param>
        public void clickOnWebElement(IWebDriver driver, IWebElement elementToClick)
        {
            Console.WriteLine("Inside clickOnWebElement().........");
            try
            {
                elementToClick.Click();
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

        /// <summary>
        /// This method is designed to fetch text from an element of the web page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// <returns>It returns text from webelement</returns>
        public String getTextFromElement(IWebDriver driver, IWebElement element)
        {
            waitForPageLoad(driver);
            String text = "";
            try
            {
                if (getStatusCode(driver.Url).Equals("OK") ||
                    getStatusCode(driver.Url).Equals("Redirect"))
                {
                    text = element.Text;
                    Console.WriteLine("Text :" + text);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return text;
        }

        public IWebElement getWebElement(IWebDriver driver, String locatorType, String locator)
        {
            switch (locatorType)
            {
                case "Id":
                    return driver.FindElement(By.XPath(locator));
                case "Xpath":
                    Console.WriteLine("Locator Type : "+ locatorType +" --> "+locator);
                    return driver.FindElement(By.XPath(locator));
                default:
                    return null;
            }

        }

        /// <summary>
        /// This method returns true if the element is displayed in the web page
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="element">The webelement need to verify the appearance in the page</param>
        /// <returns>Returns true if the element appears in the page</returns>
        //
        public bool verifyPresenceOfElement(IWebDriver driver, IWebElement element)
        {
            Console.WriteLine("Inside verifyPresenceOfElement()-----------");
            bool status = false;
            try
            {
                if (element.Displayed)
                {
                    status = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caughtin verifyPresenceOfElement()`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
            return status;
        }

        /// <summary>
        ///It  Deletes all text present in the textbox 
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="textbox">Textbox to pass for clearing existing text</param>
        public void clearTextFromTextbox(IWebDriver driver, IWebElement textbox)
        {
            try
            {
                textbox.Clear();
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }
        /// <summary>
        /// This method enters data into Textbox
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="textBox">Textbox in which we need to enter data</param>
        /// <param name="data">Data to be entered in textbox</param>
        public void enterDataInTextBox(IWebDriver driver, IWebElement textBox, String data)
        {
            try
            {
                clearTextFromTextbox(driver, textBox);
                textBox.SendKeys(data);
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }
        
        /// <summary>
        /// Search purchase id in outlook mail and clicks it
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="purcId">Purchase id</param>
        public void searchInOutlookMail(IWebDriver driver, String dataToSearch)
        {
            try
            {
                normalWait();
                waitForPageLoad(driver);
                enterDataInTextBox(driver, driver.FindElement(By.XPath("//input[@placeholder=" +
                    "'Search']")),dataToSearch);
                clickOnWebElement(driver, driver.FindElement(By.XPath("//div[@id='searchBoxId']" +
                    "/following-sibling::button")));
                //driver.FindElement(By.XPath("//input[@placeholder='Search']")).SendKeys(dataToSearch);
                //driver.FindElement(By.XPath("//div[@id='searchBoxId']/following-sibling::button")).Click();
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught`````");
                takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }
        }

        /// <summary>
        /// Clicks on approve mail which contains provided purchase id
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="purcId">Purchase id</param>
        public void clickOnApproveMail(IWebDriver driver, String purcId)
        {
            driver.FindElement(By.XPath("//span[contains(text(),'Order " + purcId
                + " approved')]")).Click();
        }

        /// <summary>
        /// Clicks on 'OK' btn of browser pop up 
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        //This method designed to click on 'OK' btn of browser popup 
        public void clickOKForBrowserPopup(IWebDriver driver)
        {
            driver.SwitchTo().Alert().Accept();
        }

        /// <summary>
        /// Takes screenshot
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void takeScreenshot(IWebDriver driver, String callingMethodName)
        {
            Console.WriteLine("Inside takeScreenshot()---------------");
            String projPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            Console.WriteLine(DateTime.Now.ToString("dd-mm-yyyy HH_mm_ss"));
            ss.SaveAsFile(projPath + "\\ScreenShot\\" + callingMethodName  + DateTime.Now.ToString("yyyyMMMdd HH-mmtt") + ".png");
            Console.WriteLine(" takeScreenshot() Finished---------------");
        }

        /// <summary>
        /// Prints the browser log
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="logType"></param>
        public void PrintLogs(IWebDriver driver, string logType)
        {
            try
            {
                var browserLogs = driver.Manage().Logs.GetLog(logType);
                if (browserLogs.Count > 0)
                {
                    foreach (var log in browserLogs)
                    {
                        Console.WriteLine(">>>>>" + log.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error caught in PrintLogs()------> " + e.Message);
            }
        }

       

        /// <summary>
        /// Print all logs
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void PrintAllLogs(IWebDriver driver)
        {
            //PrintLogs(driver, LogType.Server);
            PrintLogs(driver, LogType.Browser);
            //PrintLogs(driver, LogType.Client);
            //PrintLogs(driver, LogType.Driver);
            //PrintLogs(driver, LogType.Profiler);
        }

    }
}
