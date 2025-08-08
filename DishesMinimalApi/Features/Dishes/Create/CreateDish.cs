using DishesAPI.Entities;
using DishesMinimalApi.Extensions.EndpointsGroupers;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Shared.Abstractions;
using DishesMinimalApi.Shared.Constants;

namespace DishesMinimalApi.Features.Dishes.Create;

public static partial class Dishes
{
    public record CreateDishDto(string Name);
    public record DishCreatedDto(Guid Id, string Name);

    public class CreateDishEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = DishesGrouper.Get(app);
            group.MapPost("", CreateDishHandler)
                .Produces<DishCreatedDto>(StatusCodes.Status200OK)
                .WithName(EndPointsNames.CreateDish);
        }
    }

    public static async Task<IResult> CreateDishHandler(CreateDishDto createDishDto, IDishRepository dishRepository) 
    {
        var dishEntity = new Dish()
        {
            Name = createDishDto.Name
        };

        dishRepository.Add(dishEntity);
        await dishRepository.SaveChangesAsync();

        var dishCreatedDto = new DishCreatedDto(dishEntity.Id, dishEntity.Name);

        return Results.CreatedAtRoute(EndPointsNames.GetDishById, new {id = dishEntity.Id});
    }
}
