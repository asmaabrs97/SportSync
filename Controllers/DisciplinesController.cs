using Microsoft.AspNetCore.Mvc;
using SportSync.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Namespace
{
    public class DisciplinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisciplinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DisciplinesController
        public async Task<IActionResult> Index()
        {
            var disciplines = await _context.Sports.ToListAsync();
            return View(disciplines);
        }

        public async Task<IActionResult> Details(int id)
        {
            var discipline = await _context.Sports
                .Include(s => s.Sessions)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (discipline == null)
            {
                return NotFound();
            }

            return View(discipline);
        }

        public async Task<IActionResult> SessionDetails(int disciplineId, int sessionId)
        {
            var session = await _context.Sessions
                .Include(s => s.Sport)
                .Include(s => s.Coach)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(s => s.Id == sessionId && s.SportId == disciplineId);

            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }
    }
}
