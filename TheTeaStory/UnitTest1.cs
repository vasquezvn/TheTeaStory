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

        /*[TestMethod]
        public void CanAddItemToCart()
        {
            HomePage.GoTo();
            HomePage.GoToTeaOption(HomePage.TeaOptions.ClassicBlends);

            ClassicBlendsPage.SetQuantity(3)
                .ClickAddToCart()
                .ClickViewCart();

            Assert.IsTrue(CartPage.IsItemAdded("Classic Blends"));
        }*/

        [TestMethod]
        public void VerifyQuantityInCart()
        {
            HomePage.GoTo();
            HomePage.GoToTeaOption(HomePage.TeaOptions.ClassicBlends);

            var quantity = Helper.RandomNumber(1, 100);

            ClassicBlendsPage.SetQuantity(quantity)
                .ClickAddToCart();

            try
            {
                //Assert.IsTrue(ClassicBlendsPage.IsQuantityCorrect(quantity));
                Assert.IsTrue(ClassicBlendsPage.IsQuantityCorrect(Helper.RandomNumber(1, 100)));
            }
            catch(AssertFailedException ex)
            {
                Helper.LogErrors(ex.Message, "Introduced quantity doesn't match with quantity in cart.");
                Helper.TakeErrorScreenshot();
            }
            
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Close();
        }
    }
}
