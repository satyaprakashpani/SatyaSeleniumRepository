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
    class LoginPageScn
    {
        GenericLib generalLib = new GenericLib();
        /// <summary>
        /// This method designed to Login into Purchasing portal
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        public void loginToPurchasing(IWebDriver driver, String userName, String password)
        {
            LoginPage loginPage = new LoginPage(driver);
            
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    generalLib.enterDataInTextBox(driver,loginPage.getUserName(),userName);
                    generalLib.enterDataInTextBox(driver, loginPage.getPassword(), password);
                    generalLib.clickOnWebElement(driver, loginPage.getLoginBtn());
                }
                else
                {
                    Console.WriteLine("Status Code is not OK or Redirect");
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("`````Exception caught loginToPurchasing()`````");
               generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(exp.Message);
            }

        }

        //public void loginToPurchasingForApprover(IWebDriver driver)
        //{
        //    try
        //    {
        //        if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
        //            generalLib.getStatusCode(driver.Url).Equals("Redirect"))
        //        {
        //            driver.FindElement(By.Id("UserName")).SendKeys("test.approver10");
        //            driver.FindElement(By.Id("password")).SendKeys("Orange10");
        //            driver.FindElement(By.XPath("//input[@value='Log In']")).Click();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        Console.WriteLine("`````Exception caught`````");
        //       generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
        //        generalLib.PrintAllLogs(driver);
        //        Console.WriteLine(exp.Message);
        //    }
        //}

        /// <summary>
        /// Verify account recovery functionality and returns true if mail checked successfully
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="choice">Username/Email</param>
        /// <returns>Returns true if mail checked successfully</returns>
        public bool verifyAccountRecovery(IWebDriver driver, String choice)
        {
            driver.Manage().Cookies.DeleteAllCookies();
            generalLib.openLoginURLForPurchasing(driver);
            bool status = false;
            LoginPage loginPgEle = new LoginPage(driver);
            switch (choice.ToLower())
            {
                case "username":
                    Console.WriteLine(".....Inside username case....");
                    try
                    {
                        generalLib.waitForPageLoad(driver);
                        if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                            generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                        {
                            generalLib.clickOnWebElement(driver,loginPgEle.getForgotPasswd());
                            generalLib.waitForPageLoad(driver);
                            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                                generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                            {
                                AccountRecoveryPage accRecvPgEle = new AccountRecoveryPage(driver);
                                if (generalLib.verifyPresenceOfElement(driver,accRecvPgEle.getUserNameTextBox()))
                                {
                                    accRecvPgEle.getUserNameTextBox().SendKeys("test.requisitioner10");
                                    accRecvPgEle.getSendRecoveryEMailBtnUserName().Click();
                                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                                         generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                                    {
                                        RecoverySubmittedPage recovrySubmitPgEle = new 
                                            RecoverySubmittedPage(driver);
                                        if (recovrySubmitPgEle.accRecovInitiateHeader.Displayed)
                                        {
                                            recovrySubmitPgEle.hereLink.Click();//here link click
                                            generalLib.normalWait(2000);
                                            generalLib.openOutLookUrl(driver);
                                            generalLib.loginToOutlook(driver);
                                            if (generalLib.verifyRSetPwdByUsrName(driver))
                                            {
                                                status = true;
                                            }
                                            generalLib.logOutFromOutLook(driver);
                                            generalLib.normalWait();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Account recovery initiated dalog box not" +
                                                " appeared");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Username field is not appeared");
                                }
                            }
                        }
                    }
                    catch (Exception exp)
                    {
                        status = false;
                        Console.WriteLine("`````Exception caught`````");
                       generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name);//Take screenshot and the file saved in SreenShot folder
                        generalLib.PrintAllLogs(driver);
                        Console.WriteLine(exp);
                    }
                    return status;
                    
                case "email":
                    Console.WriteLine("---Inside email case---");
                    try
                    {
                        generalLib.waitForPageLoad(driver);
                        if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                            generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                        {
                            generalLib.clickOnWebElement(driver,loginPgEle.getForgotPasswd());
                            generalLib.waitForPageLoad(driver);
                            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                            generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                            {
                                AccountRecoveryPage accRecovPgEle = new AccountRecoveryPage(driver);
                                if (accRecovPgEle.getIknowMyEmailLink().Displayed)
                                {
                                    accRecovPgEle.getIknowMyEmailLink().Click();
                                    generalLib.normalWait(2000);
                                    accRecovPgEle.getEmailTextBox().SendKeys("satyaprakash.pani@" +
                                        "cloudbuy.com");
                                    accRecovPgEle.getSendRecoveryEMailBtnEmail().Click();
                                    if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                                         generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                                    {
                                        RecoverySubmittedPage recovSubmitPg = new RecoverySubmittedPage
                                            (driver);
                                        if (recovSubmitPg.accRecovInitiateHeader.Displayed)
                                        {
                                            recovSubmitPg.hereLink.Click();
                                            generalLib.normalWait(2000);
                                            generalLib.openOutLookUrl(driver);
                                            generalLib.loginToOutlook(driver);
                                            if (generalLib.verifyRSetPwdByEmail(driver))
                                            {
                                                status = true;
                                            }
                                            generalLib.logOutFromOutLook(driver);
                                        }
                                        else
                                            Console.WriteLine("Account recovery dialogbox not appeared");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exp)
                    {
                        status = false;
                        Console.WriteLine("`````Exception caught`````");
                       generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name);//Take screenshot and the file saved in SreenShot folder
                        generalLib.PrintAllLogs(driver);
                        Console.WriteLine(exp);
                        return status;
                    }
                    return status;
                default:
                    Console.WriteLine("Choice is different from Email/Username");
                    return status;
            }
        }

    }
}
