using AutoMapper;
using Ioasys.IMDb.API.ViewModels;
using Ioasys.IMDb.Domain.Entities;
using System.Linq;

namespace Ioasys.IMDb.API.Profiles
{
    public class DomainViewModel : Profile
    {
        public DomainViewModel()
        {
            CreateMap<Diretor, DiretorViewModel>();
            CreateMap<Filme, FilmeViewModel>()
                .ForMember(x => x.Atores, s =>
                {
                    s.MapFrom(y => y.Atores.Select(a => new AtorViewModel { Id = a.Ator.Id, Nome = a.Ator.Nome }));
                })
                .ForMember(x => x.MediaVotos, s =>
                {
                    s.MapFrom(y => y.CalcularMediaVotos());
                });
        }
    }
}
