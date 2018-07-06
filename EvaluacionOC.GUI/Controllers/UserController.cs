using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluacionOC.Model.Interfaces;
using EvaluacionOC.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionOC.GUI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserServiceBL UserService { get; }
        public UserController(IUserServiceBL userService)
        {
            UserService = userService;
        }

        [HttpPost]
        [Route("Login")]
        [RequireHttps]
        public async Task<IActionResult> Login([FromBody] LoginInfo login)
        {
            var result = await UserService.Login(login);
            if (UserService.Succes)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(UserService.Error);
            }

        }

        [HttpGet]
        [Authorize]
        [Route("VerificaLoged")]
        public IActionResult VerificaLoged()
        {
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("CambioPassword")]
        [RequireHttps]
        public async Task<IActionResult> CambioPassword([FromBody]UsuarioPassword uPass)
        {            
            if(ModelState.IsValid)
            {
                var jsonUserLogued = User.Claims.Where(c => c.Type == "UserData").FirstOrDefault().Value;
                UserService.SetUserSesion(jsonUserLogued);
                var usuario = new User(uPass);
                await UserService.CambiarPassword(usuario);
                if (UserService.Succes)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(UserService.Error);
                }
            }
            else
            {
                return (BadRequest("Modelo invalido"));
            }
        }

        [HttpGet]
        [Route("VerificaNombre")]
        public async Task<IActionResult> VerificaNombre(string nombre)
        {
            
            var result = await UserService.VerificaNombre(nombre);
            if (UserService.Succes)
            {
                return Ok(result);
            }
            else
            {
                return base.BadRequest(UserService.Error);
            }
        }
        [HttpGet]
        [Route("VerificaEmail")]
        public async Task<IActionResult> VerificaEmail(string email)
        {
            var result = await UserService.VerificaEmail(email);
            if (UserService.Succes)
            {
                return Ok(result);
            }
            else
            {
                return base.BadRequest(UserService.Error);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("Consulta")]
        public async Task<IEnumerable<User>> ConsultarUsuario()
        {

            var result = await UserService.ConsultaUsuarios();
            if (UserService.Succes)
            {
                return result;
            }
            return null;
        }
        [HttpPost]
        [Route("Crea")]        
        [RequireHttps]
        public async Task<IActionResult> CrearUsuario([FromBody] User usuario)
        {
            if (ModelState.IsValid)
            {                
                await UserService.CrearUsuario(usuario);
                if (UserService.Succes)
                {
                    return Ok(usuario);
                }
                return BadRequest(UserService.Error);
            }
            else
            {
                return base.BadRequest("Modelo invalido");
            }
        }
        [HttpPut]
        [Authorize]
        [Route("Modifica")]
        public async Task<IActionResult> ModificarUsuario([FromBody] UsuarioModificar usuarioMod)
        {
            
            if (ModelState.IsValid)
            {
                var usuario = new User(usuarioMod);
                await UserService.ModificarUsuario(usuario);
                if (UserService.Succes)
                {
                    return Ok(usuario);
                }
                return BadRequest(UserService.Error);
            }
            else
            {
                return base.BadRequest("Modelo invalido");
            }
        }
        [HttpPut]
        [Authorize]
        [Route("Elimina")]
        public async Task<IActionResult> EliminarUsuario([FromBody] UsuarioEliminar usuarioEliminar)
        {
            if (ModelState.IsValid)
            {
                var usuario = new User(usuarioEliminar);
                await UserService.EliminarUsuario(usuario);
                if (UserService.Succes)
                {
                    return Ok(usuario);
                }
                return BadRequest(UserService.Error);
            }
            else
            {
                return base.BadRequest("Modelo invalido");
            }
        }

    }
}