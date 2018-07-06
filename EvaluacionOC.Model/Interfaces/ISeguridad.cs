using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluacionOC.Model.Interfaces
{
    public interface ISeguridad
    {
        string Cifrar(string originalPassword);
    }
}
