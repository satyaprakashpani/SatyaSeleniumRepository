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
    class TableFilterScn
    {
        GenericLib generalLib = new GenericLib();
        ApplicationRelatedLib applicationLib = new ApplicationRelatedLib();

        /// <summary>
        /// This method returns true if filtered Ord Id is equal with Ord no after filter
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="ordNo">Actual Order no need to compare with the ord no extracted 
        /// from order refs text </param>
        /// <returns></returns>
        public bool verifyOrdRefFilterBtn(IWebDriver driver, String ordNo)
        {
            bool status = false;
            try
            {
                generalLib.filterDataWithDifferentOption(driver, ordNo, "Contains...", "Order refs");
                generalLib.waitForPageLoad(driver);
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    String actOrdId = applicationLib.getOrderIdFromDisplayOrdPage(generalLib.getTextFromElement(driver,
                        new DisplayOrdersPage(driver).getOrderRefText()));
                    if (actOrdId.Equals(ordNo))
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
            generalLib.normalWait(2000);
            generalLib.openURL(driver, "https://purchasing.uk-plc.net/BuyerDotNet2/ordering/" +
                "displayorders.aspx?");
            generalLib.waitForPageLoad(driver);
            return status;
        }
        /// <summary>
        /// This method returns true if filtered Ord Id is equal with Ord no after filter
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool verifyRequisitionerFilterBtn(IWebDriver driver, String data)
        {
            bool status = false;
            try
            {
                generalLib.filterDataWithDifferentOption(driver, data, "Does not contain...", "Requisitioner");
                generalLib.waitForPageLoad(driver);
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    ReadOnlyCollection<IWebElement> filteredData = driver.FindElements
                        (By.XPath("//tbody//tr"));
                    Console.WriteLine("No of Row : " + filteredData.Count);
                    if (filteredData.Count < 2)
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
            //generalLib.clickOnRemoveFilterOption(driver, "Requisitioner");
            generalLib.openURL(driver, "https://purchasing.uk-plc.net/BuyerDotNet2/ordering/" +
               "displayorders.aspx?");
            generalLib.waitForPageLoad(driver);
            generalLib.normalWait(2000);
            return status;
        }

        /// <summary>
        /// This method returns true if all filtered datas in supplier name field are equal 
        /// with data provided to filter
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool verifySupplierNameFilterBtn(IWebDriver driver, String data)
        {
            bool status = false;
            try
            {
                generalLib.filterDataWithDifferentOption(driver, data, "Equals...", "Supplier name");
                generalLib.waitForPageLoad(driver);
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    if (generalLib.verifyAllFilteredDataByNavigatingPagintaion(driver, data))
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
            generalLib.openURL(driver, "https://purchasing.uk-plc.net/BuyerDotNet2/ordering/" +
                "displayorders.aspx?");
            generalLib.waitForPageLoad(driver);
            return status;
        }
        /// <summary>
        /// This method returns true if all filtered datas in status field are equal 
        /// with data provided to filter
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool verifyStatusFilterBtn(IWebDriver driver, String data)
        {
            bool status = false;
            try
            {
                generalLib.filterDataWithDifferentOption(driver, data, "Begins with...", "Status");
                //filterWithStatusFltrBtn(driver, data);
                generalLib.waitForPageLoad(driver);
                if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
                {
                    if (generalLib.verifyAllFilteredDataByNavigatingPagintaion(driver, data))
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
            generalLib.openURL(driver, "https://purchasing.uk-plc.net/BuyerDotNet2/ordering/" +
               "displayorders.aspx?");
            generalLib.waitForPageLoad(driver);
            return status;
        }

        /// <summary>
        /// This method clicks on the remove filter option by taking col name/ filter btn name as input
        /// </summary>
        /// <param name="driver">Webdriver Object to drive the execution</param>
        /// <param name="filterBtnName">Filter btn name/Column name</param>
        public void clickOnRemoveFilterOption(IWebDriver driver, String filterBtnName)
        {
            FilterWebElement filterWbElements = new FilterWebElement(driver);
            try
            {
                generalLib.clickOnWebElement(driver, filterWbElements.getFilterBtn(filterBtnName));
                generalLib.normalWait(1000);
                generalLib.clickOnWebElement(driver, filterWbElements.getRemoveFilterBtn());
                generalLib.normalWait(2000);
                Console.WriteLine("Remove Filter option clicked for '" + filterBtnName + "' filter button");
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
