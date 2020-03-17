using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleTheTeaStory;
using ConsoleTheTeaStory.Pages;

namespace TheTeaStory
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Init()
        {
            Driver.Initialize();
        }

        [TestMethod]
        public void CanAddItemToCart()
        {
            HomePage.GoTo();
            HomePage.GoToTeaOption(HomePage.TeaOptions.ClassicBlends);

            ClassicBlendsPage.SetQuantity(3)
                .ClickAddToCart();

            SidePanelCartPage.ClickViewCart();

            Assert.IsTrue(CartPage.IsItemAdded("Classic Blends"), "Item has no been added to shopping cart");
        }

        [TestMethod]
        public void VerifyQuantityInCart()
        {
            HomePage.GoTo();
            HomePage.GoToTeaOption(HomePage.TeaOptions.ClassicBlends);

            var quantity = Helper.RandomNumber(1, 100);

            ClassicBlendsPage.SetQuantity(quantity)
                .ClickAddToCart();

            Assert.IsTrue(SidePanelCartPage.IsQuantityCorrect(quantity), "Introduced quantity doesn't match with quantity in cart.");
        }

        [TestMethod]
        public void VerifyData()
        {
            Helper.GetData();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Close();
        }
    }
}
