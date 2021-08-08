using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADO_AddressBook
{
    class AddressBookRepo
    {   //Give path for Database Connection
        public static string connection = @"Server=.;Database=AddressBookService_DB;Trusted_Connection=True;";
        //Represents a connection to Sql Server Database
        SqlConnection sqlConnection = new SqlConnection(connection);
    }
}
