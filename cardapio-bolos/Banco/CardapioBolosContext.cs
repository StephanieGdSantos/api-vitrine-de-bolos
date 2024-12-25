using CardapioBolos.Model;
using Microsoft.EntityFrameworkCore;

namespace CardapioBolos.Banco
{
    public class CardapioBolosContext: DbContext
    {
        public DbSet<Bolo> Bolos { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }

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

            modelBuilder.Entity<Bolo>()
                .HasMany(b => b.ListaIngredientes)
                .WithMany(i => i.Bolos)
                .UsingEntity<Dictionary<string, object>>(
                    "BoloIngrediente",
                    j => j.HasOne<Ingrediente>().WithMany().HasForeignKey("IngredienteId"),
                    j => j.HasOne<Bolo>().WithMany().HasForeignKey("BoloId"));
        }
    }
}
