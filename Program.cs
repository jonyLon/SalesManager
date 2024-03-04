using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManager
{
    internal class Program
    {





        class SalesManager
        {
            private static SqlConnection _connection;
            public SqlConnection Connection { get => _connection;}
            public SalesManager(SqlConnection con)
            {
                _connection = con;
            }



            public void ShowAllCustomers()
            {
                string cmdStr = @"select * from Customers";
                SqlCommand sqlComm = new SqlCommand(cmdStr, _connection);
                var reader = sqlComm.ExecuteReader();
                ShowReaderData(reader);
            }
            public void ShowAllSellers()
            {
                string cmdStr = @"select * from Sellers";
                SqlCommand sqlComm = new SqlCommand(cmdStr, _connection);
                var reader = sqlComm.ExecuteReader();
                ShowReaderData(reader);
            }

            public void ShowAllSalesBySellerName(string firstName, string lastName)
            {
                string cmdStr = @"select s.SaleId, s.SaleDate, s.SaleAmount, sr.FirstName, sr.LastName from Sales as s join Sellers as sr on sr.SellerId = s.SellerId
                where sr.FirstName = @first_name and sr.LastName = @last_name";

                SqlCommand sqlComm = new SqlCommand(cmdStr, _connection);
                sqlComm.Parameters.Clear();
                sqlComm.Parameters.Add("@first_name", System.Data.SqlDbType.NVarChar).Value = firstName;
                sqlComm.Parameters.Add("@last_name", System.Data.SqlDbType.NVarChar).Value = lastName;
                var reader = sqlComm.ExecuteReader();
                ShowReaderData(reader);
            }

            public void ShowAllSalesBigerThan(int amount)
            {
                string cmdStr = @"select s.SaleId, s.SaleDate, s.SaleAmount from Sales as s where s.SaleAmount > @amount";

                SqlCommand sqlComm = new SqlCommand(cmdStr, _connection);
                sqlComm.Parameters.Clear();
                sqlComm.Parameters.Add("@amount", System.Data.SqlDbType.Int).Value = amount;
                var reader = sqlComm.ExecuteReader();
                ShowReaderData(reader);
            }

            public void ShowMinAndMaxSalesByCustomerName(string firstName, string lastName)
            {
                string cmdStr = @"select s.SaleId, s.SaleDate, s.SaleAmount, c.FirstName, c.LastName from Sales as s join Customers as c on c.CustomerId = s.CustomerId
                where c.FirstName = @first_name and c.LastName = @last_name and s.SaleAmount = (select Min(s.SaleAmount) from Sales as s join Customers as c on c.CustomerId = s.CustomerId where c.FirstName = @first_name and c.LastName = @last_name) or s.SaleAmount = (select Max(s.SaleAmount) from Sales as s join Customers as c on c.CustomerId = s.CustomerId where c.FirstName = @first_name and c.LastName = @last_name) ";
                SqlCommand sqlComm = new SqlCommand(cmdStr, _connection);
                sqlComm.Parameters.Clear();
                sqlComm.Parameters.Add("@first_name", System.Data.SqlDbType.NVarChar).Value = firstName;
                sqlComm.Parameters.Add("@last_name", System.Data.SqlDbType.NVarChar).Value = lastName;
                var reader = sqlComm.ExecuteReader();
                ShowReaderData(reader);
            }
            public void FirstSaleBySellerName(string firstName, string lastName)
            {
                string cmdStr = @"select s.SaleId, s.SaleDate, s.SaleAmount, sr.FirstName, sr.LastName from Sales as s join Sellers as sr on sr.SellerId = s.SellerId
                where sr.FirstName = @first_name and sr.LastName = @last_name and s.SaleDate = (select  Min(s.SaleDate) from Sales as s join Sellers as sr on sr.SellerId = s.SellerId
                where sr.FirstName = @first_name and sr.LastName = @last_name)";

                SqlCommand sqlComm = new SqlCommand(cmdStr, _connection);
                sqlComm.Parameters.Clear();
                sqlComm.Parameters.Add("@first_name", System.Data.SqlDbType.NVarChar).Value = firstName;
                sqlComm.Parameters.Add("@last_name", System.Data.SqlDbType.NVarChar).Value = lastName;
                var reader = sqlComm.ExecuteReader();
                ShowReaderData(reader);
            }


            private void ShowReaderData(SqlDataReader reader)
            {
                Console.WriteLine();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader.GetName(i),20}");
                }
                Console.WriteLine();
                Console.WriteLine(new string('-', reader.FieldCount*20));
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader[i],20}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                reader.Close();
            }
        }






        static void Main(string[] args)
        {
            string conn = ConfigurationManager.ConnectionStrings["Sales_db"].ConnectionString;
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();
            var sm = new SalesManager(connection);
            while (true)
            {
                Console.WriteLine("1 - Show All Customers\n2 - Show All Sellers\n3 - Show All Sales By Seller Name\n4 - Show All Sales Where Price Biger Than\n5 - Show Min And Max Sales By Customer Name\n6 - First Sale By Seller Name\n0 - Exit\nSelect Option: ");
                var op = Console.ReadLine();
                if (op == "0")
                {
                    break;
                }
                switch(op) {
                    case "1":
                        sm.ShowAllCustomers();
                        break;
                    case "2":
                        sm.ShowAllSellers();
                        break;
                    case "3":
                        Console.WriteLine("Enter first name: ");
                        var first_name = Console.ReadLine();
                        Console.WriteLine("Enter last name: ");
                        var last_name = Console.ReadLine();
                        sm.ShowAllSalesBySellerName(first_name, last_name); 
                        break;
                    case "4":
                        Console.WriteLine("Enter price: ");
                        var price =int.Parse(Console.ReadLine());
                        sm.ShowAllSalesBigerThan(price);
                        break;
                    case "5":
                        Console.WriteLine("Enter first name: ");
                        first_name = Console.ReadLine();
                        Console.WriteLine("Enter last name: ");
                        last_name = Console.ReadLine();
                        sm.ShowMinAndMaxSalesByCustomerName(first_name, last_name);
                        break;
                    case "6":
                        Console.WriteLine("Enter first name: ");
                        first_name = Console.ReadLine();
                        Console.WriteLine("Enter last name: ");
                        last_name = Console.ReadLine();
                        sm.FirstSaleBySellerName(first_name, last_name);
                        break;
                    default: 
                        Console.WriteLine("Wrong Option!");
                        break;
                }

            }
            connection.Close();
            
        }

    }

}
