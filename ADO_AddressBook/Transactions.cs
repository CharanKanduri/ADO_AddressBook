using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_AddressBook
{
    public class Transactions
    {
        public static string connection = @"Server=.;Database=AddressBookServiceDB;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connection);

        List<AddressAttributes> contactList = new List<AddressAttributes>();
        public int AlterTableaddStartDate()
        {
            int result = 0;
            sqlConnection.Open();
            using (sqlConnection)
            {

                //Begin SQL transaction
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = "alter table Contact_Person add Date_Added Date";
                    result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated!");
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Updated!");

                }
            }
            sqlConnection.Close();
            return result;
        }
        public int SetStartDateValue(string query)
        {
            int result = 0;
            sqlConnection.Open();
            using (sqlConnection)
            {
                //Begin SQL transaction
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = query;
                    result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Updated!");
                }
            }
            sqlConnection.Close();
            return result;
        }
        public int AddMultipleDataToList()
        {
            string nameList = "";
            //query to be executed
            string query = "select AddressBookName,FirstName,LastName,Address,City,State,zip,PhoneNumber,Email,ContactType_Name from Contact_Person INNER JOIN  Address_Book on Address_Book.AddressBookID = Contact_Person.AddressBookID INNER JOIN TypeManager on TypeManager.Contact_Identity = ContactID INNER JOIN ContactType on TypeManager.ContactType_Identity = ContactTypeID";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    DisplayEmployeeDetails(sqlDataReader);
                    nameList += sqlDataReader["FirstName"].ToString() + " ";
                    stopWatch.Stop();
                    Console.WriteLine("Time elapsed using Thread: {0}", stopWatch.ElapsedMilliseconds);
                }
            }
            sqlConnection.Close();
            return contactList.Count;
        }
        //Display all Object details
        public void DisplayEmployeeDetails(SqlDataReader sqlDataReader)
        {
            AddressAttributes addressBook = new AddressAttributes();
            addressBook.AddressBookName = Convert.ToString(sqlDataReader["AddressBookName"]);
            addressBook.FirstName = Convert.ToString(sqlDataReader["FirstName"]);
            addressBook.Address = Convert.ToString(sqlDataReader["Address"] + " " + sqlDataReader["City"] + " " + sqlDataReader["State"] + " " + sqlDataReader["zip"]);
            addressBook.PhoneNumber = Convert.ToInt64(sqlDataReader["PhoneNumber"]);
            addressBook.Email = Convert.ToString(sqlDataReader["Email"]);
            addressBook.Type = Convert.ToString(sqlDataReader["ContactType_Name"]);
            Task task = new Task(() =>
            {
                Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4} \t {5} \t {6}", addressBook.FirstName, addressBook.LastName, addressBook.Address, addressBook.PhoneNumber, addressBook.Email, addressBook.AddressBookName, addressBook.Type);
                contactList.Add(addressBook);
            });
            task.Start();
        }


    }
}
