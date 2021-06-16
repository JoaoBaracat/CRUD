using CRUD.Net.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.Net.Infra.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).IsRequired()
                .HasMaxLength(200)
                .HasColumnType("VARCHAR(200)");

            builder.HasOne(p => p.Fornecedor).WithMany().IsRequired();

            builder.Property(p => p.Quantidade).IsRequired();

            builder.ToTable("Produtos");

        }
    }
}
