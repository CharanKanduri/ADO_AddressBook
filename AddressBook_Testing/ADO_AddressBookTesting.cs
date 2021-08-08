using ADO_AddressBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AddressBook_Testing
{
    [TestClass]
    public class ADO_AddressBookTesting
    {
        AddressBookRepo addressBookRepo;

        [TestInitialize]
        void SetUp()
        {
            addressBookRepo = new AddressBookRepo();
        }

        [TestMethod]
        [TestCategory ("Insert")]
        public void InsertTesting()
        {
            int expected = 1;
            AddressAttributes addressAttributes = new AddressAttributes();

            addressAttributes.FirstName = "Charan";
            addressAttributes.LastName = "Kanduri";
            addressAttributes.Address = "SaraswathiNagar";
            addressAttributes.City = "Nellore";
            addressAttributes.State = "AP";
            addressAttributes.zip = 524;
            addressAttributes.PhoneNumber = 98;
            addressAttributes.Email = "charan@gmail.com";
            addressAttributes.AddressBookName = "1_Book";
            addressAttributes.Type = "Self";

            int actual = addressBookRepo.Insert(addressAttributes);
            Assert.AreEqual(expected, actual);
        }
    }
}
