using static DishesMinimalApi.Features.Dishes.GetAll.GetAllDishes;
using static DishesMinimalApi.Features.Dishes.GetById.GetByIdWithIngredients;

namespace DishesMinimalApi.Infrastructure.Repositories.Abstractions;

public interface IDishRepository
{
    Task<IEnumerable<DishDto>> GetAllDishes();
    Task<DishWithIngredientsDto> GetDishById(Guid id);
}
