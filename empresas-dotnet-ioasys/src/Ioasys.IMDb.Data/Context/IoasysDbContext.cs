using Ioasys.IMDb.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ioasys.IMDb.Data.Context
{
    public class IoasysDbContext : IdentityDbContext
    {
        public DbSet<Filme> Filme { get; set; }
        public DbSet<Ator> Ator { get; set; }
        public DbSet<Diretor> Diretor { get; set; }
        public DbSet<Voto> Voto { get; set; }

        public DbSet<FilmeAtor> FilmeAtor { get; set; }


        public IoasysDbContext(DbContextOptions<IoasysDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(IoasysDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
