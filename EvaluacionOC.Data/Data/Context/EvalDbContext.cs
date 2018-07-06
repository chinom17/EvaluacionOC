using EvaluacionOC.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluacionOC.Data.Data.Context
{
    public class EvalDbContext : DbContext
    {
        public EvalDbContext(DbContextOptions<EvalDbContext> options):
            base(options)
        {

        }

        public DbSet<User> Usuario  { get; set; }
        public DbSet<Genero> Genero { get; set; }
    }
}
