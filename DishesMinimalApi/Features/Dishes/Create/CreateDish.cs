using DishesAPI.Entities;
using DishesMinimalApi.Exceptions;
using DishesMinimalApi.Extensions.EndpointsGroupers;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Resources;
using DishesMinimalApi.Shared.Abstractions;
using DishesMinimalApi.Shared.Constants;
using FluentValidation;

namespace DishesMinimalApi.Features.Dishes.Create;

public static partial class Dishes
{
    public record CreateDishDto(string Name);
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
