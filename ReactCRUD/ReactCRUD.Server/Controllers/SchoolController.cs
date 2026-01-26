using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactCRUD.Core.Domain;
using ReactCRUD.Core.ServiceInterface;
using ReactCRUD.Data;
using ReactCRUD.Server.ViewModels;

namespace ReactCRUD.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : Controller
    {
        private readonly ReactCRUDContext _context;
        private readonly ISchoolInterface _schoolInterface;

        public SchoolController(ReactCRUDContext context, ISchoolInterface schoolInterface)
        {
            _context = context;
            _schoolInterface = schoolInterface;
        }

        [HttpGet]
        public IActionResult GetSchools()
        {
            var result = _context.Schools
                .Select(x => new SchoolListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    StudentCount = x.StudentCount,
                })
                .ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var school = await _schoolInterface.SchoolDetail(id);
            if (school == null)
            {
                return NotFound();
            }

            var result = new SchoolListViewModel
            {
                Id = school.Id,
                Name = school.Name,
                Address = school.Address,
                StudentCount = school.StudentCount,
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SchoolListViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Name) || string.IsNullOrWhiteSpace(vm.Address))
                return BadRequest("Name and Address are required.");

            if (vm.StudentCount < 0)
                return BadRequest("StudentCount cannot be negative.");

            var school = new School
            {
                Id = Guid.NewGuid(),
                Name = vm.Name,
                Address = vm.Address,
                StudentCount = vm.StudentCount
            };

            var created = await _schoolInterface.CreateSchool(school);

            var result = new SchoolListViewModel
            {
                Id = created.Id,
                Name = created.Name,
                Address = created.Address,
                StudentCount = created.StudentCount
            };

            return CreatedAtAction(nameof(Details), new { id = created.Id }, result);
        }

    }
}