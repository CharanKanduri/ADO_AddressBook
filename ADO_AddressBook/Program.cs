using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADO_AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().CreateTable();
        }
        public void CreateTable()
        {
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection("data source=.; Database=AddressBookServiceDB; Trusted_Connection=True;");
                Console.WriteLine("Connected Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }
    }
}
