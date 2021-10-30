using System;using System.Collections.Generic;using System.Linq;using System.Net;using System.Threading.Tasks;using Inmobiliaria.Models;using Microsoft.AspNetCore.Authentication.JwtBearer;using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Http;using Microsoft.AspNetCore.Server.HttpSys;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860namespace Inmobiliaria.Api{    [Route("api/[controller]")]    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]    [ApiController]    public class ContratosController : ControllerBase    {        private readonly DataContext contexto;        public ContratosController(DataContext contexto)        {            this.contexto = contexto;        }


        // GET: api/<ContratosController>
        [HttpGet("inmueblesConContrato")]        public async Task<ActionResult> InmueblesConContrato()        {            try            {                var usuario = User.Identity.Name;                var contratosVigentes = contexto.Contratos              .Include(x => x.Inquilinos)              .Include(x => x.Inmuebles)              .Where(c => c.Inmuebles.Propietarios.Email == usuario).ToList();
                // .ThenInclude(x => x.Propietarios)


                return Ok(contratosVigentes);            }            catch (Exception ex)            {                return BadRequest(ex);            }        }
        // GET: api/<ContratosController/obtenerPorId>
        [HttpGet("obtenerId/{id}")]        public async Task<IActionResult> obtenerId(int id)        {            try            {                var usuario = User.Identity.Name;                var res = contexto.Contratos                    .Include(e => e.Inquilinos)                    .Include(e => e.Inmuebles)                    .Where(e => e.Inmuebles.Propietarios.Email == usuario)                    .Single(e => e.idInmueble == id);                return Ok(res);            }            catch (Exception ex)            {                return BadRequest(ex);            }        }                       [HttpGet("vigente/{id}")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratoVigentePorInmueble(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                var contrato = contexto.Contratos
                .Include(cont => cont.Inmuebles)
                .Include(cont => cont.Inquilinos)
                .Include(cont => cont.Inmuebles.Propietarios)
                .Where(cont => cont.idInmueble == id && cont.FechaInicio <= DateTime.Now && cont.FechaFin >= DateTime.Now && cont.Inmuebles.Propietarios.Email == usuario);
                //.FirstOrDefaultAsync();
                

                return Ok(contrato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        // GET: api/Contratos/
        [HttpGet("Vigentes")]        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratoVigente()        {            try            {                var usuario = User.Identity.Name;                var contrato = contexto.Contratos                .Include(cont => cont.Inmuebles)                .Include(cont => cont.Inquilinos)                .Include(cont => cont.Inmuebles.Propietarios)                .Where(cont => cont.FechaInicio <= DateTime.Now && cont.FechaFin >= DateTime.Now && cont.Inmuebles.Propietarios.Email == usuario);
                //.FirstOrDefaultAsync();
                if (contrato == null)                {                    return NotFound("No existen contratos vigentes");                }                return Ok(contrato);            }            catch (Exception ex)            {                return BadRequest(ex);            }        }




        // POST api/<ContratosController>
        [HttpPost]        public async Task<ActionResult<Contrato>>Get(int id)        {            var contrato = await contexto.Contratos.FindAsync(id);            if (contrato == null)            {                return NotFound();            }            return contrato;        }                // PUT api/<ContratosController>/1        [HttpPut("{id}")]        public async Task<IActionResult> Put(int id, Contrato contratos)        {            try            {                if (ModelState.IsValid && contexto.Contratos.AsNoTracking().Include(e => e.Inmuebles).ThenInclude(x => x.Propietarios).FirstOrDefault(e => e.idInmueble == id && e.Inmuebles.Propietarios.Email == User.Identity.Name) != null)                {                    contratos.idContrato = id;                    contexto.Contratos.Update(contratos);                    contexto.SaveChanges();                    return Ok(contratos);                }                return BadRequest();            }            catch (Exception ex)            {                return BadRequest(ex);            }        }        // DELETE api/<ContratoController>/5        [HttpDelete("{id}")]        public async Task<IActionResult> Delete(int id)        {            var contratos = await contexto.Contratos.FindAsync(id);            if (contratos == null)            {                return NotFound();            }            contexto.Contratos.Remove(contratos);            await contexto.SaveChangesAsync();            return (IActionResult)contratos;        }                    }}