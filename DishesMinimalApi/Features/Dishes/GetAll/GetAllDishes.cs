using DishesMinimalApi.Extensions.EndpointsGroupers;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Shared.Abstractions;
using DishesMinimalApi.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DishesMinimalApi.Features.Dishes.GetAll;

public static partial class Dishes
{
    public record DishDto(Guid Id, string Name);

    public class GetAllDishesEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = DishesGrouper.Get(app);
            group.MapGet("", GetAllDishesHandler)
                .Produces<IEnumerable<DishDto>>(StatusCodes.Status200OK)
                .WithName(EndPointsNames.GetAllDishes);
        }
    }

    public static async Task<IResult> GetAllDishesHandler(IDishRepository dishRepository) 
    {
        var dishesDtos = await dishRepository.GetAllDishDtosAsync();
        return Results.Ok(dishesDtos);
    }
}
