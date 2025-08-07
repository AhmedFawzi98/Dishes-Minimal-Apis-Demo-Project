using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Shared.Abstractions;

namespace DishesMinimalApi.Features.Dishes.GetAll;

public static class GetAllDishes
{
    public record DishDto(Guid Id, string Name);

    public class GetAllDishesEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/dishes", GetAllDishesHandler);
        }
    }

    public static async Task<IResult> GetAllDishesHandler(IDishRepository dishRepository) 
    {
        var dishesDtos = await dishRepository.GetAllDishes();
        return Results.Ok(dishesDtos);
    }
}
