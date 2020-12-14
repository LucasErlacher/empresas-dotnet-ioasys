using Ioasys.IMDb.Domain.Entities;
using Ioasys.IMDb.Domain.Interfaces.Repositories;
using Ioasys.IMDb.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ioasys.IMDb.Domain.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IFilmeRepository _filmeRepository;

        public FilmeService(IHttpContextAccessor httpContext, IFilmeRepository filmeRepository)
        {
            _httpContext = httpContext;
            _filmeRepository = filmeRepository;
        }

        public Filme Atualizar(Filme filme)
        {
            return _filmeRepository.Atualizar(filme);
        }

        public void AvaliarFilme(Guid filmeId, int nota)
        {
            var novoVoto = new Voto();

            novoVoto.FilmeId = filmeId;
            novoVoto.Nota = nota;
            novoVoto.UserId =
                _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (novoVoto.Nota < 0 || novoVoto.Nota > 4)
                throw new ArgumentOutOfRangeException();

            _filmeRepository.RegistrarVoto(novoVoto);
        }

        public Filme BuscarFilme(Guid id)
        {
            return _filmeRepository.GetById(id);
        }

        public ICollection<Filme> BuscarTodosFilmes()
        {
            return _filmeRepository.GetAll();
        }

        public void Deletar(Guid filmeId)
        {
            _filmeRepository.Deletar(filmeId);
        }

        public Filme RegistrarFilme(Filme filme)
        {
            var result = _filmeRepository.RegistrarFilme(filme);

            return result;
        }
    }
}
