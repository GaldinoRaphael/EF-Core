using System;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //InserirDados();
            // InserirDadosEmMassa();
            // ConsultarDados();
            AtualizarDados();
        }

        // private static void ConsultarPedidoCarregamentoAdiantado()
        // {
        //     using (var db = new Data.ApplicationContext())
        //     {
        //         var pedidos = db
        //             .Pedidos
        //             //Inclui no carregamento adiantado os itens em pedido
        //             .Include(p => p.Itens)
        //                 //Inclui os pedidos que estão dentro de item
        //                 .thenInclude(p => p.produto)
        //                 .ToList();

        //         Console.WriteLine(pedidos);
        //     }
        // }
        public static void RemoverRegistros()
        {
            using (var db = new Data.ApplicationContext())
            {
                //var cliente = db.Clientes.Find(2);
                //db.Clientes.Remove(cliente);
                //db.Remove(cliente);
                var cliente = new Cliente { Id = 1 };
                db.Entry(cliente).State = EntityState.Deleted;

                db.SaveChanges();
            }
        }
        public static void AtualizarDados()
        {
            using (var db = new Data.ApplicationContext())
            {
                var cliente = db.Clientes.Find(1);
                cliente.Nome = "Cliente Teste";
                //db.Clientes.Update(cliente);
                db.SaveChanges();

            }

            using (var db = new Data.ApplicationContext())
            {
                var cliente = db.Clientes.Find(1);

                var clienteDesconectado = new
                {
                    Nome = "Cliente desconectado",
                    Telefone = "12345648"
                };

                //Copia as propiedades do objeto passado no setvalues e coloca no objeto rastreado pelo entry
                db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            }

        }
        // private static void ConsultarDados()
        // {
        //     using (var db = new Data.ApplicationContext())
        //     {
        //         //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
        //         //AsNoTracking não gera o rastrio e portando não gera os arquivos em memória
        //         var consultaPorMetodo = db.Clientes.AsNoTracking.FirstOfDefault(p => p.Id > 0).ToList();
        //         foreach (var cliente in consultaPorMetodo)
        //         {
        //             //Procura primeiro por clientes em memória
        //             db.Clientes.Find(cliente.id);
        //         }
        //     }
        // }
        private static void InserirDadosEmMassa()
        {
            var produtos = new[]
            {
                new Produto
                {
                    CodigoBarras = "123456",
                    Ativo = true,
                    Descricao = "Produto Teste",
                    TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                    valor = 10m,
                },
                new Produto
                {
                    CodigoBarras = "123456",
                    Ativo = true,
                    Descricao = "Produto Teste",
                    TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                    valor = 10m,
                },
                new Produto
                {
                    CodigoBarras = "123456",
                    Ativo = true,
                    Descricao = "Produto Teste",
                    TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                    valor = 10m,
                }
            };

            // var cliente = new Cliente
            // {
            //     Cep = "1235678",
            //     Cidade = "Divinópolis",
            //     Estado = "mg",
            //     Nome = "joao",
            //     Telefone = "99000001111",
            // };
            using var db = new Data.ApplicationContext();
            // db.AddRange(produto, cliente);
            db.AddRange(produtos);
            var registros = db.SaveChanges();

            Console.WriteLine($"O total de registros foi: {registros}");
        }
        private static void InserirDados()
        {
            var produto = new Produto
            {
                CodigoBarras = "123456",
                Ativo = true,
                Descricao = "Produto Teste",
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                valor = 10m
            };

            using var db = new Data.ApplicationContext();
            //EF Rastrea na memória os daods
            db.Produtos.Add(produto);
            //Persiste na base de dados
            var registros = db.SaveChanges();
            Console.WriteLine($"O total de registros foi: {registros}");
        }
    }
}
