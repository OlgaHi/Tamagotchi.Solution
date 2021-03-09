using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Tamagotchi.Models
{
  public class Pet
  {
    public int Food { get; set; } = 100;
    public int Attention { get; set; } = 100;
    public int Sleep { get; set; } = 100;

    public string Mood { get; }
    public string Name { get; set; }
    public int Id { get; set; }


    public Pet(string name)
    {
      Name = name;
    }

    public Pet (int id, string name, int food, int attention, int sleep, string mood)
    {
      Name = name;
      Id = id;
      Food = food;
      Attention = attention;
      Sleep = sleep;
      Mood = mood;
    }

    public override bool Equals(System.Object otherPet)
    {
      if (!(otherPet is Pet))
      {
        return false;
      }
      else
      {
        Pet newPet = (Pet) otherPet;
        // bool idEquality = (this.Id == newPet.Id);
        bool nameEquality = (this.Name == newPet.Name);
        // return (idEquality && nameEquality);
        return (nameEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"INSERT INTO pets (name) VALUES (@PetName);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@PetName";
      name.Value = this.Name;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      // Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


    public static List<Pet> GetAll()
    {
      List<Pet> allPets = new List<Pet> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM pets;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int petId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int food = rdr.GetInt32(0);
        int attention = rdr.GetInt32(3);
        int sleep = rdr.GetInt32(4);
        string mood = rdr.GetString(5);

        Pet newPet = new Pet(petId, name, food, attention, sleep, mood);
        allPets.Add(newPet);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allPets;
    }
    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM pets;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Pet Find(int searchId)
    {
      Pet placeholderItem = new Pet("placeholder item");
      return placeholderItem;
    }

    public static void DecreaseStats()
    {
      // foreach (Pet pet in _instances)
      // {
      //   pet.Food -= 10;
      //   pet.Attention -= 10;
      //   pet.Sleep -= 10;
      // }
    }

    public void IncreaseFood()
    {
      this.Food = 100;
    }

    public void IncreaseAttention()
    {
      this.Attention = 100;
    }

    public void IncreaseSleep()
    {
      this.Sleep = 100;
    }

    public static void RemovePet(int searchId)
    {
      // Pet petToRemove = Pet.Find(searchId);
      // _instances.Remove(petToRemove);

      // //reassign ids
      // for (int i = 0; i < _instances.Count; i++)
      // {
      //   _instances[i].Id = i + 1;
      // }
    }
  }
}


// Form that takes a name and then redirects
// Properties: Food, Attention, Rest (Private), (getters/setters)
// HomePage displays all the properties and names
// Buttons Feed, Play, Sleep should affect their respective properties
// Make time pass each button press (decrease all properties by an amount)
// If any property hits 0, it should announce that its dead