using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests
{
    [TestClass]
    public class Cart
    {
        public Mock<IRepository> GetMock()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllIceCreams()).Returns(new List<IceCream>
            {
                new IceCream { Id = 1, Name = "Product1"},
                new IceCream { Id = 2, Name = "Product2"},
                new IceCream { Id = 3, Name = "Product3"},
                new IceCream { Id = 4, Name = "Product4"},
                new IceCream { Id = 5, Name = "Product5"},
                new IceCream { Id = 6, Name = "Product6"}
            });

            return mock;
        }

        [TestMethod]
        public void Can_Add_Item()
        {
            // Arrange
            IceCream item1 = new IceCream {Id = 1, Name = "Proguct1"};
            IceCream item2 = new IceCream { Id = 2, Name = "Proguct2" };
            Domain.Model.Cart cart = new Domain.Model.Cart();

            // Act
            cart.AddItem(item1, 1);
            cart.AddItem(item2, 2);
            var listItems = cart.Items;
            
            // Assert
            Assert.IsTrue(listItems.Count == 2);
            Assert.AreEqual(listItems[0].Quantity, 1);
            Assert.AreEqual(listItems[1].Quantity, 2);
            Assert.AreEqual(listItems[0].IceCream.Name, "Proguct1");
            Assert.AreEqual(listItems[1].IceCream.Name, "Proguct2");
        }

        [TestMethod]
        public void Can_Remove_Item()
        {
            // Arrange
            IceCream item1 = new IceCream { Id = 1, Name = "Proguct1" };
            IceCream item2 = new IceCream { Id = 2, Name = "Proguct2" };
            Domain.Model.Cart cart = new Domain.Model.Cart();
            cart.AddItem(item1, 2);
            cart.AddItem(item2, 4);

            // Act
            cart.RemoveLine(item1);

            // Assert
            Assert.AreEqual(cart.Items.Where(m => m.IceCream == item1).ToList().Count, 0);
            Assert.AreEqual(cart.Items.Count, 1);
        }

        [TestMethod]
        public void Can_Clear_Cart()
        {
            // Arrange
            IceCream item1 = new IceCream { Id = 1, Name = "Proguct1" };
            IceCream item2 = new IceCream { Id = 2, Name = "Proguct2" };
            Domain.Model.Cart cart = new Domain.Model.Cart();
            cart.AddItem(item1, 2);
            cart.AddItem(item2, 4);

            // Act
            cart.Clear();

            // Assert
            Assert.AreEqual(cart.Items.Count, 0);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange
            IceCream item1 = new IceCream { Id = 1, Name = "Proguct1" };
            IceCream item2 = new IceCream { Id = 2, Name = "Proguct2" };
            Domain.Model.Cart cart = new Domain.Model.Cart();

            // Act
            cart.AddItem(item1, 3);
            cart.AddItem(item1, 1);
            cart.AddItem(item2, 6);
            var result = cart.Items.OrderBy(m => m.IceCream.Id).ToList();

            // Assert
            Assert.AreEqual(result[0].Quantity, 4);
            Assert.AreEqual(result[1].Quantity, 6);
        }

        [TestMethod]
        public void Total_Sum_Cart()
        {
            // Arrange
            IceCream item1 = new IceCream { Id = 1, Name = "Proguct1", Price = 200};
            IceCream item2 = new IceCream { Id = 2, Name = "Proguct2", Price = 300};
            Domain.Model.Cart cart = new Domain.Model.Cart();

            // Act
            cart.AddItem(item1, 2);
            cart.AddItem(item2, 3);

            // Assert
            Assert.AreEqual(cart.ComputeTotalValue(), 1300);
        }
    }
}
