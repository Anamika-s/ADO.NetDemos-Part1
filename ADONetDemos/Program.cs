using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Step 1 : Add namespace 
using System.Data.SqlClient;


namespace ADONetDemos
{
    class Program
    {
        static SqlConnection sqlConnection;
        static void Main(string[] args)
        {
            string choice = "y";
            while (choice == "y")
            {

                Menu();
                Console.WriteLine("Enter your choice");
                int ch = Byte.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter ID");
                            int id = Convert.ToByte(Console.ReadLine());
                            Console.WriteLine("Enter Name");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter Address");
                            string address = Console.ReadLine();
                            Console.WriteLine("Enter Salary");
                            int salary = Int16.Parse(Console.ReadLine());
                            InsertEmployee(id, name, address, salary);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter ID for which to edit record");
                            int id = Convert.ToByte(Console.ReadLine());

                            Console.WriteLine("Enter Address");
                            string address = Console.ReadLine();
                            Console.WriteLine("Enter Salary");
                            int salary = Int16.Parse(Console.ReadLine());

                            EditEmployee(id, address, salary);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter ID for which to delete record");
                            int id = Convert.ToByte(Console.ReadLine());


                            DeleteEmployee(id);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Enter ID for which to search record");
                            int id = Convert.ToByte(Console.ReadLine());
                            SearchEmployee(id);
                            break;
                        }
                    case 5:
                        {
                            GetEmployees();
                            break;
                        }
                    case 6:
                        {
                            GetEmployeesCount();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Choice");
                            break;
                        }
                }
                Console.WriteLine("Do you want to repeat any step");
                choice = Console.ReadLine();
            }
           
        }
        static void Menu()
        {
            Console.WriteLine("1. Insert ");
            Console.WriteLine("2. Edit ");
            Console.WriteLine("3. Delete ");
            Console.WriteLine("4. Search ");
            Console.WriteLine("5. List ");
            Console.WriteLine("6. Employees Count ");

        }
        static SqlConnection GetConnection()
        {
            string connectionString = @"data source=adminvm\SQLEXPRESS;initial catalog=PracticeDB;user id=sa;password=pass@123";

            sqlConnection = new SqlConnection(connectionString);
            return sqlConnection;
        }
        static void GetEmployees()
        {
            // Step 2: //, Create object of SqlConnection ,
            // Pass connectinString
            // Server name , database name , credentials

            //SqlConnection sqlConnection = new SqlConnection();
            // string connectionString = @"data source=adminvm\SQLEXPRESS;initial catalog=PracticeDB;user id=sa;password=pass@123";
            ////string connectionString = @"data source=adminvm\SQLEXPRESS;initial catalog=PracticeDB;integrated security=true";
            //sqlConnection.ConnectionString = connectionString;
            //SqlConnection sqlConnection = new SqlConnection(@"data source=adminvm\SQLEXPRESS;initial catalog=PracticeDB;user id=sa;password=pass@123");
            //string connectionString = @"data source=adminvm\SQLEXPRESS;initial catalog=PracticeDB;user id=sa;password=pass@123";

            //SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection = GetConnection();
            // Step 3 , create SqlCommand command, 
            // pass command & SqlConnection object to that

            //SqlCommand sqlCommand = new SqlCommand();
            //sqlCommand.CommandText = "Select * from Employee";
            //sqlCommand.Connection = sqlConnection;

            SqlCommand sqlCommand = new SqlCommand("Select * from Employee", sqlConnection);

            // Step 4 :
            // Open Connection
            sqlConnection.Open();
            // Step 5 :
            // Execute Query in Backend
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                //while (reader.Read())
                //{
                //    //    Console.WriteLine(reader[0].ToString() + " " + reader[1] + " " + reader[2]);
                //    Console.WriteLine(reader["id"].ToString() + " " + reader["name"] + " " + reader["address"] + " " + reader["salary"].ToString());

                //}
                while (reader.Read())
                {
                    //    Console.WriteLine(reader[0].ToString() + " " + reader[1] + " " + reader[2]);
                    for (int i = 0; i < reader.FieldCount; i++)
                        Console.Write(reader[i].ToString() + "\t");
                }
            }
            else
                Console.WriteLine("No Records");
            reader.Close();
            sqlConnection.Close();
        }
        static void InsertEmployee(int id, string name, string address, int salary)
        {
            //string connectionString = @"data source=adminvm\SQLEXPRESS;initial catalog=PracticeDB;user id=sa;password=pass@123";
            //SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection = GetConnection();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "insert into Employee(id, name, address, salary) values(@id, @name, @address, @salary)";
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@name", name);
            sqlCommand.Parameters.AddWithValue("@address", address);
            sqlCommand.Parameters.AddWithValue("@salary", salary);
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        static void EditEmployee(int id, string address, int salary)
        {
            sqlConnection = GetConnection();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "Update Employee set address=@address, salary = @salary where id=@id";
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@address", address); 
            sqlCommand.Parameters.AddWithValue("@salary", salary);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }


        static void DeleteEmployee(int id)
        {
            sqlConnection = GetConnection();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "Delete Employee where id=@id";
            sqlCommand.Parameters.AddWithValue("@id", id);

            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        static void SearchEmployee(int id)
        {
            sqlConnection = GetConnection();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "select * from  Employee where id=@id";
            sqlCommand.Parameters.AddWithValue("@id", id);

            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if(reader.HasRows)
            {
                reader.Read();
                for (int i = 0; i < reader.FieldCount; i++)
                    Console.Write(reader[i].ToString() + "\t");
            }
        
            else
                Console.WriteLine("No Records");
            reader.Close();
            sqlConnection.Close();
        }
        static void GetEmployeesCount()
        {
            sqlConnection = GetConnection();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "select count(*) from  Employee";
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int count = (int)sqlCommand.ExecuteScalar();
            Console.WriteLine("No of Employees are " + count);
            sqlConnection.Close();
        }
    }
}