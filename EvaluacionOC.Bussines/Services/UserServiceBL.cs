using EvaluacionOC.Model.Interfaces;
using EvaluacionOC.Model.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionOC.Bussines.Services
{
    public class UserServiceBL : IUserServiceBL
    {
        private bool succes;
        private string error;
        private User usuarioSesion;

        public bool Succes => succes;

        public string Error => error;
        public User UsuarioSesion => usuarioSesion;

        public UserServiceBL(IUserService userService, ISeguridad seguridad)
        {
            UserService = userService;
            Seguridad = seguridad;
        }

        public IUserService UserService { get; }
        public ISeguridad Seguridad { get; }



        public async Task<ResultLogin> Login(LoginInfo login)
        {
            login.Password = Seguridad.Cifrar(login.Password);
            var result = await UserService.Login(login);
            ResultLogin resultLogin = new ResultLogin() { Token = result };
            succes = true;
            if (UserService.Error != null)
            {
                succes = false;
                error = UserService.Error.Message;
                resultLogin.Token = string.Empty;
                return resultLogin;
            }
            if (string.IsNullOrEmpty(result))
            {
                succes = false;
                error = "Usuario o contraseña incorrecta";
                resultLogin.Token = string.Empty;
                return resultLogin;
            }
            return resultLogin;

        }
        public async Task<bool> VerificaNombre(string nombre)
        {
            bool existe = false;
            var result = await UserService.VerificaNombre(nombre);
            succes = true;
            if (result.Count() > 0)
            {
                existe = true;
                succes = false;
                error = "Nombre de usuario ya registrado";
            }
            if (UserService.Error != null)
            {
                error = UserService.Error.Message;
                succes = false;
            }
            return existe;
        }
        public async Task<bool> VerificaEmail(string email)
        {
            bool existe = false;
            var result = await UserService.VerificaEmail(email);
            succes = true;
            if (result.Count() > 0)
            {
                existe = true;
                succes = false;
                error = "Email ya registrado";
            }
            if (UserService.Error != null)
            {
                error = UserService.Error.Message;
                succes = false;
            }
            return existe;
        }

        public async Task<IEnumerable<User>> ConsultaUsuarios()
        {

            var result = await UserService.ConsultaUsuarios();
            foreach(var r in result)
            {
                r.Password = "";
            }            
            succes = true;
            if (UserService.Error != null)
            {
                error = UserService.Error.Message;
                succes = false;
            }

            return result;
        }

        public async Task CrearUsuario(User usuario)
        {
            if (await VerificaNombre(usuario.NombreUsuario))
            {
                succes = false;
                error = "El usuario ya esta registrado";
                return;
            }
            if (await VerificaEmail(usuario.Email))
            {
                succes = false;
                error = "El email ya esta registrado";
                return;
            }
            usuario.Password = Seguridad.Cifrar(usuario.Password);
            usuario.Status = true;
            var jsonUsuario = JsonConvert.SerializeObject(usuario);
            var id = await UserService.CrearUsuario(jsonUsuario);
            usuario.Password = string.Empty;
            succes = true;
            if (UserService.Error != null)
            {
                error = UserService.Error.Message;
                succes = false;
            }
            else
            {
                usuario.Id = id;
            }
        }

        public async Task EliminarUsuario(User usuario)
        {

            usuario.Status = false;
            await UserService.EliminarUsuario(usuario);
            succes = true;
            if (UserService.Error != null)
            {
                error = UserService.Error.Message;
                succes = false;
            }
        }

        public async Task CambiarPassword(User usuario)
        {
            usuario.Id = UsuarioSesion.Id;
            usuario.Password = Seguridad.Cifrar(usuario.Password);
            await UserService.CambiarPassword(usuario);
            succes = true;
            if (UserService.Error != null)
            {
                succes = false;
                error = UserService.Error.Message;
                return;
            }

        }

        public async Task ModificarUsuario(User usuario)
        {
            var usu = await UserService.ConsultaUsuario(usuario.Id);
            if (UserService.Error != null)
            {
                error = UserService.Error.Message;
                succes = false;
                return;
            }
            if (usu != null)
            {
                if (usu.NombreUsuario != usuario.NombreUsuario)
                {
                    var existe = await VerificaNombre(usuario.NombreUsuario);
                    if (existe)
                    {
                        succes = false;
                        error = "El nombre de usuario ya existe";
                        return;
                    }
                }
                if (usu.Email != usuario.Email)
                {
                    var existe = await VerificaEmail(usuario.Email);
                    if (existe)
                    {
                        succes = false;
                        error = "El correo ya esiste";
                        return;
                    }
                }
            }
            await UserService.ModificarUsuario(usuario);
            succes = true;
            if (UserService.Error != null)
            {
                error = UserService.Error.Message;
                succes = false;
            }
        }

        public void SetUserSesion(string jsonUser)
        {
            usuarioSesion = JsonConvert.DeserializeObject<User>(jsonUser);
        }
    }
}
