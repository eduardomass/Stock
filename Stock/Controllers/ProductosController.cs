using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stock.Models;
using Stock.ModelsView;

namespace Stock.Controllers
{
    //[Authorize]
    public class ProductosController : Controller
    {
        private readonly StockContext _context;

        public ProductosController(StockContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var stockContext = _context
                .Productos
                .Include(p => p.Categoria);
            return View(await stockContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        [Authorize]
        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Descripcion");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Descripcion", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Descripcion", producto.CategoriaId);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,CategoriaId")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Descripcion", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'StockContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return _context.Productos.Any(e => e.Id == id);
        }


        // GET: Productos
        public async Task<IActionResult> Productos()
        {
            var stockContext = _context
                .Productos
                .Include(p => p.Categoria);
            return View(await stockContext.ToListAsync());
        }

        // GET: Productos
        public IActionResult Carrito()
        {
            var modelo = this.ProductosEnCarrito;
            return View(modelo);
        }
        // GET: Productos
        public async Task<IActionResult> Agregar(int id)
        {
            var producto = await _context
                .Productos
                .Where(p => p.Id == id)
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync();
            if (producto == null)
            {
                producto = new Producto();
                //falta tratado de error
            }
            ProductoCarrito modelo = new ProductoCarrito()
            {
                Descripcion = producto.Nombre,
                DescripcionCategoria = producto.Categoria.Descripcion,
                Id = producto.Id,
                Cantidad = 0
            };
            return View(modelo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(ProductoCarrito modelo)
        {
            var producto = await _context
               .Productos
               .Where(p => p.Id == modelo.Id)
               .Include(p => p.Categoria)
               .FirstOrDefaultAsync();
            modelo.SetProducto(producto);
            this.AgregarACarrito(modelo);

            return RedirectToAction(nameof(Carrito));
        }

        public List<ProductoCarrito> ProductosEnCarrito { 
            get
            {
                var value = HttpContext.Session.GetString("Productos");
                if (value == null)
                    return new List<ProductoCarrito>();
                else
                    return JsonConvert.DeserializeObject<List<ProductoCarrito>>(value);
            }
            set
            {
                var js = JsonConvert.SerializeObject(value);
                HttpContext.Session.SetString("Productos", js);
            }
        }
        private void AgregarACarrito(ProductoCarrito productoCarrito)
        {
            var carrito = this.ProductosEnCarrito;
            var productoExistente = carrito.Where(o => o.Id == productoCarrito.Id).FirstOrDefault();
            //Si el producto no esta, lo agrego, sino remplazo la cantidad
            if (productoExistente == null)
            {
                carrito.Add(productoCarrito);
                this.ProductosEnCarrito = carrito; //seteo nuevamente el carrito

            }
            else
            {
                productoExistente.Cantidad = productoCarrito.Cantidad;
            }
        }
    }
}
