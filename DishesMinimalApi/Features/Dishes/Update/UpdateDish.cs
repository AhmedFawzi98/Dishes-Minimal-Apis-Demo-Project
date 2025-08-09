using DishesMinimalApi.Exceptions;
using DishesMinimalApi.Extensions.EndpointsGroupers;
using DishesMinimalApi.Features.Dishes.Documentation;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Resources;
using DishesMinimalApi.Shared.Abstractions;
using DishesMinimalApi.Shared.Constants;
using System.ComponentModel;

namespace DishesMinimalApi.Features.Dishes.Update;

public static partial class Dishes
{
    public record UpdateDishDto([Description(DishesDocumentationConstants.Update.UpdateDishDto_Name_Description)] string Name);

    public class UpdateDishEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = DishesGrouper.Get(app);
            group.MapPatch("/{id:guid}", UpdateDishHandler)
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithName(EndPointsNames.UpdateDish)
                .WithSummary(DishesDocumentationConstants.Update.EndPoint_Summary)
                .WithDescription(DishesDocumentationConstants.Update.EndPoint_Description);
        }
    }

    public static async Task<IResult> UpdateDishHandler([Description(DishesDocumentationConstants.Update.Parameter_Id_Description)] Guid id, UpdateDishDto updateDishDto, IDishRepository dishRepository) 
    {
        var dishToUpdate = await dishRepository.GetDishByIdAsync(id);
        if(dishToUpdate == null) 
            throw new CustomNotFoundException(Messages.EntityNotFound);

        dishToUpdate.Name = updateDishDto.Name;

        await dishRepository.SaveChangesAsync();
        
        return Results.NoContent();
    }
}
