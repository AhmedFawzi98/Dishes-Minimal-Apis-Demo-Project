using DishesMinimalApi.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DishesAPI.Entities;

public class Dish 
{ 
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    public ICollection<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>();

    public Dish()
    { 
    }

    [SetsRequiredMembers]
    public Dish(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
