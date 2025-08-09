using DishesMinimalApi.Exceptions;
using DishesMinimalApi.Extensions.EndpointsGroupers;
using DishesMinimalApi.Features.Dishes.Documentation;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Resources;
using DishesMinimalApi.Shared.Abstractions;
using DishesMinimalApi.Shared.Constants;
using System.ComponentModel;

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
                .WithName(EndPointsNames.DeleteDish)
                .WithSummary(DishesDocumentationConstants.Delete.EndPoint_Summary)
                .WithDescription(DishesDocumentationConstants.Delete.EndPoint_Description); 
        }
    }

    public static async Task<IResult> DeleteDishHandler([Description(DishesDocumentationConstants.Delete.Parameter_Id_Description)] Guid id, IDishRepository dishRepository) 
    {
        var dishToDelete = await dishRepository.GetDishByIdAsync(id);
        if(dishToDelete == null)
            throw new CustomNotFoundException(Messages.EntityNotFound);

        dishRepository.Delete(dishToDelete);
        await dishRepository.SaveChangesAsync();
        
        return Results.NoContent();
    }
}
