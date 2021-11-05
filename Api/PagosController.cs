using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inmobiliaria.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly DataContext contexto;

        public PagoController(DataContext contexto)
        {
            this.contexto = contexto;
        }
        // GET: api/<PagosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            try
            {
                var pago = await contexto.Pagos
                .Include(pagos => pagos.Contratos)
                .Include(pagos => pagos.Contratos.Inmuebles)
                .Where(pagos => pagos.Contratos.Inmuebles.Propietarios.Email == User.Identity.Name && pagos.Contratos.FechaInicio <= DateTime.Now && pagos.Contratos.FechaFin >= DateTime.Now)
                .ToListAsync();
                if (pago == null)
                {
                    return NotFound("No se registraron pagos");
                }

                return Ok(pago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<PagoController>
        [HttpGet("Inmueble/{id}")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetPagosPorInmueble(int id)
        {
            try
            {
                    var pago = await contexto.Pagos
                    .Include(pagos => pagos.Contratos)
                    .Include(pagos => pagos.Contratos.Inmuebles)
                    .Include(pagos => pagos.Contratos.Inmuebles.Propietarios)
                    .Include(pagos => pagos.Contratos.Inquilinos)
                    .Where(pagos => pagos.Contratos.idInmueble == id && pagos.Contratos.Inmuebles.Propietarios.Email == User.Identity.Name && pagos.Contratos.FechaInicio <= DateTime.Now && pagos.Contratos.FechaFin >= DateTime.Now)
                    .ToListAsync();

                    if(pago == null)
                    { 
                    return NotFound("No se registraron pagos");


                    }
                    return Ok(pago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<PagoController>
        [HttpPost]
        public async Task<ActionResult<Pago>>PostPago (Pago pagos)
        {
            contexto.Pagos.Add(pagos);
            await contexto.SaveChangesAsync();

            return CreatedAtAction("GetPago", new { id = pagos.idPago }, pagos);
        }

        // PUT api/<PagoController>/4
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id,  Pago pago)
        {
            if (id != pago.idPago)
            {
                return BadRequest();
            }

            contexto.Entry(pago).State = EntityState.Modified;

            try
            {
                await contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<PagoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pago>> DeletePago(int id)
        {
            var pago = await contexto.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            contexto.Pagos.Remove(pago);
            await contexto.SaveChangesAsync();

            return pago;
        }

        private bool PagoExists(int id)
        {
            return contexto.Pagos.Any(e => e.idPago == id);
        }
    }
}
