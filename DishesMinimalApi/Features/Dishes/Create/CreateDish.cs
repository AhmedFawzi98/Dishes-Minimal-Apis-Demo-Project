using Asp.Versioning;
using DishesAPI.Entities;
using DishesMinimalApi.Exceptions;
using DishesMinimalApi.Extensions.EndpointsGroupers;
using DishesMinimalApi.Features.Dishes.Documentation;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Resources;
using DishesMinimalApi.Shared.Abstractions;
using DishesMinimalApi.Shared.Constants;
using FluentValidation;
using System.ComponentModel;

namespace DishesMinimalApi.Features.Dishes.Create;

public static partial class Dishes
{
    public record CreateDishDto([Description(DishesDocumentationConstants.Create.CreateDishDto_Name_Description)] string Name);
    public record DishCreatedDto(Guid Id, string Name);

    public class CreateDishValidator : AbstractValidator<CreateDishDto>
    {
        public CreateDishValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty()
                .WithMessage(Messages.DishNameNullOrEmpty)
                .MaximumLength(20)
                .WithMessage(Messages.DishNameExceedMaxLength);
        }
    }

    public class CreateDishEndpointV1 : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = DishesGrouper.Get(app);
            group.MapPost("", CreateDishHandler)
                .MapToApiVersion(1)
                .Produces<DishCreatedDto>(StatusCodes.Status200OK)
                .WithName(EndPointsNames.CreateDishV1)
                .WithSummary(DishesDocumentationConstants.Create.EndPoint_Summary)
                .WithDescription(DishesDocumentationConstants.Create.EndPoint_Description);
        }
    }

    public class CreateDishEndpointV2 : IEndpoint
    {
        //dummy v2, same implementation
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = DishesGrouper.Get(app);
            var endpoint = group.MapPost("", CreateDishHandler)
                .MapToApiVersion(2)
                .Produces<DishCreatedDto>(StatusCodes.Status200OK)
                .WithName(EndPointsNames.CreateDishV2)
                .WithSummary(DishesDocumentationConstants.Create.EndPoint_Summary)
                .WithDescription(DishesDocumentationConstants.Create.EndPoint_Description);
        }
    }

    public static async Task<IResult> CreateDishHandler(CreateDishDto createDishDto, IDishRepository dishRepository, IValidator<CreateDishDto> validator) 
    {
        var valdiationResult = validator.Validate(createDishDto);
        if (!valdiationResult.IsValid)
            throw new CustomValidationException(valdiationResult.ToDictionary());
       
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
