using OpenQA.Selenium;
using System;

namespace ConsoleTheTeaStory.Pages
{
    public class ClassicBlendsPage
    {
        private static IWebElement quantityLabel => Driver.Instance.FindElement(By.ClassName("item-quantity"));
        private static IWebElement IFrameLayer => Driver.Instance.FindElement(By.ClassName("s_yOSHETPAPopupSkiniframe"));

        public static ClassicBlendsCommand SetQuantity(int quantity)
        {
            return new ClassicBlendsCommand(quantity);
        }

        public static bool IsQuantityCorrect(int quantity)
        {
            Driver.Instance.SwitchTo().Frame(IFrameLayer);
            //Helper.WaitForElementLoad(Driver.Instance, By.ClassName("item-quantity"), 10);
            System.Threading.Thread.Sleep(1000);

            var quantityLabelString = quantityLabel.Text.Split()[1];
            var result = false;
            var quantityLabelNumber = 0;

            try
            {
                quantityLabelNumber = Int32.Parse(quantityLabelString);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{quantityLabelString}'");
            }

            if (quantity == quantityLabelNumber)
                result = true;

            Driver.Instance.SwitchTo().ParentFrame();

            return result;
        }
    }
}
