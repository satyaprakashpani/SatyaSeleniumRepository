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
    class ContactDetailsScn
    {
        GenericLib generalLib = new GenericLib();
        ApplicationRelatedLib applicationLib = new ApplicationRelatedLib();

        /// <summary>
        /// This method designed to navigate contact details page via left hand side menu
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void navigateToContactDetailPage(IWebDriver driver)
        {
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    applicationLib.clickOnMenuSwipeBtn(driver);
                    generalLib.clickOnWebElement(driver, new CommonPageWebElements(driver).
                        getDesiredMenuLinkWebElemnt("Contact details"));
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
        /// This method adds Gl Code in contact details page
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="glCode">Name of Gl Code  entered to Add </param>
        /// <returns>Returns true if it found successful message</returns>
        public bool verifyaddGlCodeInContDetailsPage(IWebDriver driver, String glCode)
        {
            
            bool status = false;
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                  generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                try
                {
                    ContactDetailsPage contDetailsPageElement = new ContactDetailsPage(driver);
                    contDetailsPageElement.glCodeTextBox.Clear();
                    generalLib.enterDataInTextBox(driver, contDetailsPageElement.glCodeTextBox, glCode);
                    generalLib.clickOnWebElement(driver, contDetailsPageElement.glCodeSaveButton);
                    generalLib.normalWait(2000);
                    if (driver.FindElement(By.Id("successMessage")).Displayed)
                    {
                        String sMsg = driver.FindElement(By.XPath("//div[@id='successMessage']/p")).Text;
                        Console.WriteLine("Successul Msg : " + sMsg);
                        if (sMsg.Equals("Codes updated."))
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
