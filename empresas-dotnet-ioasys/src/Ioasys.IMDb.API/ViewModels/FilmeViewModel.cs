using System;

namespace Ioasys.IMDb.API.ViewModels
{
    public class FilmeViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public DiretorViewModel Diretor { get; set; }
        public AtorViewModel[] Atores { get; set; }
        public double MediaVotos { get; set; }
    }

    public class AtorViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }

    public class DiretorViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }

    public class CadastrarFilmeViewModel
    {
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public string Genero { get; set; }
        public string[] Atores { get; set; }
    }
}
