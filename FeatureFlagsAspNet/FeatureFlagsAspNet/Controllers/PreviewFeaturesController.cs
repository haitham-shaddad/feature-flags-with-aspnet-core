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
    [FeatureGate("PreviewFeatures")]
    public class PreviewFeaturesController : Controller
    {
        private readonly ILogger<PreviewFeaturesController> _logger;

        public PreviewFeaturesController(ILogger<PreviewFeaturesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Content("This is a preview feature controller");
        }
    }
}
