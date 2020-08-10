using System.Collections.Generic;

namespace Box.Models
{
  public class MealType
  {
    public MealType()
    {
      this.Recipes = new HashSet<MealTypeRecipe>();
    }
    public int MealTypeId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<MealTypeRecipe> Recipes { get; set; }
  }
}