namespace Box.Models
{
  public class MealType
  {
    public MealType()
    {
      this.Recipes = new HashSet<Recipe>(); 
    }
    public int MealTypeId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Recipe> Recipes { get; set; }
  }
}