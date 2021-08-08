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

        public int Edit()
        {
            //Open Connection
            sqlConnection.Open();
            string query = "Update ContactInfo set Email = 'srikar.kanduri@gmail.com' where FirstName = 'Srikar'";
           
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            int result = sqlCommand.ExecuteNonQuery();
            if (result != 0)
            {
                Console.WriteLine("Updated!");
            }
            else
            {
                Console.WriteLine("Not Updated!");
            }
            //Close Connection
            sqlConnection.Close();
            Console.WriteLine(result);
            return result;
        }


    }
}
