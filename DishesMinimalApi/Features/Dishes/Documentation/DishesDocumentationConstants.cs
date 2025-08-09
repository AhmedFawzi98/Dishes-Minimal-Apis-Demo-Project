namespace DishesMinimalApi.Features.Dishes.Documentation;

public static class DishesDocumentationConstants
{
    public static class Create
    {
        public const string EndPoint_Summary = "Creates a new dish.";
        public const string EndPoint_Description = "Adds a new dish to the menu.";
        public const string CreateDishDto_Name_Description = "Name of the dish to create.";
    }

    public static class Delete
    {
        public const string EndPoint_Summary = "Deletes an existing dish.";
        public const string EndPoint_Description = "Removes the dish with the specified ID from the menu.";
        public const string Parameter_Id_Description = "Unique identifier of the dish to delete.";
    }

    public static class GetAll
    {
        public const string EndPoint_Summary = "Retrieves all dishes.";
        public const string EndPoint_Description = "Returns a list of all available dishes.";
    }

    public static class GetByIdWithIngredients
    {
        public const string EndPoint_Summary = "Retrieves a dish with its ingredients.";
        public const string EndPoint_Description = "Returns the dish and its associated ingredients by the specified ID.";
        public const string Parameter_Id_Description = "Unique identifier of the dish to retrieve.";
    }

    public static class Update
    {
        public const string EndPoint_Summary = "Updates the details of an existing dish.";
        public const string EndPoint_Description = "Modifies the name or details of the dish with the specified ID.";
        public const string Parameter_Id_Description = "Unique identifier of the dish to update.";
        public const string UpdateDishDto_Name_Description = "New name for the dish.";
    }
}
