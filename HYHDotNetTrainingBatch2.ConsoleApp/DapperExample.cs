using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYHDotNetTrainingBatch2.ConsoleApp
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch2",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };

        public void Read()
        {
            using (IDbConnection conn = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                conn.Open();

                // use with BlogDto (Model: Data Transfer Object)
                List<BlogDto> result = conn.Query<BlogDto>("SELECT * FROM Tbl_Blog").ToList();
                foreach(var item in result)
                {
                    Console.WriteLine("blog title: " + item.BlogTitle);
                    Console.WriteLine("blog author: " + item.BlogAuthor);
                    Console.WriteLine("blog contents: " + item.BlogContent);
                    Console.WriteLine("\n");
                }
            }
                
        }


        public void Edit()
        {
        FirstPage: 
            Console.Write("Enter id: ");
            string? id = Console.ReadLine();
            bool isInt = int.TryParse(id, out int inputId);
            if (!isInt) {
                Console.WriteLine("Please Enter valid integer value: ");
            }

            using (IDbConnection conn = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<BlogDto>("SELECT * from Tbl_Blog WHERE BlogId = @BlogId", new BlogDto { BlogId = inputId });

                if(result == null)
                {
                    Console.WriteLine("Item not found with id: " + inputId);
                    goto FirstPage;
                }

                Console.WriteLine("Blog Title: " + result.BlogTitle);
                Console.WriteLine("Blog Author: " + result.BlogAuthor);
                Console.WriteLine("Blog Content: " + result.BlogContent);
            }
        }

    }
}
