using DishesMinimalApi.Extensions.EndpointsGroupers;
using DishesMinimalApi.Features.Dishes.Documentation;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Shared.Abstractions;
using DishesMinimalApi.Shared.Constants;

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
                .WithName(EndPointsNames.GetAllDishes)
                .WithSummary(DishesDocumentationConstants.GetAll.EndPoint_Summary)
                .WithDescription(DishesDocumentationConstants.GetAll.EndPoint_Description);
        }
    }

    public static async Task<IResult> GetAllDishesHandler(IDishRepository dishRepository) 
    {
        var dishesDtos = await dishRepository.GetAllDishDtosAsync();
        return Results.Ok(dishesDtos);
    }
}
