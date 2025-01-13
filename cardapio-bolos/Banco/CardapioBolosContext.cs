using CardapioBolos.Model;
using Microsoft.EntityFrameworkCore;

namespace CardapioBolos.Banco
{
    public class CardapioBolosContext: DbContext
    {
        public DbSet<Bolo> Bolos { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<Administrador> Administrador { get; set; }

        private string connectionString = "Server=host.docker.internal,1433;Database=CardapioDeBolos;Trusted_Connection=false;Encrypt=false;User Id=sa;Password=BDVitrine#10;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Encomenda>()
                .HasMany(e => e.Bolos)
                .WithMany(b => b.Encomendas)
                .UsingEntity<Dictionary<string, object>>(
                    "BoloEncomenda",
                    j => j.HasOne<Bolo>().WithMany().HasForeignKey("BoloId"),
                    j => j.HasOne<Encomenda>().WithMany().HasForeignKey("EncomendaId")
                );

            modelBuilder.Entity<Bolo>()
                .HasOne(b => b.Administrador)
                .WithMany(a => a.Bolos)
                .HasForeignKey(b => b.AdministradorId);
        }
    }
}
