using AutoMapper;
using Ioasys.IMDb.API.ViewModels;
using Ioasys.IMDb.Domain.Entities;
using Ioasys.IMDb.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ioasys.IMDb.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeService _filmeService;
        private readonly IMapper _mapper;

        public FilmesController(IFilmeService filmeService, IMapper mapper)
        {
            _filmeService = filmeService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_mapper.Map<ICollection<FilmeViewModel>>(_filmeService.BuscarTodosFilmes())
                .OrderByDescending(x => x.MediaVotos)
                .ThenBy(x => x.Nome));
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public ActionResult Get(Guid id)
        {
            return Ok(_mapper.Map<FilmeViewModel>(_filmeService.BuscarFilme(id)));
        }

        [HttpPost]
        public ActionResult Post(CadastrarFilmeViewModel vm)
        {
            return Ok(_mapper.Map<FilmeViewModel>(
                _filmeService.RegistrarFilme(_mapper.Map<Filme>(vm))));
        }

        [HttpPut("{id:guid}")]
        public ActionResult Put(Guid id, CadastrarFilmeViewModel vm)
        {
            var filme = _mapper.Map<Filme>(vm);
            filme.Id = id;

            return Ok(_mapper.Map<FilmeViewModel>(_filmeService.Atualizar(filme)));
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            _filmeService.Deletar(id);

            return Ok();
        }

        [Authorize(Roles = "Usuario")]
        [HttpPost("votar/{id:guid}/{nota:int}")]
        public ActionResult Votar(Guid id, int nota)
        {
            try
            {
                _filmeService.AvaliarFilme(id, nota);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }
    }
}