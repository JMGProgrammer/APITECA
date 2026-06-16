namespace Actividad_BibliotecaAPI.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public int Paginas { get; set; }
        public bool Leido { get; set; }
    }
}
