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
            DisplayContactInfo displayContactInfo = new DisplayContactInfo();
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            addressBookRepo.Retrive("Nellore","AP");
            displayContactInfo.Display();



        }

    }
}
