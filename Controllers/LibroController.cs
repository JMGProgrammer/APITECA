using Actividad_BibliotecaAPI.Data;
using Actividad_BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Actividad_BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibroController(AppDbContext context)
        {
            _context = context;
        }

        //Ejercicio 1: Buscar todos los libros
        [HttpGet]
        public async Task<IActionResult> GetLibros()
        {
            var libros = await _context.Libros.ToListAsync();
            return Ok(libros);
        }

        //Ejercicio 2: Buscar libro por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLibroXId(int id)
        {
            var libro = await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            if (libro == null)
            {
                return NotFound("Libro no encontrado");
            }
            return Ok(libro);
        }

        //Ejercicio 6: Calcular si un libro es corto, mediano o largo según la cantidad de páginas
        [HttpGet("{id}/tipo")]
        public async Task<IActionResult> GetTipoLibro(int id)
        {
            var libro = await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            if (libro == null)
            {
                return NotFound("Libro no encontrado");
            }

            string tipo = "";

            if (libro.Paginas < 100)
            {
                tipo = "Libro Corto";
            }
            else if (libro.Paginas >= 100 && libro.Paginas <= 300)
            {
                tipo = "Libro Mediano";
            }
            else
            {
                tipo = "Libro Largo";
            }

            return Ok(new { Tipo = tipo });
        }

        //Ejercico 7: Calcular Fibonacci según la cantidad de páginas del libro
        [HttpGet("{id}/fibonacci")]
        public async Task<IActionResult> GetFibonacci(int id)
        {
            var libro = await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            if (libro == null)
            {
                return NotFound("Libro no encontrado");
            }

            int nroPaginas = libro.Paginas;
            long fibonacci = CalcularFibonacci(nroPaginas);

            return Ok(new { Titulo = libro.Titulo, Paginas = libro.Paginas, Numero = nroPaginas, Fibonacci = fibonacci });
        }

        private long CalcularFibonacci(int n) //Buscado en Google 
        {
            if (n <= 1)
                return n;
            long a = 0, b = 1, temp;
            for (int i = 2; i <= n; i++)
            {
                temp = a + b;
                a = b;
                b = temp;
            }
            return b;
        }

        //Ejercicio EXTRA: Buscar libros por autor
        [HttpGet("autor/{autor}")]
        public async Task<IActionResult> GetLibrosXAutor(string autor)
        {
            var libros = await _context.Libros.Where(l => l.Autor.ToLower() == autor.ToLower()).ToListAsync();
            if (libros.Count == 0)
            {
                return NotFound("No se encontraron libros para el autor especificado");
            }
            return Ok(libros);
        }

        //Ejercicio 10: Resumen de la biblioteca
        [HttpGet("resumen")]
        public async Task<IActionResult> GetResumenBiblioteca()
        {
            var libros = await _context.Libros.ToListAsync();

            int totalLibros = libros.Count;
            int librosLeidos = libros.Count(l => l.Leido);
            int librosNoLeidos = totalLibros - librosLeidos;

            double promedioPaginas = 0;
            if (totalLibros > 0)
            {
                promedioPaginas = libros.Average(l => l.Paginas);
            }
            else
            {
                promedioPaginas = 0;
            }

            return Ok(new
            {
                TotalLibros = totalLibros,
                LibrosLeidos = librosLeidos,
                LibrosNoLeidos = librosNoLeidos,
                PromedioPaginas = promedioPaginas
            });
        }

        //Ejercicio 3: Agregar un nuevo libro
        [HttpPost]
        public async Task<IActionResult> PostLibro([FromBody] Libro libro)
        {
            if (libro == null)
            {
                return BadRequest("Libro no válido");
            }
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return Ok(libro);
        }

        //Ejercicio 8: Agregar un nuevo libro con recomendación según la cantidad de páginas
        [HttpPost("con-recomendacion")]
        public async Task<IActionResult> PostLibroConRecomendacion([FromBody] Libro libro)
        {
            if (libro == null)
            {
                return BadRequest("Libro no válido");
            }

            string recomendacion = "";

            if (libro.Paginas < 100)
            {
                recomendacion = "Ideal para leer en un día";
            }
            else if (libro.Paginas >= 100 && libro.Paginas <= 300)
            {
                recomendacion = "Lectura normal";
            }
            else
            {
                recomendacion = "Lectura larga";
            }

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return Ok(new { Libro = libro, Recomendacion = recomendacion });
        }

        //Ejercicio 4: Modificar un libro
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro(int id, [FromBody] Libro libro)
        {
            var libroExistente = await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            if (libroExistente == null)
            {
                return NotFound("Libro no encontrado");
            }

            libroExistente.Titulo = libro.Titulo;
            libroExistente.Autor = libro.Autor;
            libroExistente.Paginas = libro.Paginas;
            libroExistente.Leido = libro.Leido;

            await _context.SaveChangesAsync();
            return Ok(libroExistente);
        }

        //Ejercicio 9: Marcar un libro como leído
        [HttpPut("{id}/marcar-leido")]
        public async Task<IActionResult> PutMarcarLeido(int id)
        {
            var libroExistente = await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            if (libroExistente == null)
            {
                return NotFound("Libro no encontrado");
            }

            libroExistente.Leido = true;
            await _context.SaveChangesAsync();
            return Ok("El libro ha sido marcado como leído.");
        }

        //Ejercicio 5: Eliminar un libro
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libroExistente = await _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            if (libroExistente == null)
            {
                return NotFound("Libro no encontrado");
            }

            _context.Libros.Remove(libroExistente);
            await _context.SaveChangesAsync();
            return Ok(libroExistente);
        }
    }
}
