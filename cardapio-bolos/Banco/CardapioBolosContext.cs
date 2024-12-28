using CardapioBolos.Model;
using Microsoft.EntityFrameworkCore;

namespace CardapioBolos.Banco
{
    public class CardapioBolosContext: DbContext
    {
        public DbSet<Bolo> Bolos { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }

        private string connectionString = "Data Source=DESKTOP-VB2NFI2;Initial Catalog=CardapioDeBolos;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
        }
    }
}
