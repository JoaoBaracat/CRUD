using CRUD.Net.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.Net.Infra.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Login).IsRequired()
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)");

            builder.Property(p => p.Senha).IsRequired()
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)");

            builder.ToTable("Usuarios");

        }
    }
}
