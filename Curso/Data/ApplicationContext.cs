using System;
using System.Linq;
using CursoEFCore.Data.Configurations;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CursoEFCore.Data
{
    public class ApplicationContext : DbContext
    {
        //Informa qual o log que o EF vai utilizar para escrever os logs
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseLoggerFactory(_logger)
            .EnableSensitiveDataLogging()
            .UseSqlServer
                (@"Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEfCore;Integrated Security=true",
                     //Faz tentativas de conexões em caso de erro
                     p => p.EnableRetryOnFailure
                        (maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null)
                        //Muda o nome da tabela onde fica armazenados as histórias de migração
                        .MigrationsHistoryTable("curso_ef_core"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aplica as configurações de modelo em todas as classes
            //que extendem de Ientity no assembly (programa) ApplicationContext
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());

            MapearPropriedadesEsquecidas(modelBuilder);
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));
            }
        }
    }
}