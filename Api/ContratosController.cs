﻿using System;


        // GET: api/<ContratosController>
        [HttpGet("inmueblesConContrato")]
                // .ThenInclude(x => x.Propietarios)


                return Ok(contratosVigentes);
        // GET: api/<ContratosController/obtenerPorId>
        [HttpGet("obtenerId/{id}")]
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
        [HttpGet("Vigentes")]
                //.FirstOrDefaultAsync();
                if (contrato == null)




        // POST api/<ContratosController>
        [HttpPost]