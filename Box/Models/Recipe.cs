using System.Collections.Generic;

namespace Box.Models
{
public class Recipe {
  public Recipe()
  {
    this.Ingredients = new HashSet<RecipeIngredient>();
    this.MealTypes = new HashSet<MealTypeRecipe>();
  }
  public int RecipeId { get; set; }
  public string RecName { get; set; }
  public string RecDesc { get; set; }
  public string RecInst { get; set; }
  public decimal RecRating { get; set; }
  public virtual User User { get; set; }
  public virtual ICollection<RecipeIngredient> Ingredients { get; set; } 
  public ICollection<MealTypeRecipe> MealTypes { get; set; }
  }
}