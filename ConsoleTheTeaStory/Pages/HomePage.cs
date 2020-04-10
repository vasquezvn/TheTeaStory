using OpenQA.Selenium;
using System;

namespace ConsoleTheTeaStory.Pages
{
    public class HomePage
    {
        public enum TeaOptions
        {
            ClassicBlends,
            SignatureBlends,
            ChaiBlends,
            DessertBlends,
        }

        #region IWebRegion
        private static IWebElement ClassicBlendsOption => Driver.Instance.FindElement(By.XPath("//div[@id='comp-jqyzu7u6']//h3[@class='_2BULo'][text()='Classic Blends']"));
        private static IWebElement QuickViewClassicBlendOption => Driver.Instance.FindElement(By.XPath("//div[@class='slick-slide slick-active slick-current']/div/div[@class='_2cw7M']/div/div[@class='_2zTHN _2AHc6']/a[@class='_34sIs']/div[@class='_3-5SE heightByImageRatio heightByImageRatio2']/button[@class='_3ezRD']"));
        private static IWebElement TextClassicBlendOption => Driver.Instance.FindElement(By.XPath("//div[@class='slick-slide slick-active slick-current']/div/div[@class='_2cw7M']/div/div[@class='_2zTHN _2AHc6']/a[@class='_34sIs']/div[@class='_3RqKm']/h3[@class='_2BULo']"));
        #endregion


        public static void GoTo()
        {
            Driver.Instance.Navigate().GoToUrl("https://www.theteastory.co/");
        }

        public static void GoToTeaOption(TeaOptions option)
        {
            switch(option)
            {
                case TeaOptions.ClassicBlends:
                    try
                    {
                        //ClassicBlendsOption.Click();
                        TextClassicBlendOption.Click();
                    }
                    catch (Exception ex)
                    {
                        Helper.TakeErrorScreenshot();
                        throw new Exception($"Classic Blend Option webElement is not found in Home Page \n\nDetails: {ex.Message}");
                    }

                    break;
            }
        }
    }
}
