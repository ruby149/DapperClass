using System;
using System.Collections.Generic;
using System.Text;

namespace IntroSQL
{
    public interface IProductRepository
    {
        IEnumerable<product> GetAllProducts(); //stubbed out method - doesnt have implementation
        void CreateProduct(string name, double price, int categoryID);
    }
}
