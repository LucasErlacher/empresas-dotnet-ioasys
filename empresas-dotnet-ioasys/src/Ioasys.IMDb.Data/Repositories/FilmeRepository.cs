using Ioasys.IMDb.Data.Context;
using Ioasys.IMDb.Domain.Entities;
using Ioasys.IMDb.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ioasys.IMDb.Data.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        IoasysDbContext _dbContext;

        public FilmeRepository(IoasysDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Filme Atualizar(Filme filme)
        {
            var proxyFilme = _dbContext.Filme.Find(filme.Id);

            proxyFilme.Genero = filme.Genero;
            proxyFilme.Nome = filme.Nome;

            var existeDiretor = _dbContext.Diretor.Any(x => x.Nome == filme.Diretor.Nome);

            if (!existeDiretor)
            {
                _dbContext.Diretor.Add(filme.Diretor);

                proxyFilme.DiretorId = filme.DiretorId;
                proxyFilme.Diretor = filme.Diretor;
            }
            else
            {
                var proxyDiretor = _dbContext.Diretor.SingleOrDefault(x => x.Nome == filme.Diretor.Nome);

                filme.DiretorId = proxyDiretor.Id;
                filme.Diretor = proxyDiretor;
            }


            var filmesAtores = filme.Atores;
            var proxyFilmesAtores = proxyFilme.Atores;

            foreach (var rel in filmesAtores)
            {
                var existeAtor = proxyFilmesAtores.Any(x => x.Ator.Nome == rel.Ator.Nome);

                if (!existeAtor)
                {
                    var ator = _dbContext.Ator.SingleOrDefault(x => x.Nome == rel.Ator.Nome);

                    if (ator == null)
                    {
                        _dbContext.Ator.Add(rel.Ator);
                    }
                    else
                    {
                        rel.AtorId = ator.Id;
                        rel.Ator = ator;
                    }

                    _dbContext.FilmeAtor.Add(rel);
                }
            }

            //foreach (var rel in proxyFilmesAtores)
            //{
            //    var existeAtor = proxyFilmesAtores.Any(x => x.Ator.Nome == rel.Ator.Nome);

            //    if (!existeAtor)
            //    {
            //        var ator = _dbContext.Ator.SingleOrDefault(x => x.Nome == rel.Ator.Nome);

            //        if (ator == null)
            //        {
            //            _dbContext.Ator.Add(rel.Ator);
            //        }
            //        else
            //        {
            //            rel.AtorId = ator.Id;
            //            rel.Ator = ator;
            //        }
            //    }

            //    _dbContext.FilmeAtor.Add(rel);
            //}

            _dbContext.SaveChanges();

            return proxyFilme;
        }

        public void Deletar(Guid filmeId)
        {
            var filme = _dbContext.Filme.Find(filmeId);

            _dbContext.Filme.Remove(filme);

            _dbContext.SaveChanges();
        }

        public ICollection<Filme> GetAll()
        {
            var filmes = _dbContext.Filme.Include(x => x.Atores).ThenInclude(x => x.Ator)
                 .Include(x => x.Diretor)
                 .Include(x => x.Votos).AsQueryable();

            return filmes.ToList();
        }

        public ICollection<Filme> GetAll(ICollection<Expression<Func<Filme, bool>>> expressions)
        {
            var filmes = _dbContext.Filme.Include(x => x.Atores).ThenInclude(x => x.Ator)
                 .Include(x => x.Diretor)
                 .Include(x => x.Votos).AsQueryable();

            if (expressions != null && expressions.Count > 0)
            {
                foreach (var condition in expressions)
                {
                    filmes = filmes.Where(condition);
                }
            }

            return filmes.ToList();
        }

        public Filme GetById(System.Guid id)
        {
            var result = _dbContext.Filme.Include(x => x.Atores).ThenInclude(x => x.Ator)
                .Include(x => x.Diretor)
                .Include(x => x.Votos).FirstOrDefault(x => x.Id == id);
            return result;
        }

        public Filme RegistrarFilme(Filme filme)
        {
            var filmesAtores = filme.Atores;
            var diretor = filme.Diretor;

            var existeDiretor = _dbContext.Diretor.Any(x => x.Nome == diretor.Nome);

            if (!existeDiretor)
            {
                _dbContext.Diretor.Add(diretor);
            }
            else
            {
                var proxyDiretor = _dbContext.Diretor.SingleOrDefault(x => x.Nome == diretor.Nome);

                filme.DiretorId = proxyDiretor.Id;
                filme.Diretor = proxyDiretor;
            }

            foreach (var rel in filmesAtores)
            {
                var existeAtor = _dbContext.Ator.Any(x => x.Nome == rel.Ator.Nome);

                if (!existeAtor)
                {
                    _dbContext.Ator.Add(rel.Ator);
                }
                else
                {
                    var proxyAtor = _dbContext.Ator.SingleOrDefault(x => x.Nome == rel.Ator.Nome);

                    rel.AtorId = proxyAtor.Id;
                    rel.Ator = proxyAtor;
                }

                _dbContext.FilmeAtor.Add(rel);
            }

            _dbContext.Filme.Add(filme);

            _dbContext.SaveChanges();

            return _dbContext.Filme.SingleOrDefault(x => x.Nome == filme.Nome);
        }

        public void RegistrarVoto(Voto novoVoto)
        {
            _dbContext.Voto.Add(novoVoto);
            _dbContext.SaveChanges();
        }
    }
}
