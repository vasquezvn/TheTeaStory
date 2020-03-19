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
        public void VerifyInsertDataOnClientTable()
        {
            var name = Helper.RandomString(5, true);
            var lastname = Helper.RandomString(5, true);
            var email = $"{Helper.RandomString(5, true)}@test.com";
            var programm = Helper.RandomString(10, true);

            bool isInserted = Helper.InsertToClients(name, lastname, email, programm);

            Assert.IsTrue(isInserted, "Values were not inserted on Client Table");
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
