using OpenQA.Selenium;
using System;

namespace ConsoleTheTeaStory.Pages
{
    public class ClassicBlendsCommand
    {
        private int quantity;

        #region Locators
        private By locatorQuantityTxtBox => By.XPath("//*[@type='number']");
        private By locatorAddToCartBtn => By.XPath("//div[@class='_3j0qu cell']");

        #endregion

        #region IWebElemets
        private IWebElement QuantityTxtBox => Driver.Instance.FindElement(locatorQuantityTxtBox);
        private IWebElement AddToCartBtn => Driver.Instance.FindElement(locatorAddToCartBtn);

        #endregion

        public ClassicBlendsCommand() { }

        public ClassicBlendsCommand SetQuantity(int quantity)
        {
            this.quantity = quantity;

            Helper.WaitUntilElementVisible(locatorQuantityTxtBox);

            try
            {
                QuantityTxtBox.Clear();
                QuantityTxtBox.SendKeys(quantity.ToString());
            }
            catch (Exception ex)
            {
                Helper.TakeErrorScreenshot();
                throw new Exception($"Quantity text box webElement is not found in ClassicBlendsPage \n\nDetails: {ex.Message}");
            }
            

            return this;
        }

        public ClassicBlendsCommand ClickAddToCart()
        {
            Helper.WaitUntilElementClickable(locatorAddToCartBtn);

            try
            {
                AddToCartBtn.Click();
                AddToCartBtn.Click();
            }
            catch (Exception ex)
            {
                Helper.TakeErrorScreenshot();
                throw new Exception($"Add button webElement is not found in ClassicBlendsPage \n\nDetails: {ex.Message}");
            }

            return this;
        }

        
    }
}
