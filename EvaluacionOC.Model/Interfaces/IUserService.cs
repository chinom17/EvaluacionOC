using EvaluacionOC.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionOC.Model.Interfaces
{
    public interface IUserService
    {
        Exception Error { get; }
        Task<int> CrearUsuario(string jsonUsuairo);
        Task ModificarUsuario(User usuario);
        Task<IEnumerable<User>> ConsultaUsuarios();
        Task EliminarUsuario(User usuario);
        Task<string> Login(LoginInfo login);
        Task<IEnumerable<User>> VerificaNombre(string nombreUsuario);
        Task<IEnumerable<User>> VerificaEmail(string email);
        Task<User> ConsultaUsuario(int id);
        Task CambiarPassword(User usuario);


    }
}
