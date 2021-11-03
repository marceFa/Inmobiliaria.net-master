using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inmobiliaria.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class InmueblesController : Controller
    {
        private readonly DataContext contexto;

        public InmueblesController(DataContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/<InmueblesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuarios = User.Identity.Name;
                return Ok(contexto.Inmuebles.Include(e => e.Propietarios).Where(e => e.Propietarios.Email == usuarios));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/<InmueblesController/obtenerPorId>
        [HttpGet("obtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {

            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Inmuebles
                    .Include(e => e.Propietarios)
                    .Where(e => e.Propietarios.Email == usuario).Single(e => e.idInmueble == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        //Me sirve para modificar todos los campos
        [HttpPut("modificarDisponible/{id}")]
        public async Task<IActionResult> ModificarDisponible(int id, [FromForm] Inmueble inmueble)
        {
            var inm = contexto.Inmuebles.AsNoTracking().Include(e => e.Propietarios).FirstOrDefault(e => e.idInmueble == id && e.Propietarios.Email == User.Identity.Name);
           
            try
            {
                if (ModelState.IsValid && inm != null)
                {
                    inmueble.idInmueble = inm.idInmueble;
                    inmueble.id = inm.id;
                    inmueble.Direccion = inm.Direccion;
                    inmueble.Tipo = inm.Tipo;
                    inmueble.Uso = inm.Uso;
                    inmueble.Ambientes = inm.Ambientes;
                    inmueble.Precio = inm.Precio;
                    if (inm.Disponible)
                    {
                        inmueble.Disponible = false;
                    }
                    else
                    {
                        inmueble.Disponible = true;
                    }
                    contexto.Entry(inmueble).State = EntityState.Modified;
                    await contexto.SaveChangesAsync();
                    return Ok(inmueble);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(Inmueble entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entidad.id = contexto.Propietarios.Single(e => e.Email == User.Identity.Name).id;
                    contexto.Inmuebles.Add(entidad);
                    contexto.SaveChanges();
                    return CreatedAtAction(nameof(Get), new { id = entidad.idInmueble }, entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<InmublesController>/11
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Inmueble entidad)
        {
            try
            {
                if (ModelState.IsValid && contexto.Inmuebles.AsNoTracking().Include(e => e.Propietarios).FirstOrDefault(e => e.id == id && e.Propietarios.Email == User.Identity.Name) != null)
                {
                    entidad.id = id;
                    contexto.Inmuebles.Update(entidad);
                    contexto.SaveChanges();
                    return Ok(entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<controller>/12
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entidad = contexto.Inmuebles.Include(e => e.Propietarios).FirstOrDefault(e => e.id == id && e.Propietarios.Email == User.Identity.Name);
                if (entidad != null)
                {
                    contexto.Inmuebles.Remove(entidad);
                    contexto.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("BajaLogica/{id}")]
        public async Task<IActionResult> BajaLogica(int id)
        {
            try
            {
                var entidad = contexto.Inmuebles.Include(e => e.Propietarios).FirstOrDefault(e => e.id == id && e.Propietarios.Email == User.Identity.Name);
                if (entidad != null)
                {
                    entidad.Ambientes = -1;//cambiar por estado = 0
                    contexto.Inmuebles.Update(entidad);
                    contexto.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}