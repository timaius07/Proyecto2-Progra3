using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
 public   class Cuadro
    {     /// <summary>
    /// numero del cuadro
    /// </summary>
        public int Numero { get; set; }
        /// <summary>
        /// nos sirve para saber si el jugador1 selcciono este cuadro
        /// </summary>
        public bool Jugador1{ get; set; }
        public bool[,] Tablero { get; set; }
        /// <summary>
        /// nos sirve para saber si el jugador2 selcciono este cuadro
        /// </summary>
        public bool Jugador2 { get; set; }
    }

}
