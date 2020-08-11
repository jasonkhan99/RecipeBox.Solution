using System.Collections.Generic;

namespace Box.Models
{

  public class Ingredient
  {
    public Ingredient()
    {
      this.Recipes = new HashSet<RecipeIngredient>();
    }
    public int IngredientId { get; set; }
    public string Name { get; set; }
    public float Quantity { get; set; }
    public string Prep { get; set; }
    public ICollection<RecipeIngredient> Recipes { get; set; }
  }
}