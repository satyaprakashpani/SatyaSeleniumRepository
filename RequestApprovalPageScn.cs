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
    
    class RequestApprovalPageScn
    {
        GenericLib generalLib = new GenericLib();
        /// <summary>
        /// Enter data for request approval and click on Request Approval btn
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void enterRequestApprovalMsgAndClickBtn(IWebDriver driver, String message)
        {
            RquestApprovalPage reqAppPageElements = new RquestApprovalPage(driver);
            try
            {
                generalLib.waitForPageLoad(driver);
                generalLib.enterDataInTextBox(driver,reqAppPageElements.getRequestApprovalTextBox(),message);
                //driver.FindElement(By.Id("strApprovalMessage")).SendKeys(message);
                //driver.FindElement(By.Id("RequestApproval")).Click();
                generalLib.clickOnWebElement(driver, reqAppPageElements.getRequestApprovalSubmitBtn());
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
