using DishesAPI.Entities;
using DishesMinimalApi.Exceptions;
using DishesMinimalApi.Extensions.EndpointsGroupers;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Resources;
using DishesMinimalApi.Shared.Abstractions;
using DishesMinimalApi.Shared.Constants;
using static DishesMinimalApi.Features.Dishes.GetById.Dishes;

namespace DishesMinimalApi.Features.Dishes.Update;

public static partial class Dishes
{
    public record UpdateDishDto(string Name);

    public class UpdateDishEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = DishesGrouper.Get(app);
            group.MapPatch("/{id:guid}", UpdateDishHandler)
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithName(EndPointsNames.UpdateDish);
        }
    }

    public static async Task<IResult> UpdateDishHandler(Guid id, UpdateDishDto updateDishDto, IDishRepository dishRepository) 
    {
        var dishToUpdate = await dishRepository.GetDishByIdAsync(id);
        if(dishToUpdate == null) 
            throw new CustomNotFoundException(Messages.EntityNotFound);

        dishToUpdate.Name = updateDishDto.Name;

        await dishRepository.SaveChangesAsync();
        
        return Results.NoContent();
    }
}
