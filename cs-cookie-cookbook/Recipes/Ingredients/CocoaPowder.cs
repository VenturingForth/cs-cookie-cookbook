namespace cs_cookie_cookbook.Recipes.Ingredients
{
    public class CocoaPowder : Ingredient
    {
        public override int Id => 8;
        public override string Name => "Cocoa Powder";
        public override string PreparationInstructions => $"{base.PreparationInstructions}";
    }
}
