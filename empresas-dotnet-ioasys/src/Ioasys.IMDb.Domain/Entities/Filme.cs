using System;
using System.Collections.Generic;

namespace Ioasys.IMDb.Domain.Entities
{
    public class Filme
    {
        public Guid Id { get; set; }
        public Guid DiretorId { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }

        //EF
        public virtual Diretor Diretor { get; set; }
        public virtual ICollection<FilmeAtor> Atores { get; set; }
        public virtual ICollection<Voto> Votos { get; set; }

        public double CalcularMediaVotos()
        {
            double total = 0;

            foreach (var voto in Votos)
            {
                total += voto.Nota;
            }

            return Votos.Count > 0 ? total / Votos.Count : 0;
        }
    }
}
