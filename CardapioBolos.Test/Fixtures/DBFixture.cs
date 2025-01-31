using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardapioBolos.Test.Fixtures
{
    public class DBFixture : IDisposable
    {
        public CardapioBolosContext Context { get; private set; }

        public DBFixture()
        {
            var options = new DbContextOptionsBuilder<CardapioBolosContext>()
                .UseInMemoryDatabase(databaseName: "CardapioDeBolosTest")
                .Options;

            Context = new CardapioBolosContext(options);
            Context.Database.EnsureCreated();

            InserirDados();
        }

        private void InserirDados()
        {
            var administrador = new Administrador("Teste", "123456789", "teste@gmail.com", VerificadorDeSenha.GerarHashSenha("TesteSenha"));
            Context.Administrador.Add(administrador);

            var bolo2 = new Bolo("Bolo de Chocolate", "https://www.receiteria.com.br/wp-content/uploads/receitas-de-bolo-de-chocolate-1-730x449.jpg", "Bolo de chocolate com cobertura de chocolate", new List<Ingrediente>(), 25.0, 1)
            {
                Id = 1
            };
            var bolo1 = new Bolo("Bolo de Cenoura", "https://www.receiteria.com.br/wp-content/uploads/receitas-de-bolo-de-cenoura-1-730x449.jpg", "Bolo de cenoura com cobertura de chocolate", new List<Ingrediente>(), 20.0, 1)
            { 
                Id = 2 
            };
            Context.Bolos.AddRange(bolo1, bolo2);

            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
