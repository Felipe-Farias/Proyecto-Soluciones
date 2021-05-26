using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Directorio.Data;
using Microsoft.Data.Sqlite;

namespace Directorio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Persona> Personas = new List<Persona>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            SqliteConnection conexion = new SqliteConnection("Data Source=directorio.db");
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "select id, nombre, apellido, telefono, id_unidad from persona";

            conexion.Open();

            SqliteDataReader lector = comando.ExecuteReader();

            while(lector.Read())
            {
                Persona p = new Persona();
                p.Id = lector.GetInt32(0);
                p.Nombre = lector.GetString(1);
                p.Apellido = lector.GetString(2);
                p.Telefono = lector.GetString(3);
                p.IdUnidad = lector.GetInt32(4);
                Personas.Add(p);
            }

        }
    }
}
