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
    class ApprovalsScn
    {
        GenericLib generalLib = new GenericLib();
        ApplicationRelatedLib applicationLib = new ApplicationRelatedLib();

        /// <summary>
        /// This method selects Approve/Reject radio button
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="action"></param>
        public void approveOrRejectAnOrder(IWebDriver driver, String action, String purchaseOrdId)
        {
            ViewApprovalsPage viewApprvalPageElements = new ViewApprovalsPage(driver);
            if (generalLib.getStatusCode(driver.Url).Equals("OK") ||
                   generalLib.getStatusCode(driver.Url).Equals("Redirect"))
            {
                try
                {
                    generalLib.waitForPageLoad(driver);
                    generalLib.clickOnWebElement(driver, new CommonPageWebElements(driver).
                           getDesiredMenuLinkWebElemnt("Orders that require your approval"));
                    generalLib.filterDataWithDifferentOption(driver, purchaseOrdId, "Equals...",
                        "Purchase order ID");
                    generalLib.waitForPageLoad(driver);
                    switch (action.ToLower())
                    {
                        case "approve":
                            generalLib.clickOnWebElement(driver, viewApprvalPageElements
                                .getApproveRadioBtn());
                            generalLib.clickOnWebElement(driver, viewApprvalPageElements
                                .getApproveRejectSubmitBtn());
                            break;

                        case "reject":
                            generalLib.clickOnWebElement(driver, viewApprvalPageElements
                                .getRejectRadioBtn());
                            generalLib.clickOnWebElement(driver, viewApprvalPageElements
                                .getApproveRejectSubmitBtn());
                            break;
                        default:
                            Console.WriteLine("Data not provided for what to select");
                            break;
                    }
                    generalLib.clickOKForBrowserPopup(driver);
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
    }
}
