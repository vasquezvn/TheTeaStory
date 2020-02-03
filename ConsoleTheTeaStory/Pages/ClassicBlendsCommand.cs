using OpenQA.Selenium;

namespace ConsoleTheTeaStory.Pages
{
    public class ClassicBlendsCommand
    {
        private int quantity;

        #region IWebElemets
        private static IWebElement QuantityTxtBox => Driver.Instance.FindElement(By.XPath("//div[@class='_1ZOt- cell']/div/div/div[@class='_2uERl']/input"));
        private static IWebElement AddToCartBtn => Driver.Instance.FindElement(By.XPath("//span[@class='buttonnext2195568898--content']/div[@class='StatesButton656985045--text']"));
        private static IWebElement ViewCartBtn => Driver.Instance.FindElement(By.Id("widget-view-cart-button"));
        private static IWebElement IFrameLayer => Driver.Instance.FindElement(By.ClassName("s_yOSHETPAPopupSkiniframe"));
        #endregion

        public ClassicBlendsCommand(int quantity)
        {
            this.quantity = quantity;
        }

        public ClassicBlendsCommand ClickAddToCart()
        {
            QuantityTxtBox.Clear();
            QuantityTxtBox.SendKeys(quantity.ToString());

            System.Threading.Thread.Sleep(1000);
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
