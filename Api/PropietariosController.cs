using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inmobiliaria.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PropietariosController : ControllerBase//
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;
        

        public PropietariosController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }
        // GET: api/<PropietariosController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {

                var usuario = User.Identity.Name;
                var res = contexto.Propietarios.FirstOrDefault(x => x.Email == usuario);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/<PropietariosController>/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginView loginView)
        {
            Propietario prop = contexto.Propietarios.FirstOrDefault(x => x.Email == loginView.Email);
            if (prop != null)
            {

                try
                {


                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                       password: loginView.Clave,
                       salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                       prf: KeyDerivationPrf.HMACSHA1,
                       iterationCount: 1000,
                       numBytesRequested: 256 / 8));
                    var us = contexto.Usuarios.FirstOrDefault(x => x.Email == loginView.Email);
                    if (us == null)
                    {
                        return BadRequest("Email y / o Contraseña incorrecta");
                    }
                    else
                    {
                        
                        var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
                        var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, us.Email),
                        new Claim("FullName", us.Nombre + " " + us.Apellido),
                        new Claim(ClaimTypes.Role, "Propietario"),
                    };

                        var token = new JwtSecurityToken(
                            issuer: config["TokenAuthentication:Issuer"],
                            audience: config["TokenAuthentication:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(60),
                            signingCredentials: credenciales
                        );
                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Este Usuario no es un Propietario");

            }
        }




        // POST api/<PropietariosController>/login
        [HttpGet("propietarioActual")]
        //public async Task<IActionResult> PropietarioActual([FromBody] LoginView loginView)
        public async Task<IActionResult> PropietarioActual()
        {
            try
            {
                return Ok(
                    contexto.Propietarios
                    
                    .Select(x => new { x.id, x.Nombre, x.Apellido, x.Dni, x.Email, x.Clave, x.Telefono })
                    .FirstOrDefault(x => x.Email == User.Identity.Name));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        // POST api/<PropietariosController>
        [HttpPost]
        public async Task<IActionResult> Post(Propietario entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await contexto.Propietarios.AddAsync(entidad);
                    contexto.SaveChanges();
                    return CreatedAtAction(nameof(Get), new { id = entidad.id }, entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<PropietariosController>/1
        [HttpPut("editar")]
        public async Task<IActionResult> Put([FromBody] Propietario entidad)
        {
            try
            {
                var usuario = User.Identity.Name;
                var res = contexto.Propietarios.AsNoTracking().FirstOrDefault(x => x.Email == usuario);

                if (ModelState.IsValid && res != null)
                {
                    entidad.id = res.id;
                    entidad.Email = res.Email;
                    contexto.Entry(entidad).State = EntityState.Modified;
                    //contexto.Propietarios.Update(entidad);
                    await contexto.SaveChangesAsync();
                    return Ok(entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<PropietariosController>/borra/4
        [HttpDelete("borra{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var p = contexto.Propietarios.Find(id);
                    if (p == null)
                        return NotFound();
                    contexto.Propietarios.Remove(p);
                    contexto.SaveChanges();
                    return Ok(p);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Propietarios/test
        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult Test()
        {
            try
            {
                return Ok("anduvo");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Propietarios/test/5
        [HttpGet("test/{codigo}")]
        [AllowAnonymous]
        public IActionResult Code(int codigo)
        {
            try
            {
                //StatusCodes.Status418ImATeapot //constantes con códigos
                return StatusCode(codigo, new { Mensaje = "Anduvo", Error = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}