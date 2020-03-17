using OpenQA.Selenium;
using System;

namespace ConsoleTheTeaStory.Pages
{
    public class SidePanelCartCommand
    {
        private By locatorIFrameLayer => By.ClassName("s_yOSHETPAPopupSkiniframe");
        private By locatorQuantityLabel => By.ClassName("item-quantity");

        #region IWebElements
        private IWebElement IFrameLayer => Driver.Instance.FindElement(locatorIFrameLayer);
        private IWebElement ViewCartBtn => Driver.Instance.FindElement(By.Id("widget-view-cart-button"));
        private IWebElement quantityLabel => Driver.Instance.FindElement(locatorQuantityLabel);

        #endregion

        public void ClickViewCart()
        {
            Helper.WaitUntilElementExists(locatorIFrameLayer, 60);

            try
            {
                Driver.Instance.SwitchTo().Frame(IFrameLayer);
            }
            catch (Exception ex)
            {
                Helper.TakeErrorScreenshot();
                throw new Exception($"IFrame webElement is not found in ClassicBlendsPage Side Bar \n\nDetails: {ex.Message}");
            }


            try
            {
                ViewCartBtn.Click();
            }
            catch (Exception ex)
            {
                Helper.TakeErrorScreenshot();
                throw new Exception($"ViewCart button webElement is not found in ClassicBlendsPage \n\nDetails: {ex.Message}");
            }

            Driver.Instance.SwitchTo().ParentFrame();
        }

        public bool IsQuantityCorrect(int quantity)
        {
            Helper.WaitUntilElementVisible(locatorIFrameLayer, 60);

            var result = false;
            var quantityLabelNumber = 0;
            var quantityLabelString = string.Empty;

            try
            {
                Driver.Instance.SwitchTo().Frame(IFrameLayer);
            }
            catch (Exception ex)
            {
                Helper.TakeErrorScreenshot();
                throw new Exception($"Iframe webElement is not found in Side Panel Cart \n\nDetails: {ex.Message}");
            }
            
            Helper.WaitUntilElementVisible(locatorQuantityLabel);

            try
            {
                quantityLabelString = quantityLabel.Text.Split()[1];
            }
            catch (Exception ex)
            {
                Helper.TakeErrorScreenshot();
                throw new Exception($"Qhantity label webElement is not found in Side Panel Cart \n\nDetails: {ex.Message}");
            }
            
            try
            {
                quantityLabelNumber = Int32.Parse(quantityLabelString);
            }
            catch (Exception ex)
            {
                Helper.TakeErrorScreenshot();
                throw new Exception($"Quantity label can't be parsed to int in Side Panel Cart \n\nDetails: {ex.Message}");
            }

            if (quantity == quantityLabelNumber)
                result = true;

            Driver.Instance.SwitchTo().ParentFrame();

            return result;
        }
    }
}
