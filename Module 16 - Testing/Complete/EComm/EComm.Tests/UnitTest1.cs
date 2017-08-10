using EComm.Data;
using EComm.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace EComm.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, (2 + 2));
        }

        [Fact]
        public void ProductControllerFact()
        {
            // Arrange
            var ecommDataStub = new ECommDataStub();
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<ProductController>();
            var controller = new ProductController(ecommDataStub, logger);

            // Act
            var result = controller.Detail(2);

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var vr = result as ViewResult;
            Assert.IsAssignableFrom<Product>(vr.Model);
            var model = vr.Model as Product;
            Assert.Equal(ecommDataStub.GetProduct(2).ProductName, model.ProductName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ProductControllerTheory(int id)
        {
            // Arrange
            var ecommDataStub = new ECommDataStub();
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<ProductController>();
            var controller = new ProductController(ecommDataStub, logger);

            // Act
            var result = controller.Detail(id);

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var vr = result as ViewResult;
            Assert.IsAssignableFrom<Product>(vr.Model);
            var model = vr.Model as Product;
            Assert.Equal(ecommDataStub.GetProduct(id).ProductName, model.ProductName);
        }
    }
}
