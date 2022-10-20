using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stock.Models;

public class StockContext : DbContext
{
    public StockContext(DbContextOptions<StockContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductoPorUsuario>().HasKey(sc => new { sc.ProductoId, sc.UsuarioId });
    }
    public DbSet<Stock.Models.Categoria> Categoria { get; set; } = default!;

    public DbSet<Stock.Models.Producto> Productos { get; set; } = default!;

    public DbSet<Stock.Models.ProductoPorUsuario> ProductosPorUsuario { get; set; } = default!;
    public DbSet<Stock.Models.Usuario> Usuarios { get; set; } = default!;

}
