using ADO_AddressBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AddressBook_Testing
{
    [TestClass]
    public class ADO_AddressBookTesting
    {
        AddressBookRepo addressBookRepo;
        AddressBookAfterER addressBookAfterER;
        Transactions transactions;


        [TestInitialize]
       public void SetUp()
        {
            addressBookRepo = new AddressBookRepo();
            addressBookAfterER = new AddressBookAfterER();
            transactions = new Transactions();

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
        [TestMethod]
        [TestCategory("Edit")]
        public void EditTesting()
        {
            int expected = 1;
            int actual = addressBookRepo.Edit();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteTesting()
        {
            int expected = 1;
            int actual = addressBookRepo.Delete();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [TestCategory("Retrive")]
        public void GivenRetrieveQuery_ReturnString()
        {
            string expected = "Sripathi   Lakshmi    Bhanu      ";
            string actual = addressBookRepo.Retrive("Nellore", "AP");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GivenCountQuery_ReturnString()
        {
            string expected = "1 1 2 1 ";
            string actual = addressBookRepo.CountDataBasedOnCity();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GivenSortQuery_ReturnString()
        {
            string expected = "Lakshmi    Sripathi   ";
            string actual = addressBookRepo.SortDataBasedOnCity("Nellore");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GivenCountTypeQuery_ReturnString()
        {
            string expected = "1 1 2 1 ";
            string actual = addressBookRepo.RetrivesBasedOnType();
            Assert.AreEqual(expected, actual);
        }

        //After ER

        [TestMethod]
        public void AddContactToAllTypes_UsingER_ReturnString()
        {
            string expected = "Kamal      R          Vishnu     Jss        Sripathi   Kanduri    Jason      Paul       Ashwin     Ravi       Lakshimi   Kanduri    ";
            string actual = addressBookAfterER.AddPersonToAllTypes();
            Assert.AreEqual(expected, actual);
        }

        //Transactions

       
        [TestMethod]
        public void GivenUpdateQuery_UsingTransaction_ContadIDGreaterthan2_ReturnInt()
        {
            Transactions transactions = new Transactions();
            int expected = 4;
            int actual = transactions.SetStartDateValue("update Contact_Person set Date_Added='2017-09-12' where ContactID > 2");
            Assert.AreEqual(expected, actual);
        }
        //Threads

        [TestMethod]
        public void GivenSelectQuery_UsingTransaction_ReturnCount()
        {
            Transactions transactions = new Transactions();
            int expected = 6;
            int actual = transactions.AddMultipleDataToList();
            Assert.AreEqual(expected, actual);
        }
    }
}
