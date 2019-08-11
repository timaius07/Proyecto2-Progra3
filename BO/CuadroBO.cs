using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
namespace BO
{
  public  class CuadroBO

    {/// <summary>
    /// variable del cuadroDAo
    /// </summary>
        CuadroDAO cudD = new CuadroDAO();
        /// <summary>
        /// metodo que llena la matriz 
        /// </summary>
        public void Llenar_Cuadro()
        {
            cudD.LlenarTablero();
        }

        /// <summary>
        /// pone true la pasicion marcada
        /// </summary>
        /// <param name="x">fila</param>
        /// <param name="y">columna</param>
        public void Validar_PosicionCuad(int x, int y)
        {
            cudD.Validar_Posicion(x,y);
        }

        /// <summary>
        /// valida si el jugador a ganado
        /// </summary>
        /// <param name="usocomodin"></param>
        /// <returns></returns>
        public bool Ganar(bool usocomodin)
        {
            return cudD.Jugada(usocomodin);
        }
    }
}
