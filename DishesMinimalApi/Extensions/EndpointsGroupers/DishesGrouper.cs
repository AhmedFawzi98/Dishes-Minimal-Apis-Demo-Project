using DishesMinimalApi.Shared.Constants;

namespace DishesMinimalApi.Extensions.EndpointsGroupers;
public static class DishesGrouper
{
    public static RouteGroupBuilder Get(IEndpointRouteBuilder app)
    {
        return app.MapGroup(EndPointsRoutes.Dishes)
                  .RequireRateLimiting(RateLimitingPolicies.SlidingWindowPolicy)
                  .WithTags(EndPointsTags.Dishes);
    }
}