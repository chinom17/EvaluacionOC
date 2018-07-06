using EvaluacionOC.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluacionOC.Model.Interfaces
{
    public interface IToken
    {
        string CrearToken(User usuario);
    }
}
