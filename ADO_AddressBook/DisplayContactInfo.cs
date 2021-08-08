using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_AddressBook
{
    public class DisplayContactInfo
    {    //Give path for Database Connection
        public static string connection = @"Server=.;Database=AddressBookServiceDB;Trusted_Connection=True;";
        //Represents a connection to Sql Server Database
        SqlConnection sqlConnection = new SqlConnection(connection);
        AddressAttributes addressAttributes = new AddressAttributes();
        public void Display()
        {
            //opening the sql connection
            this.sqlConnection.Open();
            //create the query to display data
            string query = @"select * from dbo.ContactInfo";
            //create object for employee detail class
            AddressAttributes addressAttributes = new AddressAttributes();
            try
            {
                //create the sql command object nd pass the querry and connection
                SqlCommand command = new SqlCommand(query, sqlConnection);
                //create data reader 
                SqlDataReader reader = command.ExecuteReader();
                //if it has data
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        addressAttributes.FirstName = Convert.ToString(reader["FirstName"]);
                        addressAttributes.LastName = Convert.ToString(reader["LastName"]);
                        addressAttributes.Address = Convert.ToString(reader["Address"]);
                        addressAttributes.City = Convert.ToString(reader["City"]);
                        addressAttributes.State=Convert.ToString(reader["State"]);
                        addressAttributes.zip = Convert.ToInt32(reader["Zip"]);
                        addressAttributes.PhoneNumber = Convert.ToInt32(reader["PhoneNumber"]);
                        addressAttributes.Email = Convert.ToString(reader["Email"]);
                        addressAttributes.AddressBookName = Convert.ToString(reader["AddressBookName"]);
                        addressAttributes.Type = Convert.ToString(reader["AddressBookType"]);

                        Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9}", addressAttributes.FirstName, addressAttributes.LastName, addressAttributes.Address, addressAttributes.City, addressAttributes.State, addressAttributes.PhoneNumber, addressAttributes.zip, addressAttributes.Email, addressAttributes.AddressBookName, addressAttributes.Type);
                    }
                }
                else
                {
                    Console.WriteLine("No data vailable");
                }
                reader.Close();
            }
            //if any exception occurs catch and display exception message
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //finally close the connection
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}
