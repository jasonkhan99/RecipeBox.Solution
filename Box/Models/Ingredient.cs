using System.Collections.Generic;

namespace Box.Models
{

  public class Ingredient
  {
    public int IngredientId { get; set; }
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public float Quantity { get; set; }
    public string Prep { get; set; }
    public virtual Recipe Recipe { get; set; }
  }
}