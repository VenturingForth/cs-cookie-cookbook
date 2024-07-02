
using CookieCookbook.Recipes;
using cs_cookie_cookbook.Recipes.Ingredients;

var cookiesRecipeApp = new CookiesRecipeApp(
    new RecipesRepository(),
    new RecipesConsoleUserInteraction(new IngredientsRegister()));

cookiesRecipeApp.Run("recipes.txt");

public class CookiesRecipeApp
{
    private readonly IRecipesRepository _recipesRepository;
    private readonly IRecipesUserInteraction _recipesUserInteraction;

    public CookiesRecipeApp(
        IRecipesRepository recipesRepository,
        IRecipesUserInteraction recipesUserInteraction
        )
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction;
    }
    public void Run(string filePath)
    {
        var allRecipes = _recipesRepository.Read(filePath);
        _recipesUserInteraction.PrintExistingRecipes(allRecipes);

        _recipesUserInteraction.PromptToCreateRecipe();

        //var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        //if(ingredients.Count > 0)
        //{
        //    var recipes = new CookiesRecipeApp(ingredients);
        //    allRecipes.Add(recipe);
        //    _recipesRepository.Write(filePath, allRecipes);

        //    _recipesUserInteraction.showMessage("Recipe added:");
        //    _recipesUserInteraction.showMessage(recipe.ToString());
        //}
        //else
        //{
        //    _recipesUserInteraction.ShowMessage(
        //        @"No ingredients have been selected.
        //        Recipe will not be saved"
        //    );
        //}

        _recipesUserInteraction.Exit();
    }
}
public interface IRecipesUserInteraction
{
    void ShowMessage( string message );
    void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);
    void PromptToCreateRecipe();
}
public class IngredientsRegister
{
    public IEnumerable<Ingredient> All {  get; } = new List<Ingredient>
    {
        new WheatFlour(),
        new CoconutFlour(),
        new Butter(),
        new Chocolate(),
        new Sugar(),
        new Cardamom(),
        new Cinnamon(),
        new CocoaPowder()
    };
}
public class RecipesConsoleUserInteraction : IRecipesUserInteraction
{
    private readonly IngredientsRegister _ingredientsRegister;
    public RecipesConsoleUserInteraction(IngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
    }
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
    public void Exit()
    {
        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }

    public void PrintExistingRecipes(IEnumerable<Recipe> allRecipes)
    {
        if(allRecipes.Count() > 0)
        {
            Console.WriteLine($"Existing recipes are: {Environment.NewLine}");
            int counter = 1;
            foreach(var recipe in allRecipes)
            {
                Console.WriteLine($"*****{counter}*****");
                Console.WriteLine(recipe);
                Console.WriteLine();
                ++counter;
            }
        }
    }

    public void PromptToCreateRecipe()
    {
        Console.WriteLine("Create a new cookie recipe! " +
            "Available ingredients are:");
        foreach(var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }
}
public interface IRecipesRepository
{
    List<Recipe> Read(string filePath);
}
public class RecipesRepository: IRecipesRepository
{
    public List<Recipe> Read(string filePath)
    {
        return new List<Recipe>
        {
            new Recipe(new List<Ingredient>
            {
                new WheatFlour(),
                new Butter(),
                new Sugar()
            }),
            new Recipe(new List<Ingredient>
            {
                new CocoaPowder(),
                new CoconutFlour(),
                new Cinnamon()
            })
        };
    }
}