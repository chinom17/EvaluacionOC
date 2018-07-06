using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EvaluacionOC.Model.Model
{
    public class UsuarioPassword
    {
        //[JsonIgnore]
        //public int Id { get; set; }
        [MinLength(10)]
        [RegularExpression(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$")]
        public string Password { get; set; }
    }
}
