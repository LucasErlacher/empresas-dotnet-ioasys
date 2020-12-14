using System;
using System.Text.Json.Serialization;

namespace Ioasys.IMDb.Domain.Entities
{
    public class FilmeAtor
    {
        public int Id { get; set; }
        public Guid FilmeId { get; set; }
        public Guid AtorId { get; set; }

        //EF
        [JsonIgnore]
        public virtual Filme Filme { get; set; }
        public virtual Ator Ator { get; set; }
    }
}
