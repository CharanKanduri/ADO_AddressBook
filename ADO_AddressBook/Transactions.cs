using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    }
}
