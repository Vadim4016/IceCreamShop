using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Filter
    {
        [TestMethod]
        public void Filter_By_Oder_Cost_Up()
        {
            // Arrange
            List<IceCream> iceCreams = new List<IceCream>
            {
                new IceCream {Id = 1, Name = "Product1", Price = 400},
                new IceCream {Id = 1, Name = "Product1", Price = 200},
                new IceCream {Id = 1, Name = "Product1", Price = 300}
            };
            IFilter filter = new FilterIceCream();

            // Act
            var result = filter.FilterByOderCost(iceCreams, "Up").ToList();

            // Assert
            Assert.AreEqual(result[0].Price, 200);
            Assert.AreEqual(result[1].Price, 300);
            Assert.AreEqual(result[2].Price, 400);
        }

        [TestMethod]
        public void Filter_By_Oder_Cost_Down()
        {
            // Arrange
            List<IceCream> iceCreams = new List<IceCream>
            {
                new IceCream {Id = 1, Name = "Product1", Price = 400},
                new IceCream {Id = 1, Name = "Product1", Price = 200},
                new IceCream {Id = 1, Name = "Product1", Price = 300}
            };
            IFilter filter = new FilterIceCream();

            // Act
            var result = filter.FilterByOderCost(iceCreams, "Down").ToList();

            // Assert
            Assert.AreEqual(result[0].Price, 400);
            Assert.AreEqual(result[1].Price, 300);
            Assert.AreEqual(result[2].Price, 200);
        }

        [TestMethod]
        public void Filter_By_Oder_Cost_Null()
        {
            // Arrange
            List<IceCream> iceCreams = new List<IceCream>
            {
                new IceCream {Id = 1, Name = "Product1", Price = 400},
                new IceCream {Id = 1, Name = "Product1", Price = 200},
                new IceCream {Id = 1, Name = "Product1", Price = 300}
            };
            IFilter filter = new FilterIceCream();

            // Act
            var result = filter.FilterByOderCost(iceCreams, null).ToList();

            // Assert
            Assert.AreEqual(result.Count, 3);
        }

        [TestMethod]
        public void Filter_By_Cost()
        {
            // Arrange
            List<IceCream> iceCreams = new List<IceCream>
            {
                new IceCream {Id = 1, Name = "Product1", Price = 200},
                new IceCream {Id = 2, Name = "Product2", Price = 200},
                new IceCream {Id = 3, Name = "Product3", Price = 300},
                new IceCream {Id = 4, Name = "Product4", Price = 500}
            };
            IFilter filter = new FilterIceCream();

            // Act
            var result = filter.FilterByCost(iceCreams, 200).ToList();

            // Assert
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual(result[0].Price, 200);
            Assert.AreEqual(result[1].Price, 200);
        }

        [TestMethod]
        public void Filter_By_Fat_Item()
        {
            // Arrange
            List<IceCream> iceCreams = new List<IceCream>
            {
                new IceCream {Id = 1, Name = "Product1", Fat = 25},
                new IceCream {Id = 2, Name = "Product2", Fat = 25},
                new IceCream {Id = 3, Name = "Product3", Fat = 0},
                new IceCream {Id = 4, Name = "Product4", Fat = 15}
            };
            IFilter filter = new FilterIceCream();

            // Act
            var result = filter.FilterByFat(iceCreams, 25).ToList();

            // Assert
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual(result[0].Fat, 25);
            Assert.AreEqual(result[1].Fat, 25);
        }

        [TestMethod]
        public void Filter_By_Fat_From_To()
        {
            // Arrange
            List<IceCream> iceCreams = new List<IceCream>
            {
                new IceCream {Id = 1, Name = "Product1", Fat = 20},
                new IceCream {Id = 2, Name = "Product2", Fat = 25},
                new IceCream {Id = 3, Name = "Product3", Fat = 20},
                new IceCream {Id = 4, Name = "Product4", Fat = 45}
            };
            IFilter filter = new FilterIceCream();

            // Act
            var result = filter.FilterByFat(iceCreams, 0, 25).OrderBy(m => m.Fat).ToList();

            // Assert
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0].Fat, 20);
        }

        [TestMethod]
        public void Filter_By_Fillers()
        {
            // Arrange
            List<IceCream> iceCreams = new List<IceCream>
            {
                new IceCream {Id = 1, Name = "Product1", Filler = new Filler {Сhocolate = true, SugarPowder = true} },
                new IceCream {Id = 2, Name = "Product2", Filler = new Filler {Сhocolate = true, Fruit = true}},
                new IceCream {Id = 3, Name = "Product3", Filler = new Filler {Jams = false, Fruit = true, Сhocolate = true}},
                new IceCream {Id = 4, Name = "Product4", Filler = new Filler {Сhocolate = true, Fruit = true, SugarPowder = true}}
            };
            IFilter filter = new FilterIceCream();
            Filler filterParam = new Filler
            {
                Сhocolate = true,
                Fruit = true,
                Jams = false,
                SugarPowder = false,
                Syrups = false
            };

            // Act
            var result = filter.FilterByFillers(iceCreams, filterParam).OrderBy(m => m.Id).ToList();

            // Assert
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0], iceCreams[1]);
            Assert.AreEqual(result[1], iceCreams[2]);
        }
    }
}
