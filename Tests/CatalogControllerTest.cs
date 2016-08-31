using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Models;

namespace Tests
{
    [TestClass]
    public class CatalogControllerTest
    {
        public Mock<IRepository> GetMock()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllIceCreams()).Returns(new List<IceCream>
            {
                new IceCream {Id = 1, Name = "Product1", Price = 100, Fat = 10, Filler = new Filler {Fruit = true} },
                new IceCream {Id = 2, Name = "Product2", Price = 200, Fat = 20, Filler = new Filler {Fruit = true, Сhocolate = true}},
                new IceCream {Id = 3, Name = "Product3", Price = 100, Fat = 10, Filler = new Filler {Fruit = true, SugarPowder = true}},
                new IceCream {Id = 4, Name = "Product4", Price = 400, Fat = 20, Filler = new Filler {Fruit = true, Сhocolate = true}},
                new IceCream {Id = 5, Name = "Product5", Price = 200, Fat = 30, Filler = new Filler {Fruit = false, Syrups = true}},
                new IceCream {Id = 6, Name = "Product6", Price = 600, Fat = 30, Filler = new Filler {}}
            });
            return mock;
        }

        [TestMethod]
        public void Filter_Result_Test()
        {
            // Arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllIceCreams()).Returns(new List<IceCream>
            {
                new IceCream {Id = 1, Name = "Product1", Price = 100, Fat = 10, Filler = new Filler {Fruit = true} },
                new IceCream {Id = 2, Name = "Product2", Price = 200, Fat = 20, Filler = new Filler {Сhocolate = true}},
                new IceCream {Id = 3, Name = "Product3", Price = 100, Fat = 10, Filler = new Filler {Fruit = true, SugarPowder = true}},
                new IceCream {Id = 4, Name = "Product4", Price = 400, Fat = 20, Filler = new Filler {Сhocolate = true}},
                new IceCream {Id = 5, Name = "Product5", Price = 200, Fat = 30, Filler = new Filler {Fruit = false, Syrups = true}},
                new IceCream {Id = 6, Name = "Product6", Price = 600, Fat = 30, Filler = new Filler {}}
            });
            FilterViewModel filterViewModel = new FilterViewModel
            {
                Filler = new Filler
                {
                    Сhocolate = true
                },
                Fat = 20,
                OrderSortCost = "Up"
            };
            CatalogController controller = new CatalogController(mock.Object);
            controller.ItemsCount = 2;

            // Act
            var result = (CatalogViewModel)controller.CatalogProducts(1, filterViewModel).Model;

            // Assert
            var items = result.IceCreams.ToList();
            Assert.AreEqual(items.Count, 2);
            Assert.AreEqual(items[0].Name, "Product2");
            Assert.AreEqual(items[1].Name, "Product4");
        }

        [TestMethod]
        public void Items_Result_Test()
        {
            // Arrange
            Mock<IRepository> mock = GetMock();
            CatalogController controller = new CatalogController(mock.Object);
            controller.ItemsCount = 2;

            // Act
            var result = (CatalogViewModel)controller.CatalogProducts(2).Model;
            List<IceCream> items = result.IceCreams.ToList();

            // Assert
            Assert.AreEqual(items.Count, 2);
            Assert.IsTrue(items.Count == 2);
            Assert.AreEqual(items[0].Name, "Product3");
            Assert.AreEqual(items[1].Name, "Product4");
        }
    }
}
