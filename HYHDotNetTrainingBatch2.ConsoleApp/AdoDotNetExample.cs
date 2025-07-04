using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYHDotNetTrainingBatch2.ConsoleApp
{
    public class AdoDotNetExample
    {
        // Ctrl +R R ( to redefine variable name
        SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch2",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };

        public void Read()
        {
            //SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            //sqlConnectionStringBuilder.DataSource = ".";
            //sqlConnectionStringBuilder.InitialCatalog = "DotNetTrainingBatch2";
            //sqlConnectionStringBuilder.UserID = "sa";
            //sqlConnectionStringBuilder.Password = "sasa@123";


            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            Console.WriteLine("Connection Opening.. ");
            // define query
            string query = "SELECT * FROM Tbl_Blog";

            // command
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            // SqlDataAdapter to run command
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            // DataTable defination
            DataTable dataTable = new DataTable();

            // execute adapter and store results into dataTable
            adapter.Fill(dataTable);


            sqlConnection.Close();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = dataTable.Rows[i];
                Console.WriteLine(" => "+dataRow["BlogId"]);
                Console.WriteLine(" => "+dataRow["BlogTitle"]);
                Console.WriteLine(" => "+dataRow["BlogAuthor"]);
                Console.WriteLine(" => "+dataRow["BlogContent"]);
            }
        }


        public void Create()
        {
            // get user input
            Console.Write("Enter Title");
            string title = Console.ReadLine()!;

            Console.Write("Enter Author: ");
            string author = Console.ReadLine()!;

            Console.Write("Enter Content: ");
            string content = Console.ReadLine()!;


            SqlConnection conn = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            conn.Open();

            //       string query = $@"INSERT INTO [dbo].[Tbl_Blog]
            //      ([BlogId]
            //       ,[BlogTitle]
            //      ,[BlogAuthor]
            //      ,[BlogContent])
            //VALUES
            //      ('1'
            //       ,'{title}'
            //      ,'{author}'
            //      ,'{content}')";


            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogId]
            ,[BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           ('2'
            ,@Title
            ,@Author
            ,@Content)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Author", author);
            cmd.Parameters.AddWithValue("@Content", content);

            // execute query
            int result = cmd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine(result > 0 ? "Insert Success!" : "Insert Failed");
        }



        public void Update()
        {
            // get id
            Console.Write("Enter Id: ");
            int id = int.Parse(Console.ReadLine()!);

            // get name
            Console.Write("Enter title: ");
            string title = Console.ReadLine()!;

            // connection
            SqlConnection conn = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            conn.Open();

            // query
            string query = $@"UPDATE Tbl_Blog SET BlogTitle=@blogtitle where BlogId = @blogid";

            // command
            SqlCommand comd = new SqlCommand(query, conn);
            comd.Parameters.AddWithValue("@blogid", id);
            comd.Parameters.AddWithValue("@blogtitle", title);

            // executeNonQuery()
            int result = comd.ExecuteNonQuery();

            conn.Close();
            Console.WriteLine(result > 0 ? "Update success!" : "Update Failed");
        }

        public void delete()
        {
            //connection
            SqlConnection conn = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            conn.Open();

            //query
            string query = @"";

            // command
            SqlCommand comd = new SqlCommand(query, conn);

            //executeNonQuery()
            int result = comd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine(result > 0 ? "delete success" : "Delete Failed!");

        }

    }
}
