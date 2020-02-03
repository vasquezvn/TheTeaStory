using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ConsoleTheTeaStory
{
    public static class Driver
    {
        public static IWebDriver Instance { get; set; }
        public static void Initialize()
        {
            Instance = new ChromeDriver(@"C:\Users\ivan.vasquez\Libraries\Selenium");
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Instance.Manage().Window.Maximize();
        }

        public static void Close()
        {
            Instance.Close();
        }

        public static void SetURL(string url)
        {
            Instance.Url = url;
        }
    }
}
