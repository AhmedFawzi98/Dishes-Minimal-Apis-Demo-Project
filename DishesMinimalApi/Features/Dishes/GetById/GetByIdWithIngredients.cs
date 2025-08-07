using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Resources;
using DishesMinimalApi.Shared.Abstractions;

namespace DishesMinimalApi.Features.Dishes.GetById;

public static class GetByIdWithIngredients
{
    public record DishWithIngredientsDto(Guid Id, string Name, List<DishIngredientDto> Ingredients);
    public record DishIngredientDto(Guid Id, string Name);

    public class GetByIdWithIngredientsEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/dishes/{id:guid}", GetDishByIdWithIngredients);
        }
    }

    public static async Task<IResult> GetDishByIdWithIngredients(Guid id, IDishRepository dishRepository) 
    {
        var dishWithIngredientsDto = await dishRepository.GetDishById(id);
        if (dishWithIngredientsDto is null)
            return Results.NotFound(ExceptionMessages.EntityNotFound);

        return Results.Ok(dishWithIngredientsDto);
    }
}
