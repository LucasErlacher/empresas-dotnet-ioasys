using Ioasys.IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ioasys.IMDb.Data.Configurations
{
    public class FilmeConfiguration : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Diretor)
                .WithMany()
                .HasForeignKey(x => x.DiretorId);
        }
    }
}
