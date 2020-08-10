namespace Box.Models
{
  public class MealTypeRecipe
  {
    public int MealTypeRecipeId { get; set; }
    public int MealTypeId { get; set; }
    public int RecipeId { get; set; }
    public MealType MealType { get; set; }
    public Recipe Recipe { get; set; }
  }
}