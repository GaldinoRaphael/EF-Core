using CursoEFCore.ValueObjects;

namespace CursoEFCore.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public decimal valor { get; set; }

        public TipoProduto TipoProduto { get; set; }
        public bool Ativo { get; set; }
    }
}