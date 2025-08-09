using DishesMinimalApi.Exceptions;
using DishesMinimalApi.Extensions.EndpointsGroupers;
using DishesMinimalApi.Features.Dishes.Documentation;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Resources;
using DishesMinimalApi.Shared.Abstractions;
using DishesMinimalApi.Shared.Constants;

namespace DishesMinimalApi.Features.Dishes.GetById;

public static partial class Dishes
{
    public record DishWithIngredientsDto(Guid Id, string Name, List<DishIngredientDto> Ingredients);
    public record DishIngredientDto(Guid Id, string Name);

    public class GetByIdWithIngredientsEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = DishesGrouper.Get(app);
            group.MapGet("/{id:guid}", GetDishByIdWithIngredients)
               .Produces<DishWithIngredientsDto>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status404NotFound)
               .WithName(EndPointsNames.GetDishById)
               .WithSummary(DishesDocumentationConstants.GetByIdWithIngredients.EndPoint_Summary)
               .WithDescription(DishesDocumentationConstants.GetByIdWithIngredients.EndPoint_Description);
        }
    }

    public static async Task<IResult> GetDishByIdWithIngredients(Guid id, IDishRepository dishRepository) 
    {
        var dishWithIngredientsDto = await dishRepository.GetDishWithIngredientsDtoByIdAsync(id);
        if (dishWithIngredientsDto is null)
            throw new CustomNotFoundException(Messages.EntityNotFound);

        return Results.Ok(dishWithIngredientsDto);
    }
}
