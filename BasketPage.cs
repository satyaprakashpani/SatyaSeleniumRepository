using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApllications.PurchasingPageObjects
{
    class BasketPage
    {
        private IWebDriver _driver;
        private IWebElement returnToMainSiteBtn;
        private IWebElement emptyBasketBtn;
        private IWebElement basketItemName;
        private IWebElement basketQuantityTextbox;
        private IWebElement basketTotalPrice;
        private IWebElement basketGoToCheckoutBtn;

        public BasketPage(IWebDriver driver)
        {
            _driver = driver;
        }

       
        public IWebElement getReturnToMainSiteBtn()
        {
            returnToMainSiteBtn = _driver.FindElement(By.XPath("//span[contains(text(),'Return" +
                " to Main')]/.."));
            return returnToMainSiteBtn;
        }
        public IWebElement getEmptyBasketBtn()
        {
            emptyBasketBtn = _driver.FindElement(By.XPath("//span[contains(text(),'Empty Basket')]/.."));
            return emptyBasketBtn;
        }
        public IWebElement getBasketItemName()
        {
            basketItemName = _driver.FindElement(By.TagName("dt"));
            return basketItemName;
        }
        public IWebElement getBasketQuantityTextbox()
        {
            basketQuantityTextbox = _driver.FindElement(By.XPath("//form//input[@type='text']"));
            return basketQuantityTextbox;
        }
        public IWebElement getBasketTotalPrice()
        {
            basketTotalPrice = _driver.FindElement(By.XPath("//li[contains(text(),'BASKET " +
                    "TOTAL')]/following-sibling::li[4]"));
            return basketTotalPrice;
        }
        public IWebElement getBasketGoToCheckoutBtn()
        {
            basketGoToCheckoutBtn = _driver.FindElement(By.XPath("//a//span[contains(text()," +
                "'Go to Checkout')]"));
            return basketGoToCheckoutBtn;
        }
    }
}
