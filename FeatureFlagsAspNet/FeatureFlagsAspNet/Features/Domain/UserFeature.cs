using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureFlagsAspNet.Features.Domain
{
    public class UserFeature
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
