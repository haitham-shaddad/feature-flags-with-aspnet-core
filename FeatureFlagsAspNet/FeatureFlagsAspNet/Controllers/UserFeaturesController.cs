using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeatureFlagsAspNet.Data;
using FeatureFlagsAspNet.Features.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FeatureFlagsAspNet.Controllers
{
    [Authorize]
    public class UserFeaturesController : Controller
    {
        private readonly FeaturesContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public UserFeaturesController(FeaturesContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserFeatures
        public async Task<IActionResult> Index()
        {
            var featuresContext = _context.UserFeatures.Include(u => u.Feature);
            return View(await featuresContext.ToListAsync());
        }


        // GET: UserFeatures/Create
        public IActionResult Create()
        {
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Title");
            ViewBag.UserId = _userManager.GetUserId(Request.HttpContext.User);
            return View();
        }

        // POST: UserFeatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FeatureId")] UserFeature userFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Title", userFeature.FeatureId);
            return View(userFeature);
        }


        // GET: UserFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFeature = await _context.UserFeatures
                .Include(u => u.Feature)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFeature == null)
            {
                return NotFound();
            }

            return View(userFeature);
        }

        // POST: UserFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userFeature = await _context.UserFeatures.FindAsync(id);
            _context.UserFeatures.Remove(userFeature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFeatureExists(int id)
        {
            return _context.UserFeatures.Any(e => e.Id == id);
        }
    }
}
