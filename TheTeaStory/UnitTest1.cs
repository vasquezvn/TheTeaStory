using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleTheTeaStory;
using ConsoleTheTeaStory.Pages;
using RestApiTheTeaStory;
using static RestApiTheTeaStory.Api;

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
        public void VerifyInserClientTable()
        {
            var name = Helper.RandomString(5, true);
            var lastname = Helper.RandomString(5, true);
            var email = $"{Helper.RandomString(5, true)}@test.com";
            var programm = Helper.RandomString(10, true);

            var isInserted = Api.InsertToClients(name, lastname, email, programm);

            Assert.IsTrue(isInserted, "Values were not inserted on Client Table");
        }

        [TestMethod]
        public void VerifyUpdateClientTable()
        {
            var name = Helper.RandomString(5, true);
            var lastname = Helper.RandomString(5, true);
            var email = $"{Helper.RandomString(5, true)}@test.com";
            var programm = Helper.RandomString(10, true);

            var randomRecord = Helper.RandomNumber(1, Api.GetNumberOfRows(Tables.Clients));
            var idClient = Api.GetIdByNumberRow(Tables.Clients, randomRecord);

            var isUpdated = Api.UpdateClients(idClient, name, lastname, email, programm);

            Assert.IsTrue(isUpdated, "Values has not been updated on Client Table");
        }

        [TestMethod]
        public void VerifyDeleteClientTable()
        {
            var randomRecord = Helper.RandomNumber(1, Api.GetNumberOfRows(Tables.Clients));
            var idClient = Api.GetIdByNumberRow(Tables.Clients, randomRecord);

            var isDeleted = Api.DeleteClient(idClient);

            Assert.IsTrue(isDeleted, $"Record with Id {idClient} could not be deleted");
        }

        [TestMethod]
        public void VerifyGetAllClients()
        {
            var numberRows = Api.GetNumberOfRows(Tables.Clients);

            var isTotalRowsMatch = Api.PrintValidateNumberRecords(Tables.Clients, numberRows);

            Assert.IsTrue(isTotalRowsMatch, "Number of rows in table doesn't match with number of printed records");
        }

        [TestMethod]
        public void VerifyGetByName()
        {
            var name = Helper.RandomString(5, true);
            var lastname = Helper.RandomString(5, true);
            var email = $"{Helper.RandomString(5, true)}@test.com";
            var programm = Helper.RandomString(10, true);

            Api.InsertToClients(name, lastname, email, programm);

            var isNameInDB = Api.IsNameRecordedInDB(name);

            Assert.IsTrue(isNameInDB, $"Record can't be found by name: {name}");
        }

        [TestMethod]
        public void VerifyGetPreferenceById()
        {
            var randomRecord = Helper.RandomNumber(1, Api.GetNumberOfRows(Tables.Clients));
            var idClient = Api.GetIdByNumberRow(Tables.Clients, randomRecord);

            var isTherePreferences = Api.IsTherePreferencesByClientId(idClient);

            Assert.IsTrue(isTherePreferences, $"There isn't preferences to Client by id: {idClient}");
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
