using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock.Models;

namespace Stock.Controllers
{
    public class ProductosPorUsuariosController : Controller
    {
        private readonly StockContext _context;

        public ProductosPorUsuariosController(StockContext context)
        {
            _context = context;
        }

        // GET: ProductosPorUsuarios
        public async Task<IActionResult> Index()
        {
            var stockContext = _context.ProductosPorUsuario.Include(p => p.Producto).Include(p => p.Usuario);
            return View(await stockContext.ToListAsync());
        }

        // GET: ProductosPorUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductosPorUsuario == null)
            {
                return NotFound();
            }

            var productoPorUsuario = await _context.ProductosPorUsuario
                .Include(p => p.Producto)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (productoPorUsuario == null)
            {
                return NotFound();
            }

            return View(productoPorUsuario);
        }

        // GET: ProductosPorUsuarios/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: ProductosPorUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,ProductoId")] ProductoPorUsuario productoPorUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoPorUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", productoPorUsuario.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", productoPorUsuario.UsuarioId);
            return View(productoPorUsuario);
        }

        // GET: ProductosPorUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductosPorUsuario == null)
            {
                return NotFound();
            }

            var productoPorUsuario = await _context.ProductosPorUsuario.FindAsync(id);
            if (productoPorUsuario == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", productoPorUsuario.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", productoPorUsuario.UsuarioId);
            return View(productoPorUsuario);
        }

        // POST: ProductosPorUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,ProductoId")] ProductoPorUsuario productoPorUsuario)
        {
            if (id != productoPorUsuario.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoPorUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoPorUsuarioExists(productoPorUsuario.ProductoId))
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
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", productoPorUsuario.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", productoPorUsuario.UsuarioId);
            return View(productoPorUsuario);
        }

        // GET: ProductosPorUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductosPorUsuario == null)
            {
                return NotFound();
            }

            var productoPorUsuario = await _context.ProductosPorUsuario
                .Include(p => p.Producto)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (productoPorUsuario == null)
            {
                return NotFound();
            }

            return View(productoPorUsuario);
        }

        // POST: ProductosPorUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductosPorUsuario == null)
            {
                return Problem("Entity set 'StockContext.ProductosPorUsuario'  is null.");
            }
            var productoPorUsuario = await _context.ProductosPorUsuario.FindAsync(id);
            if (productoPorUsuario != null)
            {
                _context.ProductosPorUsuario.Remove(productoPorUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoPorUsuarioExists(int id)
        {
          return _context.ProductosPorUsuario.Any(e => e.ProductoId == id);
        }
    }
}
