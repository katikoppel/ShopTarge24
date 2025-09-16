using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Data;
using ShopTARge24.Models.Kindergartens;

namespace ShopTARge24.Controllers
{
    public class KindergartensController : Controller
    {
        private readonly ShopTARge24Context _context;

        public KindergartensController
            (
                ShopTARge24Context context
            )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Kindergartens

                .Select(x => new KindergartenIndexViewModel
                {

                });

            return View(result);
        }
    }
}
