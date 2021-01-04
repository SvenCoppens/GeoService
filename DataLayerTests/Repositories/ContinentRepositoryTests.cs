using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomeinLaag.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DomeinLaag.Model;
using ManualTesting;

namespace DomeinLaag.Interfaces.Tests
{
    [TestClass()]
    public class ContinentRepositoryTests
    {
        private TestDataAccess GetTestDataAccess()
        {
            return new TestDataAccess();
        }
        [TestMethod()]
        public void AddContinentTest_GetContinentTest_ShouldWorkCorrectly()
        {
            string name = "testname";
            Continent continent = new Continent(name);
            var data = GetTestDataAccess();

            data.Continents.AddContinent(continent);

            var result = data.Continents.GetContinentForId(1);
            Assert.IsTrue(result.Name == name);
            Assert.IsTrue(result.Id == 1);
        }
        [TestMethod()]
        public void AddContinentTest_ReturnsAddedContinent()
        {
            string name = "testname";
            Continent continent = new Continent(name);
            var data = GetTestDataAccess();

            var result1 = data.Continents.AddContinent(continent);
            var result2 = data.Continents.GetContinentForId(1);
            Assert.IsTrue(result1.Equals(result2));
        }
        [TestMethod()]
        public void DeleteContinentTest_ShouldWorkCorrectly()
        {
            string name = "testname";
            Continent continent = new Continent(name);
            var data = GetTestDataAccess();

            data.Continents.AddContinent(continent);
            data.Continents.DeleteContinent(1);
            var result = data.Continents.GetContinentForId(1);

            Assert.IsTrue(result == null);
        }

        [TestMethod()]
        public void UpdateContinentTest_ShouldUpdateCorrectly()
        {
            string name = "testname";
            string newName = "newName";
            Continent continent = new Continent(name);
            var data = GetTestDataAccess();

            Continent addedContinent = data.Continents.AddContinent(continent);
            addedContinent.Name = newName;
            data.Continents.UpdateContinent(addedContinent);
            Continent updatedContinent = data.Continents.GetContinentForId(1);
            Assert.IsTrue(updatedContinent.Id == 1);
            Assert.IsTrue(updatedContinent.Name == newName);
        }

        [TestMethod()]
        public void IsNameAvailableTest()
        {
            string name = "testname";
            Continent continent = new Continent(name);
            var data = GetTestDataAccess();
            Assert.IsTrue(data.Continents.IsNameAvailable(name), "The name returned false when there was nothing in the database.");

            data.Continents.AddContinent(continent);
            Assert.IsTrue(!data.Continents.IsNameAvailable(name), "The name returned true when the name was in the database.");
            Assert.IsTrue(data.Continents.IsNameAvailable(name + "s"), "The name returned false when the name wasn't in the database.");
        }
        [TestMethod()]
        public void GetContinentTest_ShouldReturnEntireDataStructure()
        {

        }
    }
}