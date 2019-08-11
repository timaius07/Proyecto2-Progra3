using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
  public  class Jugador
    {/// <summary>
    /// id de jugador
    /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// nombre del jugador
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// contraseña de jugador 
        /// </summary>
        public string Contrasena { get; set; }
        /// <summary>
        /// imagen de perfil de jugador
        /// </summary>
        public Imagen Imagen { get; set; }
        /// <summary>
        /// correo de juggador
        /// </summary>
        public string correo { get; set; }
        /// <summary>
        /// telefono del jugador 
        /// </summary>
        public string telefono { get; set; }
    



    }
}
