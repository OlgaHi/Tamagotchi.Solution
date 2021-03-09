using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tamagotchi.Models;
using System;

namespace Tamagotchi.Tests
{

  [TestClass]
  public class PetTests : IDisposable
  {

    public void Dispose()
    {
      Pet.ClearAll();
    }

    // The method below is new code.
    public PetTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=tamagotchi_test;";
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_PetList()
    {
      //Arrange
      List<Pet> newList = new List<Pet> { };

      //Act
      List<Pet> result = Pet.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Equals_returnsTrueIfDescriptionsAreTheSame_Pet()
    {
      // Arrange, Act
      Pet firstPet = new Pet("Pikachu");
      Pet secondPet = new Pet ("Pikachu");

      // Assert
      Assert.AreEqual(firstPet, secondPet);
    }

    [TestMethod]
    public void Save_SavesToDatabase_PetList()
    {
      // Arrange
      // Pet testPet = new Pet(1, "Charmander", 100, 100, 100, "Happy");
      Pet testPet = new Pet("Charmander");

      // Act
      testPet.Save();
      List<Pet> result = Pet.GetAll();
      List<Pet> testList = new List<Pet>{testPet};

      // Assert
      CollectionAssert.AreEqual(testList, result);

    }
  }
}