using FeatureFlagsAspNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FeatureFlagsAspNet.Features
{
    [FilterAlias("UserFeatures")]
    public class UserFeaturesFilter : IFeatureFilter
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserFeaturesFilter(IServiceScopeFactory scopeFactory, IHttpContextAccessor httpContextAccessor)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return false;

            using (var scope = _scopeFactory.CreateScope())
            {
                var featuresContext = scope.ServiceProvider.GetRequiredService<FeaturesContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                var feature = featuresContext.Features.FirstOrDefault(a => a.Name.ToLower() == context.FeatureName.ToLower());

                if (user == null || feature == null)
                    return false;

                var userFeature = featuresContext.UserFeatures.FirstOrDefault(f => f.UserId == user.Id && f.FeatureId == feature.Id);
                return userFeature != null;
            }
        }
    }
}
