using DishesAPI.DbContexts;
using DishesAPI.Entities;
using DishesMinimalApi.Infrastructure.Entities;

namespace DishesMinimalApi.Infrastructure.Seeders;

public sealed class DbSeeder(DishesDbContext _context) : IDbSeeder
{
    public async Task SeedAsync()
    {
        await SeedIngredientsAsync();
        await SeedDishesAsync();
        await SeedDishIngredientsAsync();
        await _context.SaveChangesAsync();
    }

    private async Task SeedIngredientsAsync()
    {
        if (_context.Ingredients.Any())
            return;

        _context.Ingredients.AddRange(
            new Ingredient(Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Beef"),
            new Ingredient(Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Onion"),
            new Ingredient(Guid.Parse("c19099ed-94db-44ba-885b-0ad7205d5e40"), "Dark beer"),
            new Ingredient(Guid.Parse("0c4dc798-b38b-4a1c-905c-a9e76dbef17b"), "Brown piece of bread"),
            new Ingredient(Guid.Parse("937b1ba1-7969-4324-9ab5-afb0e4d875e6"), "Mustard"),
            new Ingredient(Guid.Parse("7a2fbc72-bb33-49de-bd23-c78fceb367fc"), "Chicory"),
            new Ingredient(Guid.Parse("b5f336e2-c226-4389-aac3-2499325a3de9"), "Mayo"),
            new Ingredient(Guid.Parse("c22bec27-a880-4f2a-b380-12dcd99c61fe"), "Various spices"),
            new Ingredient(Guid.Parse("aab31c70-57ce-4b6d-a66c-9c1b094e915d"), "Mussels"),
            new Ingredient(Guid.Parse("fef8b722-664d-403f-ae3c-05f8ed7d7a1f"), "Celery"),
            new Ingredient(Guid.Parse("8d5a1b40-6677-4545-b6e8-5ba93efda0a1"), "French fries"),
            new Ingredient(Guid.Parse("40563e5b-e538-4084-9587-3df74fae21d4"), "Tomato"),
            new Ingredient(Guid.Parse("f350e1a0-38de-42fe-ada5-ae436378ee5b"), "Tomato paste"),
            new Ingredient(Guid.Parse("d5cad9a4-144e-4a3d-858d-9840792fa65d"), "Bay leave"),
            new Ingredient(Guid.Parse("b617df23-3d91-40e1-99aa-b07d264aa937"), "Carrot"),
            new Ingredient(Guid.Parse("b8b9a6ae-9bcc-4fb3-b883-5974e04eda56"), "Garlic"),
            new Ingredient(Guid.Parse("ecd396c3-4403-4fbf-83ca-94a8e9d859b3"), "Red wine"),
            new Ingredient(Guid.Parse("c2c75b40-2453-416e-a7ed-3505b121d671"), "Coconut milk"),
            new Ingredient(Guid.Parse("3bd3f0a1-87d3-4b85-94fa-ba92bd1874e7"), "Ginger"),
            new Ingredient(Guid.Parse("047ab5cc-d041-486e-9d22-a0860fb13237"), "Chili pepper"),
            new Ingredient(Guid.Parse("e0017fe1-773f-4a59-9730-9489833c6e8e"), "Tamarind paste"),
            new Ingredient(Guid.Parse("c9b46f9c-d6ce-42c3-8736-2cddbbadee10"), "Firm fish"),
            new Ingredient(Guid.Parse("a07cde83-3127-45da-bbd5-04a7c8d13aa4"), "Ginger garlic paste"),
            new Ingredient(Guid.Parse("ebe94d5d-2ad8-4886-b246-05a1fad83d1c"), "Garam masala")
        );
    }

    private async Task SeedDishesAsync()
    {
        if (_context.Dishes.Any())
            return;

        _context.Dishes.AddRange(
            new Dish(Guid.Parse("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), "Flemish Beef stew with chicory"),
            new Dish(Guid.Parse("fe462ec7-b30c-4987-8a8e-5f7dbd8e0cfa"), "Mussels with french fries"),
            new Dish(Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), "Ragu alla bolognaise"),
            new Dish(Guid.Parse("fd630a57-2352-4731-b25c-db9cc7601b16"), "Rendang"),
            new Dish(Guid.Parse("98929bd4-f099-41eb-a994-f1918b724b5a"), "Fish Masala")
        );
    }

    private async Task SeedDishIngredientsAsync()
    {
        if (_context.DishesIngredients.Any())
            return;

         var dishIngredients = new (Guid DishId, Guid IngredientId)[]
         {
            (Guid.Parse("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35")),
            (Guid.Parse("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96")),
            (Guid.Parse("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), Guid.Parse("c19099ed-94db-44ba-885b-0ad7205d5e40")),
            (Guid.Parse("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), Guid.Parse("0c4dc798-b38b-4a1c-905c-a9e76dbef17b")),
            (Guid.Parse("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), Guid.Parse("937b1ba1-7969-4324-9ab5-afb0e4d875e6")),
            (Guid.Parse("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), Guid.Parse("7a2fbc72-bb33-49de-bd23-c78fceb367fc")),
            (Guid.Parse("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), Guid.Parse("b5f336e2-c226-4389-aac3-2499325a3de9")),
            (Guid.Parse("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), Guid.Parse("c22bec27-a880-4f2a-b380-12dcd99c61fe")),
            (Guid.Parse("eacc5169-b2a7-41ad-92c3-dbb1a5e7af06"), Guid.Parse("d5cad9a4-144e-4a3d-858d-9840792fa65d")),

            (Guid.Parse("fe462ec7-b30c-4987-8a8e-5f7dbd8e0cfa"), Guid.Parse("aab31c70-57ce-4b6d-a66c-9c1b094e915d")),
            (Guid.Parse("fe462ec7-b30c-4987-8a8e-5f7dbd8e0cfa"), Guid.Parse("fef8b722-664d-403f-ae3c-05f8ed7d7a1f")),
            (Guid.Parse("fe462ec7-b30c-4987-8a8e-5f7dbd8e0cfa"), Guid.Parse("8d5a1b40-6677-4545-b6e8-5ba93efda0a1")),
            (Guid.Parse("fe462ec7-b30c-4987-8a8e-5f7dbd8e0cfa"), Guid.Parse("c22bec27-a880-4f2a-b380-12dcd99c61fe")),
            (Guid.Parse("fe462ec7-b30c-4987-8a8e-5f7dbd8e0cfa"), Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96")),

            (Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35")),
            (Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), Guid.Parse("40563e5b-e538-4084-9587-3df74fae21d4")),
            (Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), Guid.Parse("f350e1a0-38de-42fe-ada5-ae436378ee5b")),
            (Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), Guid.Parse("d5cad9a4-144e-4a3d-858d-9840792fa65d")),
            (Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), Guid.Parse("fef8b722-664d-403f-ae3c-05f8ed7d7a1f")),
            (Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), Guid.Parse("b617df23-3d91-40e1-99aa-b07d264aa937")),
            (Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), Guid.Parse("b8b9a6ae-9bcc-4fb3-b883-5974e04eda56")),
            (Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96")),
            (Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), Guid.Parse("ecd396c3-4403-4fbf-83ca-94a8e9d859b3")),
            (Guid.Parse("b512d7cf-b331-4b54-8dae-d1228d128e8d"), Guid.Parse("c22bec27-a880-4f2a-b380-12dcd99c61fe")),

            (Guid.Parse("fd630a57-2352-4731-b25c-db9cc7601b16"), Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35")),
            (Guid.Parse("fd630a57-2352-4731-b25c-db9cc7601b16"), Guid.Parse("c2c75b40-2453-416e-a7ed-3505b121d671")),
            (Guid.Parse("fd630a57-2352-4731-b25c-db9cc7601b16"), Guid.Parse("b8b9a6ae-9bcc-4fb3-b883-5974e04eda56")),
            (Guid.Parse("fd630a57-2352-4731-b25c-db9cc7601b16"), Guid.Parse("3bd3f0a1-87d3-4b85-94fa-ba92bd1874e7")),
            (Guid.Parse("fd630a57-2352-4731-b25c-db9cc7601b16"), Guid.Parse("047ab5cc-d041-486e-9d22-a0860fb13237")),
            (Guid.Parse("fd630a57-2352-4731-b25c-db9cc7601b16"), Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96")),
            (Guid.Parse("fd630a57-2352-4731-b25c-db9cc7601b16"), Guid.Parse("e0017fe1-773f-4a59-9730-9489833c6e8e")),
            (Guid.Parse("fd630a57-2352-4731-b25c-db9cc7601b16"), Guid.Parse("c22bec27-a880-4f2a-b380-12dcd99c61fe")),

            (Guid.Parse("98929bd4-f099-41eb-a994-f1918b724b5a"), Guid.Parse("c9b46f9c-d6ce-42c3-8736-2cddbbadee10")),
            (Guid.Parse("98929bd4-f099-41eb-a994-f1918b724b5a"), Guid.Parse("a07cde83-3127-45da-bbd5-04a7c8d13aa4")),
            (Guid.Parse("98929bd4-f099-41eb-a994-f1918b724b5a"), Guid.Parse("ebe94d5d-2ad8-4886-b246-05a1fad83d1c")),
            (Guid.Parse("98929bd4-f099-41eb-a994-f1918b724b5a"), Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96")),
            (Guid.Parse("98929bd4-f099-41eb-a994-f1918b724b5a"), Guid.Parse("40563e5b-e538-4084-9587-3df74fae21d4")),
            (Guid.Parse("98929bd4-f099-41eb-a994-f1918b724b5a"), Guid.Parse("c2c75b40-2453-416e-a7ed-3505b121d671")),
            (Guid.Parse("98929bd4-f099-41eb-a994-f1918b724b5a"), Guid.Parse("d5cad9a4-144e-4a3d-858d-9840792fa65d")),
            (Guid.Parse("98929bd4-f099-41eb-a994-f1918b724b5a"), Guid.Parse("047ab5cc-d041-486e-9d22-a0860fb13237")),
            (Guid.Parse("98929bd4-f099-41eb-a994-f1918b724b5a"), Guid.Parse("c22bec27-a880-4f2a-b380-12dcd99c61fe"))
         };


        foreach (var (dishId, ingredientId) in dishIngredients)
        {
            _context.DishesIngredients.Add(new DishIngredient(dishId, ingredientId));
        }
    }
}