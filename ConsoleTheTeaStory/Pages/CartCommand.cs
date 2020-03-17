using OpenQA.Selenium;
using System;

namespace ConsoleTheTeaStory.Pages
{
    public class CartCommand
    {
        #region Locators
        private By locatorFrameLayer => By.ClassName("style-j56p2zs4iframe");

        #endregion

        #region IWebElements
        private IWebElement ItemLabel => Driver.Instance.FindElement(By.Id("item_sku_1"));
        private IWebElement FrameLayer => Driver.Instance.FindElement(locatorFrameLayer);

        #endregion

        public bool IsItemAdded(string itemName)
        {
            bool isFound = false;

            Helper.WaitUntilElementExists(locatorFrameLayer, 60);

            Driver.Instance.SwitchTo().Frame(FrameLayer);

            try
            {
                if (ItemLabel.Text.Equals(itemName))
                    isFound = true;
            }
            catch (Exception ex)
            {
                Helper.TakeErrorScreenshot();
                throw new Exception($"Item Label webElement is not found in Cart Side Iframe. \n\nDetails: {ex.Message}");
            }
            

            Driver.Instance.SwitchTo().DefaultContent();


            return isFound;
        }
    }
}
