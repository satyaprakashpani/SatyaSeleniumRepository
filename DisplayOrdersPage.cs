using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApllications.PurchasingPageObjects
{
    
    class DisplayOrdersPage
    {
        private IWebDriver _driver;
        private IWebElement actionsDropdown;
        private IWebElement actionsSelectBtn;
        private IWebElement displayOrdersPageTableBody;
        private IWebElement orderRefText;
        public DisplayOrdersPage(IWebDriver driver)
        {
            _driver = driver;
            
           
        }
        /// <summary>
        /// This method is designed to get action dropdown
        /// </summary>
        /// <returns>Returns webelement action dropdown</returns>
        public IWebElement getActionDropdown()
        {
            actionsDropdown = _driver.FindElement(By.XPath("//tbody[@id='tableBody1285']//select"));
            return actionsDropdown;
        }
        /// <summary>
        /// This method is designed to get action select button
        /// </summary>
        /// <returns></returns>
        public IWebElement getActionSelectBtn()
        {
            actionsSelectBtn = _driver.FindElement(By.Name("submit"));
            return actionsSelectBtn;
        }
        public IWebElement getDisplayOrdersPageTableBody()
        {
            displayOrdersPageTableBody = _driver.FindElement(By.Id("tableBody1285"));
            return displayOrdersPageTableBody;
        }
        public IWebElement getOrderRefText()
        {
            orderRefText = _driver.FindElement(By.XPath("//span[contains(text(),'Order:')]"));
            return orderRefText;
        }
        
    }
}
