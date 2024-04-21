using Cepedi.Banco.Pessoa.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Banco.Pessoa.Dados.EntityTypeConfiguration;
public class PessoaEntityTypeConfiguration : IEntityTypeConfiguration<TelefoneEntity>
{
    public void Configure(EntityTypeBuilder<TelefoneEntity> builder)
    {
        builder.ToTable("Telefone");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.CodPais).IsRequired().HasMaxLength(3);
        builder.Property(t => t.Ddd).IsRequired().HasMaxLength(2);
        builder.Property(t => t.Numero).IsRequired().HasMaxLength(9);
        builder.Property(t => t.Tipo).IsRequired().HasMaxLength();
        builder.Property(t => t.Principal).IsRequired();
       
        builder.HasMany(p => p.Enderecos).WithOne(e => e.Pessoa).HasForeignKey(p => p.IdPessoa);
    }
}
