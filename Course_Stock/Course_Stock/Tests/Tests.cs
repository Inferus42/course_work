using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Course_Stock.Logic.Technologist;
using Course_Stock.Models;

namespace Course_Stock.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void testMax()
        {
            Calculate calc = new Calculate();
            Component comp = new Component();
            List<Component> list = new List<Component>();
            comp.name = "Test";
            comp.quantityInStockNow = 500;
            comp.quantityInProduct = 5;
            comp.quantityInStockAfter = 0;
            list.Add(comp);
            
                Assert.AreEqual(500/5, calc.CalcMax(list));
        }

        [Test]
        public void testResult()
        {
            Calculate calc = new Calculate();
            Component comp = new Component();
            List<Component> list = new List<Component>();
            comp.name = "Test";
            comp.quantityInStockNow = 500;
            comp.quantityInProduct = 5;
            comp.quantityInStockAfter = 0;
            list.Add(comp);
            List<Component> list2 = calc.CalcResult(list, 852);

            foreach (Component c in list2)
            {
                Assert.AreEqual(-3760, c.quantityInStockAfter);
            }
            
        }
    }
}