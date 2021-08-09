using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_AddressBook
{
    public class AddressBookAfterER
    {
        AddressAttributes attributes = new AddressAttributes();
        //Give path for Database Connection
        public static string connection = @"Server=.;Database=AddressBookServiceDB;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connection);
        AddressAttributes addressAttributes = new AddressAttributes();
        AddressBookRepo addressBookRepo = new AddressBookRepo();


        public string AddPersonToAllTypes()
        {
            string nameList = "";
            //query to be executed
            string query = @"select AddressBookName,Concat(FirstName,' ',LastName) as Name,Concat(Address,' ,',City,' ,',State,' ,',zip) as Address,PhoneNumber,Email,ContactType_Name from Address_Book Full JOIN Contact_Person on Address_Book.AddressBookID=Contact_Person.AddressBookID Full JOIN TypeManager on TypeManager.Contact_Identity=ContactID Full JOIN ContactType on TypeManager.ContactType_Identity=ContactTypeID";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    string AddressBookName = Convert.ToString(sqlDataReader["AddressBookName"]);
                    string Name = Convert.ToString(sqlDataReader["Name"]);
                    string Address = Convert.ToString(sqlDataReader["Address"]);
                    int PhoneNumber = Convert.ToInt32(sqlDataReader["PhoneNumber"]);
                    string Email = Convert.ToString(sqlDataReader["Email"]);
                    string Type = Convert.ToString(sqlDataReader["ContactType_Name"]);
                    Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4} \t {5}", AddressBookName, Name, Address, PhoneNumber, Email, Type);

                    nameList += sqlDataReader["Name"].ToString() + " ";
                    
                }
                
            }
            
            return nameList;

        }
    }
}
