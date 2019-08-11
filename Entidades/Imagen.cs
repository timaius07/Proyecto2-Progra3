using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entidades
{
    public class Imagen
    {/// <summary>
    /// id de la imagen
    /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// imagen que el jugador seleciono
        /// </summary>
        public Image Foto { get; set; }
    }
}
