using DishesMinimalApi.EndpointFilters;
using DishesMinimalApi.Exceptions;
using DishesMinimalApi.Extensions.EndpointsGroupers;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Resources;
using DishesMinimalApi.Shared.Abstractions;
using DishesMinimalApi.Shared.Constants;
using static DishesMinimalApi.Features.Dishes.GetById.Dishes;

namespace DishesMinimalApi.Features.Dishes.Delete;

public static partial class Dishes
{
    public class DeleteDishEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = DishesGrouper.Get(app);
            group.MapDelete("/{id:guid}", DeleteDishHandler)
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithName(EndPointsNames.DeleteDish);
        }
    }

    public static async Task<IResult> DeleteDishHandler(Guid id, IDishRepository dishRepository) 
    {
        var dishToDelete = await dishRepository.GetDishByIdAsync(id);
        if(dishToDelete == null)
            throw new CustomNotFoundException(Messages.EntityNotFound);

        dishRepository.Delete(dishToDelete);
        await dishRepository.SaveChangesAsync();
        
        return Results.NoContent();
    }
}
