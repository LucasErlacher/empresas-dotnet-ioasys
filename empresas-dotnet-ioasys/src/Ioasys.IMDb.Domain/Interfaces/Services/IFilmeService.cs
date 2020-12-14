using Ioasys.IMDb.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Ioasys.IMDb.Domain.Interfaces.Services
{
    public interface IFilmeService
    {
        Filme RegistrarFilme(Filme filme);
        void AvaliarFilme(Guid filmeId, int nota);
        Filme BuscarFilme(Guid id);
        ICollection<Filme> BuscarTodosFilmes();
        void Deletar(Guid filmeId);
        Filme Atualizar(Filme filme);
    }
}
