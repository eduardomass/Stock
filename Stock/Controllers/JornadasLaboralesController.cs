using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock.Models;
using Stock.ModelsView;
using Stock.Reglas;

namespace Stock.Controllers
{
    public class JornadasLaboralesController : Controller
    {
        private readonly StockContext _context;

        public JornadasLaboralesController(StockContext context)
        {
            _context = context;
        }

        // GET: JornadasLaborales
        public async Task<IActionResult> Index()
        {
            var stockContext = _context.JornadasLaborales.Include(j => j.Trabajador);
            return View(await stockContext.ToListAsync());
        }

        // GET: JornadasLaborales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JornadasLaborales == null)
            {
                return NotFound();
            }

            var jornadaLaboral = await _context.JornadasLaborales
                .Include(j => j.Trabajador)
                .FirstOrDefaultAsync(m => m.JornadaLaboralId == id);
            if (jornadaLaboral == null)
            {
                return NotFound();
            }

            return View(jornadaLaboral);
        }

        // GET: JornadasLaborales/Create
        public IActionResult Create()
        {
            ViewData["TrabajadorId"] = new SelectList(_context.Trabajadores, "TrabajadorId", "NombreYApllido");
            return View();
        }

        // POST: JornadasLaborales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JornadaLaboralId,FechaYHora,TrabajadorId")] JornadaLaboral jornadaLaboral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jornadaLaboral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrabajadorId"] = new SelectList(_context.Trabajadores, "TrabajadorId", "NombreYApllido", jornadaLaboral.TrabajadorId);
            return View(jornadaLaboral);
        }

        // GET: JornadasLaborales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JornadasLaborales == null)
            {
                return NotFound();
            }

            var jornadaLaboral = await _context.JornadasLaborales.FindAsync(id);
            if (jornadaLaboral == null)
            {
                return NotFound();
            }
            ViewData["TrabajadorId"] = new SelectList(_context.Trabajadores, "TrabajadorId", "NombreYApllido", jornadaLaboral.TrabajadorId);
            return View(jornadaLaboral);
        }

        // POST: JornadasLaborales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JornadaLaboralId,FechaYHora,TrabajadorId")] JornadaLaboral jornadaLaboral)
        {
            if (id != jornadaLaboral.JornadaLaboralId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jornadaLaboral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JornadaLaboralExists(jornadaLaboral.JornadaLaboralId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrabajadorId"] = new SelectList(_context.Trabajadores, "TrabajadorId", "TrabajadorId", jornadaLaboral.TrabajadorId);
            return View(jornadaLaboral);
        }

        // GET: JornadasLaborales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JornadasLaborales == null)
            {
                return NotFound();
            }

            var jornadaLaboral = await _context.JornadasLaborales
                .Include(j => j.Trabajador)
                .FirstOrDefaultAsync(m => m.JornadaLaboralId == id);
            if (jornadaLaboral == null)
            {
                return NotFound();
            }

            return View(jornadaLaboral);
        }

        // POST: JornadasLaborales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JornadasLaborales == null)
            {
                return Problem("Entity set 'StockContext.JornadasLaborales'  is null.");
            }
            var jornadaLaboral = await _context.JornadasLaborales.FindAsync(id);
            if (jornadaLaboral != null)
            {
                _context.JornadasLaborales.Remove(jornadaLaboral);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JornadaLaboralExists(int id)
        {
            return _context.JornadasLaborales.Any(e => e.JornadaLaboralId == id);
        }


        // GET: JornadasLaborales/Generacion
        public IActionResult Generacion()
        {
            ViewData["TrabajadorId"] = new SelectList(_context.Trabajadores, "TrabajadorId", "NombreYApllido");
            return View();
        }
        [HttpPost, ActionName("Generacion")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Generacion(GeneradorJornada generador)
        {
            var regla = new RNJornadasLabroales(_context);
            await regla.GenerarJornadasLaborales(generador);
            return RedirectToAction(nameof(Index));
        }
    }
}