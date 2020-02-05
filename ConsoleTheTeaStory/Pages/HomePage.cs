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
        private static IWebElement ClassicBlendsOption => Driver.Instance.FindElement(By.XPath("//div[@id='comp-jqyzu7u6']//h3[@class='_2BULo'][text()='Classic Blends']"));
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
