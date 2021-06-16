using CRUD.Net.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.Net.Infra.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).IsRequired()
                .HasMaxLength(200)
                .HasColumnType("VARCHAR(200)");

            builder.Property(p => p.CNPJ).IsRequired()
                .HasColumnType("VARCHAR(14)");

            builder.Property(p => p.Endereco).IsRequired()
                .HasMaxLength(200)
                .HasColumnType("VARCHAR(200)");

            builder.Property(p => p.Ativo).IsRequired();

            builder.ToTable("Fornecedores");

        }
    }
}
