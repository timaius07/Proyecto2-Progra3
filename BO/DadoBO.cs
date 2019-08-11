using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DAO;

namespace BO
{
    public class DadoBO
    {
        private Random numero;
        private DadoDAO pdao;
       
        public DadoBO()
        {
            numero = new Random();
            pdao = new DadoDAO();
        }
        public int LanzarDado()
        {
            return numero.Next(1, 7);
        }

        public int Revolver_Fichas()
        {       
            return numero.Next(0, 6);
        }
        public int Revolver_Jugador()
        {
            return numero.Next(0, 8);
        }
        public MemoryStream mezclar (Stream imagen1, Stream imagen2,int x,int y,int altorel,int anchorel,bool relativo)
        {
          return  pdao.Mezclar_Imagenes_Dado(imagen1, imagen2, x, y, altorel, anchorel, relativo);
        }

    }
}
