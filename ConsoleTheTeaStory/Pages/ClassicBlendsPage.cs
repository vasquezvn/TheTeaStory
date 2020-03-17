using OpenQA.Selenium;
using System;

namespace ConsoleTheTeaStory.Pages
{
    public class ClassicBlendsPage
    {

        public static ClassicBlendsCommand SetQuantity(int quantity)
        {
            return new ClassicBlendsCommand().SetQuantity(quantity);
        }
    }
}
