using Microsoft.AspNetCore.Mvc;
using Softecnologia.Models;
using Softecnologia.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;


namespace Softecnologia.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.ToListAsync());
        }

        //Get Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Documento, Nombre, Apellidos, TipoDocumento, Contraseña, FechaNacimineto, Email, Direccion, Telefono, Celular, MunicipioRes, Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
               // TempData["mensaje"] = "Registro exitoso";
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

         //GET: Usuario/Editar
        public async Task<IActionResult> Edit(int? documento)
        {
            if (documento == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(documento);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        //POST: eSTUDIANTE//Edit
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int documento, [Bind("Documento, Nombre, Apellidos, TipoDocumento, Contraseña, FechaNacimineto, Email, Direccion, Telefono, Celular, MunicipioRes, Rol")] Usuario usuario)
        {
            if (documento != usuario.Documento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Documento))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        private bool UsuarioExists(int documento)
        {
            return _context.Usuario.Any(e => e.Documento == documento);
        }
    }
}

            