using Ioasys.IMDb.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Ioasys.IMDb.Domain.Interfaces.Repositories
{
    public interface IFilmeRepository
    {
        Filme RegistrarFilme(Filme filme);
        ICollection<Filme> GetAll();
        Filme GetById(Guid id);
        void RegistrarVoto(Voto novoVoto);
        void Deletar(Guid filmeId);
        Filme Atualizar(Filme filme);
    }
}
