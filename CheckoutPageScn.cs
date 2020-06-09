using CBApllications.GeneralLib;
using CBApllications.PurchasingPageObjects;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CBApllications.ScenarioLib
{
    class CheckoutPageScn
    {
        GenericLib generalLib = new GenericLib();

        /// <summary>
        /// This method returns Order no in String format from checkout Page
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public String getOrderNoFromCheckoutPage(IWebDriver driver)
        {
            String data = generalLib.getTextFromElement(driver, new CheckoutReviewPage(driver)
                .getOrderNoInCheckoutReview());
            String[] dataArr = data.Split(' ');
            Console.WriteLine("Ord no : " + dataArr[dataArr.Length - 1]);
            return dataArr[dataArr.Length - 1];
        }

        /// <summary>
        /// This method clicks on Complete Order button
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        public void clickOnCompleteOrderBtn(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.XPath("//input[@type='submit' and @alt='COMPLETE ORDER']")).Click();
                generalLib.waitForPageLoad(driver);
                Console.WriteLine("Complete Order btn clicked");
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
        /// This method returns a list of elements like Item name, quantity, total amount
        /// from check out page
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public List<String> getDataFromChkOutPage(IWebDriver driver)
        {
            Console.WriteLine("Inside getDataFromChkOutPage()............................");
            List<String> chkOutData = new List<string>();
            CheckoutReviewPage checkoutPgElements = new CheckoutReviewPage(driver);
            try
            {
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                    generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    chkOutData.Add(generalLib.getTextFromElement(driver, checkoutPgElements
                        .getItemNameCheckoutReview()));
                    chkOutData.Add(generalLib.getTextFromElement(driver, checkoutPgElements
                        .getQuantityCheckoutReview()));
                    chkOutData.Add(generalLib.getTextFromElement(driver, checkoutPgElements
                        .getTotalCheckoutReview()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("`````Exception caught`````");
                generalLib.takeScreenshot(driver, MethodBase.GetCurrentMethod().Name); //Take screenshot and the file saved in SreenShot folder
                generalLib.PrintAllLogs(driver);
                Console.WriteLine(ex.Message);

            }
            return chkOutData;
        }

        /// <summary>
        /// This method selects gl code in checkout/PurchasingCodeForm.aspx page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="glCode"></param>
        public void selectGLCode(IWebDriver driver, String glCode)
        {
            PurchasingCodeFormPage purCodeFormpgElements = new PurchasingCodeFormPage(driver);
            generalLib.selectByVisibleText(driver, purCodeFormpgElements.getGLCodeSelectBox(), glCode);
            generalLib.clickOnWebElement(driver, purCodeFormpgElements.getPurCodeFormSubmitBtn());
        }
    }
}
