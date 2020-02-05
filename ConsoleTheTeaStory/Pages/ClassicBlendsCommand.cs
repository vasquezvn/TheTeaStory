using OpenQA.Selenium;

namespace ConsoleTheTeaStory.Pages
{
    public class ClassicBlendsCommand
    {
        private int quantity;

        #region IWebElemets
        private static IWebElement QuantityTxtBox => Driver.Instance.FindElement(By.XPath("//*[@type='number']"));
        private static IWebElement AddToCartBtn => Driver.Instance.FindElement(By.XPath("//*[@type='button']"));
        private static IWebElement ViewCartBtn => Driver.Instance.FindElement(By.Id("widget-view-cart-button"));
        private static IWebElement IFrameLayer => Driver.Instance.FindElement(By.ClassName("s_yOSHETPAPopupSkiniframe"));
        #endregion

        public ClassicBlendsCommand(int quantity)
        {
            this.quantity = quantity;
        }

        public ClassicBlendsCommand ClickAddToCart()
        {
            Helper.WaitForElement(QuantityTxtBox, 10.0);
            QuantityTxtBox.Clear();
            QuantityTxtBox.SendKeys(quantity.ToString());

            Helper.WaitForElement(AddToCartBtn, 10.0);

            AddToCartBtn.Click();

            return this;
        }

        public void ClickViewCart()
        {
            Driver.Instance.SwitchTo().Frame(IFrameLayer);
            ViewCartBtn.Click();
            Driver.Instance.SwitchTo().ParentFrame();
            //Driver.Instance.SwitchTo().DefaultContent();
        }
    }
}
