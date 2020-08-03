using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Ubiety.Dns.Core;

namespace IntroSQL
{
    class Program
    {
        private static string connstring;

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            var productsRepo = new DapperDepartmentRepository(conn);
            Console.WriteLine($"What is the name of the new product?");
            var productName = Console.ReadLine();

            double priceOfProduct;
            bool response;
            do
            {
                Console.WriteLine($"How much does {productName} cost?");
                response = double.TryParse(Console.ReadLine(), out priceOfProduct);
            }while (response == false);

            Console.WriteLine($"What is the CategoryID for {productName}?");
            var categoryID = int.Parse(Console.ReadLine());

            productsRepo.CreateProduct(productName, priceOfProduct, categoryID);
           
            var products = productsRepo.GetAllProducts();
            foreach (var item in products)
            {
                Console.WriteLine($"{item.productID} {item.Name} {item.Price}");
            }

            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Hello user, here are the current departments:");
            Console.WriteLine("Please press enter...");
            Console.ReadLine();
            var depos = repo.GetAllDepartments();

            foreach (var depo in depos)
            {
                Console.WriteLine($"ID:{ depo.DepartmentID} Name:{ depo.Name}");
            }

            Console.WriteLine("Do you want to add a department???");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new Department???");
                userResponse = Console.ReadLine();
                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }

            Console.WriteLine("have a great day.");
        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentID} Name:{depo.Name}");
            }
        }
    }
}
