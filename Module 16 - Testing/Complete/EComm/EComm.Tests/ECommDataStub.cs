using EComm.Data;
using EComm.MVC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComm.Tests
{
    class ECommDataStub : IECommData
    {
        private List<Product> products = new List<Product>();
        public ECommDataStub()
        {
            products.Add(new Product
            { Id = 1, ProductName = "Milk", UnitPrice = 2.50M });
            products.Add(new Product
            { Id = 2, ProductName = "Bread", UnitPrice = 3.25M });
            products.Add(new Product
            { Id = 3, ProductName = "Juice", UnitPrice = 5.75M });
        }

        public IEnumerable<Product> GetProducts()
        {
            return products; 
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await Task.Run(() => products);
        }

        public Product GetProduct(int id)
        {
            return products[id];
        }
    }
}
