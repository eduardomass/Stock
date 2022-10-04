using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stock.Models;

    public class StockContext : DbContext
    {
        public StockContext (DbContextOptions<StockContext> options)
            : base(options)
        {
        }

        public DbSet<Stock.Models.Categoria> Categoria { get; set; } = default!;

    public DbSet<Stock.Models.Producto> Productos { get; set; } = default!;
}
