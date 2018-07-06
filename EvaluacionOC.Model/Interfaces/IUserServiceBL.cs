using EvaluacionOC.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionOC.Model.Interfaces
{
    public interface IUserServiceBL
    {
        bool Succes { get; }
        string Error { get; }
        User UsuarioSesion { get;}
        Task CrearUsuario(User usuairo);
        Task ModificarUsuario(User usuario);
        Task<IEnumerable<User>> ConsultaUsuarios();
        Task EliminarUsuario(User usuario);
        Task<ResultLogin> Login(LoginInfo login);
        Task<bool> VerificaNombre(string nombreUsuario);
        Task<bool> VerificaEmail(string email);
        void SetUserSesion(string jsonUser);
        Task CambiarPassword(User usuario);
    }
}
