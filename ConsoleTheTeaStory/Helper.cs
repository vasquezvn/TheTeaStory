﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace ConsoleTheTeaStory
{
    public class Helper
    {
        private static string LogsPath = @"C:\Users\ivan.vasquez\source\repos\TheTeaStory\ConsoleTheTeaStory\Logs";

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();

            return random.Next(min, max);
        }

        public static void WaitForElementLoad(IWebDriver driver, By by, int timeoutInSeconds)
        {
            if(timeoutInSeconds > 0)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
                {
                    return d.FindElement(by);
                });
            }
        }

        public static void LogErrors(string assertMessage, string customMessage)
        {
            var logPathName = LogsPath + @"\testLogs.txt";
            try
            {
                if(File.Exists(logPathName))
                {
                    using (var writer = new StreamWriter(logPathName, true))
                    {
                        writer.Write($"{DateTime.Now} / {assertMessage} \n {customMessage} \n");
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(logPathName))
                    {
                        writer.Write($"{DateTime.Now} / {assertMessage} \n {customMessage} \n");
                    }
                }
                
            }catch(Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} / {ex.Message} \n Error at moment to write in file");
            }
        }

        public static void TakeErrorScreenshot()
        {
            var logPathName = LogsPath + @"\ErrorScreenshot_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            ((ITakesScreenshot)Driver.Instance).GetScreenshot().SaveAsFile(logPathName, ScreenshotImageFormat.Png);
        }

        /*       public static void Wait(int time)
               {
                   Thread thread = new Thread(delegate ()
                   {
                       System.Threading.Thread.Sleep(time);
                   });
                   thread.Start();
                   while (thread.IsAlive)
                       Application.DoEvents();
               }*/
    }
}
