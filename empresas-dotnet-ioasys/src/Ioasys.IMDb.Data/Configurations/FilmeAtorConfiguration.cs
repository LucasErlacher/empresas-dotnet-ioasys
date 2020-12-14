using Ioasys.IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ioasys.IMDb.Data.Configurations
{
    public class FilmeAtorConfiguration : IEntityTypeConfiguration<FilmeAtor>
    {
        public void Configure(EntityTypeBuilder<FilmeAtor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Ator)
                .WithMany()
                .HasForeignKey(x => x.AtorId);

            builder.HasOne(x => x.Filme)
                .WithMany(x => x.Atores)
                .HasForeignKey(x => x.FilmeId);
        }
    }
}
