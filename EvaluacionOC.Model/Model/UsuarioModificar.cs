using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EvaluacionOC.Model.Model
{
    public class UsuarioModificar
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(7), MaxLength(50)]
        public string NombreUsuario { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public byte GeneroId { get; set; }
    }
}
