using OpenQA.Selenium;

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

        #region
        private static IWebElement ClassicBlendsOption => Driver.Instance.FindElement(By.XPath("//div[@id='comp-jqyzu7u6']/div[@class='comp-jqyzu7u6']/div[@class='_2DDgw']/div[@class='_3e4dm']/div[@class='slick-slider aM2rn slick-initialized']/div[@class='slick-list']/div[@class='slick-track']/div[@class='slick-slide slick-active slick-current']/div/div[@class='_2cw7M']/div/a[@class='_2zTHN _2AHc6']/div[@class='_3RqKm']/h3[@class='_2BULo']"));
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
                    ClassicBlendsOption.Click();
                    break;
            }
        }
    }
}
