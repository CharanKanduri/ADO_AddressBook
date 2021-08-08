using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADO_AddressBook
{
    public class AddressBookRepo
    {   //Give path for Database Connection
        public static string connection = @"Server=.;Database=AddressBookServiceDB;Trusted_Connection=True;";
        //Represents a connection to Sql Server Database
        SqlConnection sqlConnection = new SqlConnection(connection);
        AddressAttributes addressAttributes = new AddressAttributes();
        public int Insert(AddressAttributes addressAttributes)
        {
            
            int result = 0;
            try
            {
                using (sqlConnection)
                {

                    SqlCommand command = new SqlCommand("InsertSP", this.sqlConnection);
                    //Declaring object for sql command and passing name of stored procedure and connection 
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    //Making command type as storedprocedure
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

                    command.Parameters.AddWithValue("@FirstName", addressAttributes.FirstName);
                    command.Parameters.AddWithValue("@LastName", addressAttributes.LastName);
                    command.Parameters.AddWithValue("@Address", addressAttributes.Address);
                    command.Parameters.AddWithValue("@City", addressAttributes.City);
                    command.Parameters.AddWithValue("@State", addressAttributes.State);
                    command.Parameters.AddWithValue("@zip", addressAttributes.zip);
                    command.Parameters.AddWithValue("@PhoneNumber", addressAttributes.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", addressAttributes.Email);
                    command.Parameters.AddWithValue("@addressBookName", addressAttributes.AddressBookName);
                    command.Parameters.AddWithValue("@addressBookType", addressAttributes.Type);
                    sqlConnection.Open();

                    result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Updated");

                    }
                    else
                    {
                        Console.WriteLine("Not Updated");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            Console.WriteLine(result);
            return result;
        }
  
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
                        addressAttributes.Address = Convert.ToString(reader["Address"] + " " + reader["City"] + " " + reader["State"] + " " + reader["zip"]);
                        addressAttributes.PhoneNumber = Convert.ToInt64(reader["PhoneNumber"]);
                        addressAttributes.Email = Convert.ToString(reader["email"]);
                        addressAttributes.AddressBookName = Convert.ToString(reader["AddressBookName"]);
                        addressAttributes.Type = Convert.ToString(reader["TypeOfAddressBook"]);
                        Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6}", addressAttributes.FirstName, addressAttributes.LastName, addressAttributes.Address, addressAttributes.PhoneNumber, addressAttributes.Email, addressAttributes.AddressBookName, addressAttributes.Type);
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
