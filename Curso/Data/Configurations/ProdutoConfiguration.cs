using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            //Define a propriedade como primary key
            builder.HasKey(p => p.Id);
            //Define a coluna no banco como VARCHAR e como obrigatório
            builder.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(p => p.Descricao).HasColumnType("VARCHAR(60)").IsRequired();
            builder.Property(p => p.valor).IsRequired();
            //Has conversion nesse caso é para poder acessar a parte literal do enum
            builder.Property(p => p.TipoProduto).HasConversion<string>();
        }
    }
}