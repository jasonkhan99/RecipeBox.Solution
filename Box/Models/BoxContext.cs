using Microsoft.EntityFrameworkCore;

namespace Box.Models
{
public class BoxContext : DbContext
{
  public DbSet<Recipe> Recipes { get; set; }
  public virtual DbSet<Ingredient> Ingredients { get; set; }
  public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
  public Dbset<MealType> MealTypes { get; set; }
  public DbSet<MealTypeRecipe> MealTypeRecipes { get; set; }
  public BoxContext(DbContextOptions options) : base(options) { }
  }
}