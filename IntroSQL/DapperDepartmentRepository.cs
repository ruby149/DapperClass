using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IntroSQL
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _connection;
        private object categoryID;

        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

       
        public IEnumerable<Department> GetAllDepartments()
        {
            
            var depos = _connection.Query<Department>("Select * From departments").ToList();
            return depos;
        }
        public IEnumerable<product> GetAllProducts()
        {
            return _connection.Query<product>("SELECT * FROM  products").ToList();
        }

        public void InsertDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
           new { departmentName = newDepartmentName });
        }
        public void CreateProduct(string name, double price, int catergoryID)
        {
            _connection.Execute("INSERT INTO products(Name, Price, CatergoryID) VALUES(@name, @price, @categoryID);", new { name = name, price = price, categoryID = categoryID });
        }
    }





}
