using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EvaluacionOC.Model.Model
{
    public class User
    {   
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        public string Email { get; set; }
        [Required]
        [MinLength(7), MaxLength(50)]        
        public string NombreUsuario { get; set; }
        
        [Required]
        [MinLength(10)]
        [RegularExpression(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$")]
        public string Password { get; set; }
        //[Required(AllowEmptyStrings = true)]
        
        public bool Status { get; set; }
        public byte GeneroId { get; set; }
        [JsonIgnore]
        public Genero Genero { get; set; }        
        public DateTime? FechaCreacion { get; set; }



        public User()
        {

        }
        public User(UsuarioModificar uMod)
        {
            this.Id = uMod.Id;
            this.Email = uMod.Email;
            this.NombreUsuario = uMod.NombreUsuario;
            this.GeneroId = uMod.GeneroId;
        }
        public User(UsuarioEliminar uEliminar)
        {
            this.Id = uEliminar.Id;
        }
        public User(UsuarioPassword uPass)
        {
            //this.Id = uPass.Id;
            this.Password = uPass.Password;
        }
    }
}
