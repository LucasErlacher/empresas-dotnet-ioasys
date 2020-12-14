using Microsoft.AspNetCore.Identity;
using System;

namespace Ioasys.IMDb.Domain.Entities
{
    public class Voto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Guid FilmeId { get; set; }
        public int Nota { get; set; }

        //EF       
        public virtual IdentityUser User { get; set; }
        public virtual Filme Filme { get; set; }
    }
}