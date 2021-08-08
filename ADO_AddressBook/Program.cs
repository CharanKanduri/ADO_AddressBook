using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADO_AddressBook
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to AddressBook ADO program");
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            AddressAttributes addressAttributes = new AddressAttributes();

            addressBookRepo.Insert(addressAttributes);
            DisplayContactInfo displayContactInfo = new DisplayContactInfo();
            displayContactInfo.Display();
            



        }

    }
}
