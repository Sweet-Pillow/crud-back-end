using Microsoft.EntityFrameworkCore;
using crud_back_end.Models;

namespace crud_back_end.Data{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> usuario { get; set; } = null!;
        public DbSet<Contato> contato { get; set; } = null!;
        public DbSet<HistoricoLigacao> historico_ligacao { get; set; } = null!;
    }
}