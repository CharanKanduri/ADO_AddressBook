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

        public int Delete()
        {
            //Open Connection
            sqlConnection.Open();
            string query = "delete from ContactInfo where FirstName = 'Charan' and LastName = 'Kanduri'";
            //Pass query to TSql
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
            return result;
        }
        public string Retrive(string city, string State)
        {
            string nameList = "";
            //query to be executed
            string query = @"select * from ContactInfo where City =" + "'" + city + "' or State=" + "'" + State + "'";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    DisplayEmployeeDetails(sqlDataReader);
                    nameList += sqlDataReader["FirstName"].ToString() + " ";
                }
            }
            return nameList;
        }
        public void DisplayEmployeeDetails(SqlDataReader reader)
        {
            addressAttributes.FirstName = Convert.ToString(reader["FirstName"]);
            addressAttributes.LastName = Convert.ToString(reader["LastName"]);
            addressAttributes.Address = Convert.ToString(reader["Address"]);
            addressAttributes.City = Convert.ToString(reader["City"]);
            addressAttributes.State = Convert.ToString(reader["State"]);
            addressAttributes.zip = Convert.ToInt32(reader["Zip"]);
            addressAttributes.PhoneNumber = Convert.ToInt32(reader["PhoneNumber"]);
            addressAttributes.Email = Convert.ToString(reader["Email"]);
            addressAttributes.AddressBookName = Convert.ToString(reader["AddressBookName"]);
            addressAttributes.Type = Convert.ToString(reader["AddressBookType"]);

            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9}", addressAttributes.FirstName, addressAttributes.LastName, addressAttributes.Address, addressAttributes.City, addressAttributes.State, addressAttributes.PhoneNumber, addressAttributes.zip, addressAttributes.Email, addressAttributes.AddressBookName, addressAttributes.Type);

        }
        public string CountDataBasedOnCity()
        {
            string nameList = "";
            //query to be executed
            string query = @"select Count(*),state,City from ContactInfo Group by state,City";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Console.WriteLine("{0} \t {1} \t {2}", sqlDataReader[0], sqlDataReader[1], sqlDataReader[2]);
                    nameList += sqlDataReader[0].ToString() + " ";
                }
            }
            return nameList;
        }


    }
}
