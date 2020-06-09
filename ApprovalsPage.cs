using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApllications.PurchasingPageObjects
{
    class ApprovalsPage
    {
        private IWebDriver _driver;
        private IWebElement orderApprovalsBtn;
        private IWebElement invoiceApprovalsBtn;

        public ApprovalsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement getOrderApprovalsBtn()
        {
            orderApprovalsBtn = _driver.FindElement(By.XPath("//div[@id='appColumnContainer']" +
                "//a[contains(text(),'Order approvals')]"));
            return orderApprovalsBtn;
        }

        public IWebElement getInvoiceApprovalsBtn()
        {
            invoiceApprovalsBtn = _driver.FindElement(By.XPath("//div[@id='appColumnContainer']" +
                "//a[contains(text(),'Invoice approvals')]"));
            return invoiceApprovalsBtn;
        }

    }
}
