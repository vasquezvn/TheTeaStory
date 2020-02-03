using OpenQA.Selenium;

namespace ConsoleTheTeaStory.Pages
{
    public class CartPage
    {
        private static IWebElement ItemLabel => Driver.Instance.FindElement(By.Id("item_sku_1"));
        private static IWebElement FrameLayer => Driver.Instance.FindElement(By.ClassName("style-j56p2zs4iframe"));

        public static bool IsItemAdded(string itemName)
        {
            bool isFound = false;
            Driver.Instance.SwitchTo().Frame(FrameLayer);

            if (ItemLabel.Text.Equals(itemName))
                isFound = true;

            Driver.Instance.SwitchTo().DefaultContent();
            return isFound;
        }
    }
}
