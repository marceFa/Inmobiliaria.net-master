﻿using System;using System.Collections.Generic;using System.Linq;using System.Net;using System.Threading.Tasks;using Inmobiliaria.Models;using Microsoft.AspNetCore.Authentication.JwtBearer;using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Http;using Microsoft.AspNetCore.Server.HttpSys;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;namespace Inmobiliaria.Api{    [Route("api/[controller]")]    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]    [ApiController]    public class ContratosController : ControllerBase    {        private readonly DataContext contexto;        public ContratosController(DataContext contexto)        {            this.contexto = contexto;        }


        // GET: api/<ContratosController>
        [HttpGet("inmueble/{id}")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetTodosPorInmueble(int id)
        {
            try
            {
                var contrato = await contexto.Contratos
                .Include(cont => cont.Inmuebles)
                .Include(cont => cont.Inquilinos)
                .Include(cont => cont.Inmuebles.Propietarios)
                .Where(cont => cont.idInmueble == id && cont.Inmuebles.Propietarios.Email == User.Identity.Name)
                .FirstOrDefaultAsync();
                if (contrato.FechaFin < DateTime.Now)
                {
                    contrato.Vigente = false;
                }
                else if (contrato == null || contrato.Inmuebles.Propietarios.Email != User.Identity.Name)
                {
                    return NotFound("No existen contratos vigentes");
                }

                return Ok(contrato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("vigente/{id}")]
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
        [HttpGet("Vigentes")]        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratosVigentes()        {            try            {                var usuario = User.Identity.Name;                var contrato = contexto.Contratos                .Include(cont => cont.Inmuebles)                .Include(cont => cont.Inquilinos)                .Include(cont => cont.Inmuebles.Propietarios)                .Where(cont => cont.FechaInicio <= DateTime.Now && cont.FechaFin >= DateTime.Now && cont.Inmuebles.Propietarios.Email == usuario);
                
                if (contrato == null)                {                    return NotFound("No existen contratos vigentes");                }                return Ok(contrato);            }            catch (Exception ex)            {                return BadRequest(ex);            }        }

        // POST api/<ContratosController>
        [HttpPost("{id}")]        public async Task<ActionResult<Contrato>>Get(int id)        {            var contrato = await contexto.Contratos.FindAsync(id);            if (contrato == null)            {                return NotFound();            }            return contrato;        }                // PUT api/<ContratosController>/1        [HttpPut("{id}")]        public async Task<IActionResult> Put(int id, Contrato contrato)        {            if (id != contrato.idContrato)
                {
                    return BadRequest();
                }

                contexto.Entry(contrato).State = EntityState.Modified;

                try
                {
                    await contexto.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (contexto.Contratos.FirstOrDefault(e => e.idContrato == id) == null)
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
            [HttpPost]
            public async Task<ActionResult<Contrato>> Post(Contrato contrato)
            {
                contexto.Contratos.Add(contrato);
                await contexto.SaveChangesAsync();

                return CreatedAtAction("GetContrato", new { id = contrato.idContrato }, contrato);
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<Contrato>> Delete(int id)
            {
                var contrato = await contexto.Contratos.FindAsync(id);
                if (contrato == null)
                {
                    return NotFound();
                }

                contexto.Contratos.Remove(contrato);
                await contexto.SaveChangesAsync();

                return contrato;
            }


        }
    }