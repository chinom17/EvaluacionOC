using EvaluacionOC.Data.Data.Context;
using EvaluacionOC.Model.Interfaces;
using EvaluacionOC.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionOC.Data.Services
{
    class UserService : IUserService
    {
        private Exception error;

        public EvalDbContext Context { get; }
        public IToken Token { get; }

        public Exception Error => error;



        public UserService(EvalDbContext context, IToken token)
        {
            Context = context;
            Token = token;
        }

        public async Task<IEnumerable<User>> VerificaNombre(string nombreUsuario)
        {
            IEnumerable<User> result = null;
            try
            {
                result = await Context.Usuario.Where(u =>
                                    u.NombreUsuario == nombreUsuario).ToListAsync();
            }
            catch (Exception ex)
            {
                error = ex;
            }
            return result;

        }

        public async Task<IEnumerable<User>> VerificaEmail(string email)
        {
            IEnumerable<User> result = null;
            try
            {
                result = await Context.Usuario.Where(u =>
                                    u.Email == email).ToListAsync();
            }
            catch (Exception ex)
            {
                error = ex;
            }
            return result;
        }
        public async Task<string> Login(LoginInfo login)
        {
            string token = string.Empty;
            IEnumerable<User> result = null;
            var parUsuario = new SqlParameter("@usuario", login.Usuario);
            var parPassword = new SqlParameter("@password", login.Password);            
            try
            {
                result = await Context.Usuario.FromSql(
                        "spLogin @usuario, @password", parUsuario, parPassword).ToListAsync();
            }
            catch(Exception ex)
            {
                error = ex;
            }            
            if (result.Count() > 0)
            {
                token = Token.CrearToken(result.FirstOrDefault());
            }
            return token;
        }

        public async Task<User> ConsultaUsuario(int id)
        {
            User usuario = null;
            try
            {
                usuario = await Context.Usuario.FindAsync(id);
            }
            catch(Exception ex)
            {
                error = ex;
            }
            return usuario;
            
        }

        public async Task<IEnumerable<User>> ConsultaUsuarios()
        {
            IEnumerable<User> usuarios = null;
            try
            {
                usuarios = await Context.Usuario.ToListAsync();

            }
            catch (Exception ex)
            {
                error = ex;
            }
            return usuarios;
        }

        public async Task<int> CrearUsuario(string jsonUsuario)
        {
            int idUsuario = 0;
            var parUsuario = new SqlParameter("@usuario", jsonUsuario);

            var result = await Context.Usuario.FromSql<User>("spCrearUsuario @usuario", parUsuario).ToListAsync();
            if (result.Count > 0)
            {
                idUsuario = result.FirstOrDefault().Id;
            }
            return idUsuario;
        }

        public async Task EliminarUsuario(User usuario)
        { 
            int id = usuario.Id;
            var uMod = Context.Usuario.Single(u => u.Id == usuario.Id);
            var entry = Context.Entry(uMod);
            entry.State = EntityState.Modified;
            entry.Property(x => x.Password).IsModified = false;            
            entry.Property(x => x.FechaCreacion).IsModified = false;
            entry.Property(x => x.GeneroId).IsModified = false;
            entry.Property(x => x.Email).IsModified = false;
            entry.Property(x => x.NombreUsuario).IsModified = false;
            uMod.Status = usuario.Status;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(id))
                {
                    error = new Exception("No encontrado");
                }
                else
                {
                    error = ex;
                }
            }
        }

        public async Task CambiarPassword(User usuario)
        {
            int id = usuario.Id;
            var uMod = Context.Usuario.Single(u => u.Id == usuario.Id);
            var entry = Context.Entry(uMod);
            entry.State = EntityState.Modified;
            entry.Property(x => x.Status).IsModified = false;
            entry.Property(x => x.FechaCreacion).IsModified = false;
            entry.Property(x => x.GeneroId).IsModified = false;
            entry.Property(x => x.Email).IsModified = false;
            entry.Property(x => x.NombreUsuario).IsModified = false;
            uMod.Password = usuario.Password;
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(id))
                {
                    error = new Exception("No encontrado");
                }
                else
                {
                    error = ex;
                }
            }
        }

        public async Task ModificarUsuario(User usuario)
        {
            int id = usuario.Id;
            var uMod = Context.Usuario.Single(u => u.Id == usuario.Id);
            

            var entry = Context.Entry(uMod);
            entry.State = EntityState.Modified;
            entry.Property(x => x.Password).IsModified = false;
            entry.Property(x => x.Status).IsModified = false;
            entry.Property(x => x.FechaCreacion).IsModified = false;
            uMod.NombreUsuario = usuario.NombreUsuario;
            uMod.Email = usuario.Email;
            uMod.GeneroId = usuario.GeneroId;
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(id))
                {
                    error = new Exception("No encontrado");
                }
                else
                {
                    error = ex;
                }
            }
        }

        private bool UserExists(int id)
        {
            return Context.Usuario.Any(e => e.Id == id);
        }
    }
}
