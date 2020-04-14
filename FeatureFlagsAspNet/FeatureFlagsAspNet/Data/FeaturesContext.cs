using FeatureFlagsAspNet.Features.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureFlagsAspNet.Data
{
    public class FeaturesContext : DbContext
    {
        public FeaturesContext(DbContextOptions<FeaturesContext> options)
           : base(options)
        {
        }

        public DbSet<Feature> Features { get; set; }
        public DbSet<UserFeature> UserFeatures { get; set; }
    }
}
