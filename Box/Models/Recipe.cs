using System.Collections.Generic;

namespace Box.Models
{
public class Recipe {
  public int RecId { get; set; }
  public string RecName { get; set; }
  public string RecDesc { get; set; }
  public string RecInst { get; set; }
  public decimal RecRating { get; set; }
  public List<Ingredient> Ingredients {}

  public Recipe(int id, string name, string description, string instructions, decimal rating)
  {
    RecId = id;
    RecName = name;
    RecDesc = description;
    RecInst = instructions;
    RecRating = rating;
    Ingredients = new List<Ingredient>{};
    }
  }
}