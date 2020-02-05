using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace ConsoleTheTeaStory
{
    public static class Driver
    {
        public static IWebDriver Instance { get; set; }
        public static void Initialize()
        {
            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(Path.GetFullPath(Path.Combine(currentDir, @"..\..\..\ConsoleTheTeaStory\Resources")));
            var pathDir = directory.ToString();

            Instance = new ChromeDriver(pathDir);
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
