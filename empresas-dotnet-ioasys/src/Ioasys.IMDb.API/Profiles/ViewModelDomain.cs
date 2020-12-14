using AutoMapper;
using Ioasys.IMDb.API.ViewModels;
using Ioasys.IMDb.Domain.Entities;
using System;
using System.Linq;

namespace Ioasys.IMDb.API.Profiles
{
    public class ViewModelDomain : Profile
    {
        public ViewModelDomain()
        {
            CreateMap<CadastrarFilmeViewModel, Filme>()
                .ForMember(x => x.Atores, y =>
                {
                    y.MapFrom(x => x.Atores.Select(a => new FilmeAtor
                    {
                        AtorId = Guid.Empty,
                        Ator = new Ator
                        {
                            Id = Guid.Empty,
                            Nome = a
                        }
                    })); ;
                })
                .ForMember(x => x.Diretor, y =>
                {
                    y.MapFrom(x => new Diretor { Id = Guid.Empty, Nome = x.Diretor });
                });
        }
    }
}
