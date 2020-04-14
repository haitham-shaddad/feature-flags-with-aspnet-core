using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FeatureFlagsAspNet.Models;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureFlagsAspNet.Controllers
{
    [FeatureGate("NewFeature")]
    public class AnotherNewFeatureController : Controller
    {
        private readonly ILogger<AnotherNewFeatureController> _logger;

        public AnotherNewFeatureController(ILogger<AnotherNewFeatureController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Content("This is a another new feature");
        }
    }
}
