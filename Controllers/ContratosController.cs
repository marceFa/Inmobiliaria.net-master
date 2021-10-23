using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Inmobiliaria.Controllers
{
    [Authorize]
    public class ContratosController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioInmueble repositorioInmuebles;
        private readonly RepositorioInquilino repositorioInquilinos;
        private readonly RepositorioContrato repositorioContratos;

        public ContratosController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repositorioInmuebles = new RepositorioInmueble(configuration);
            repositorioInquilinos = new RepositorioInquilino(configuration);
            repositorioContratos = new RepositorioContrato(configuration);
        }
        // GET: Contratos
        [Authorize(Policy = "Permitidos")]
        public ActionResult Index()
        {
            var lista = repositorioContratos.ObtenerTodos();
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            return View(lista);
        }

        // GET: Contratos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contratos/Create
        [Authorize(Policy = "Permitidos")]
        public ActionResult Create(int id)
        {
            ViewBag.Inmuebles = repositorioInmuebles.ObtenerPorId(id);
            ViewBag.Inquilinos = repositorioInquilinos.ObtenerTodos();
            return View();
        }

        // POST: Contratos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Permitidos")]
        public ActionResult Create(Contrato c)
        {
            try
            {
                // TODO: Add insert logic here
                int res = repositorioContratos.Alta(c);
                Inmueble inm = repositorioInmuebles.ObtenerPorId(c.idInmueble);
                repositorioContratos.Vigente(c);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Inquilinos = repositorioInquilinos.ObtenerTodos();
                ViewBag.Error = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult Ver(int id)
        {
            try
            {
                ViewBag.Id = id;
                var lista = repositorioContratos.ObtenerTodosPorInm(id);
                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();

            }
        }

        // GET: Contratos/Edit/5
        [Authorize(Policy = "Permitidos")]
        public ActionResult Edit(int id)
        {
            var i = repositorioContratos.ObtenerPorId(id);
            ViewBag.Inquilinos = repositorioInquilinos.ObtenerTodos();
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(i);
        }

        // POST: Contratos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Permitidos")]
        public ActionResult Edit(int id, Contrato c)
        {
            try
            {
                c.idContrato = id;
                repositorioContratos.Modificacion(c);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Inquilinos = repositorioInquilinos.ObtenerTodos();
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(c);
            }
        }

        // GET: Contratos/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            var c = repositorioContratos.ObtenerPorId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(c);
        }

        // POST: Contratos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Contrato c)
        {
            try
            {
                repositorioContratos.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(c);
            }
        }
        public ActionResult Filtrados()
        {
            return View();
        }
        public ActionResult Busqueda()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Busqueda(PorFechaView busqueda)
        {
            try
            {

                ViewBag.Filtrados = repositorioContratos.VigentesPorFecha(busqueda.FechaBusqueda);
                if (ViewBag.Filtrados.Count == 0)
                {
                    ModelState.AddModelError("", "No se encontraron resultados");
                    return View();
                }
                return View("Filtrados");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
