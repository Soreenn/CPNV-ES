using Microsoft.VisualStudio.TestTools.UnitTesting;
using EsCpnv;
using System;
using System.Collections.Generic;

namespace EsCpnvTest
{
    [TestClass]
    public class EsCpnvTests
    {
        List<Item> items;
        Machine machine;

        [TestInitialize]
        public void TestInitialize() 
        {
            List<Item> items = new List<Item>();
            Item testItem = new Item("Smarlies", "A01", 10, 1.60M);
            Item testItem2 = new Item("Carampar", "A02", 5, 0.60M);
            Item testItem3 = new Item("Avril", "A03", 2, 2.10M);
            Item testItem4 = new Item("KokoKola", "A04", 1, 2.95M);
            items.Add(testItem);
            items.Add(testItem2);
            items.Add(testItem3);
            items.Add(testItem4);

            machine = new Machine(items);
        }

        [TestCleanup]
        public void TestClean()
        {
            items = null;
            machine = null;
        }

        [TestMethod]
        public void Test1()
        {
            machine.Insert(3.40M) ;

            Assert.AreEqual("Vending Smarlies", machine.Choose("A01"));
            Assert.AreEqual(1.80M, machine.GetChange);
        }

        [TestMethod]
        public void Test2()
        {
            machine.Insert(2.10M);
            Assert.AreEqual("Vending Avril", machine.Choose("A03"));
            Assert.AreEqual(0.00M, machine.GetChange);
            Assert.AreEqual(2.10M, machine.GetBalance);
        }

        [TestMethod]
        public void Test3()
        {
            Assert.AreEqual("Not enough money!", machine.Choose("A01"));
        }

        [TestMethod]
        public void Test4()
        {
            machine.Insert(1.00M);
            Assert.AreEqual("Not enough money!", machine.Choose("A01"));
            Assert.AreEqual(1.00M, machine.GetChange);
            Assert.AreEqual("Vending Carampar", machine.Choose("A02"));
            Assert.AreEqual(0.40M, machine.GetChange);
        }

        [TestMethod]
        public void Test5()
        {
            machine.Insert(1.00M);

            Assert.AreEqual("Invalid selection!", machine.Choose("A05"));
        }

        [TestMethod]
        public void Test6()
        {
            machine.Insert(6.00M);
            Assert.AreEqual("Vending KokoKola", machine.Choose("A04"));
            Assert.AreEqual("Item KokoKola: Out of stock!", machine.Choose("A04"));
            Assert.AreEqual(3.05M, machine.GetChange);
        }

        [TestMethod]
        public void Test7()
        {
            machine.Insert(6.00M);
            Assert.AreEqual("Vending KokoKola", machine.Choose("A04"));
            machine.Insert(6.00M);
            Assert.AreEqual("Item KokoKola: Out of stock!", machine.Choose("A04"));
            Assert.AreEqual("Vending Smarlies", machine.Choose("A01"));
            Assert.AreEqual("Vending Carampar", machine.Choose("A02"));
            Assert.AreEqual("Vending Carampar", machine.Choose("A02"));
            Assert.AreEqual(6.25M, machine.GetChange);
            Assert.AreEqual(5.75M, machine.GetBalance);
        }

        [TestMethod]
        public void Test8()
        {
            machine.Insert(1000.00M);
            machine.SetTime = "2020-01-01T20:30:00";
            machine.Choose("A01");
            machine.SetTime = "2020-03-01T23:30:00";
            machine.Choose("A01");
            machine.SetTime = "2020-03-04T09:22:00";
            machine.Choose("A01");
            machine.SetTime = "2020-04-01T23:00:00";
            machine.Choose("A01");
            machine.SetTime = "2020-04-01T23:59:59";
            machine.Choose("A01");
            machine.SetTime = "2020-04-04T09:00:00";
            machine.Choose("A01");

            Assert.AreEqual("Hour 23 generated a revenue of 4,80 \nHour 9 generated a revenue of 3,20 \nHour 20 generated a revenue of 1,60 \n", machine.GetBestRevenueHours());

        }
    }
}
