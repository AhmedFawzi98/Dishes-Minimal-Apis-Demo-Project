using DishesAPI.Entities;
using static DishesMinimalApi.Features.Dishes.GetAll.Dishes;
using static DishesMinimalApi.Features.Dishes.GetById.Dishes;

namespace DishesMinimalApi.Infrastructure.Repositories.Abstractions;

public interface IDishRepository
{
    Task<IEnumerable<DishDto>> GetAllDishDtosAsync();
    Task<DishWithIngredientsDto> GetDishWithIngredientsDtoByIdAsync(Guid id);
    Task<Dish> GetDishByIdAsync(Guid id);
    void Add(Dish dish);
    Task SaveChangesAsync();
    void Delete(Dish dishToDelete);
}
