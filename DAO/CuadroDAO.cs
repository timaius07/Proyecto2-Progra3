using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace DAO
{
   public class CuadroDAO

    {
        Cuadro cuad = new Cuadro();
/// <summary>
/// llena los valores de la matriz con los datos correspondientes
/// </summary>
        public void LlenarTablero()
        {
           
            cuad.Tablero = new bool [6,5];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cuad.Tablero[i, j] = false;
                }
            }
        }
        /// <summary>
        /// pone en true la posicion correspondiente
        /// </summary>
        /// <param name="x"># de la fila</param>
        /// <param name="y"><# numero de la columna/param>
        public void Validar_Posicion(int x, int y)
        {
            cuad.Tablero[x, y] = true;
        }
        /// <summary>
        /// verifica si la jugada es la ganadora de algun jugador
        /// </summary>
        /// <param name="comodin">verifica si ya se utilizo el comodin</param>
        /// <returns></returns>
        public bool Jugada(bool comodin)
        {
            bool ganar = false;
            int poscolum = 0;
            int contganes = 0;
            int contjugadas = 0;
            //ciclo para recorrer las colunmas de la matriz
            for (int c = 0; c < 5; c++)
            {

                for (int f = 0; f < 6; f++)
                {
                    if (cuad.Tablero[f, c] == true)
                    {
                        contjugadas += 1;
                    }
                    else if (contjugadas == 5)
                    {
                        break;
                    }
                    else
                    {
                        contjugadas = 0;
                    }
                }
                if (contjugadas >= 5)
                {
                    contganes++;
                }

                if (poscolum != c || poscolum==0)
                {
                    contjugadas = 0;
                }

                poscolum = c;
            }

            //ciclo para recorrer todas la filas de la matriz

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (cuad.Tablero[i, j] == true)
                    {
                        contjugadas += 1;
                    }
                    else
                    {
                        contjugadas = 0;
                    }
                }
                if (contjugadas == 5)
                {
                    contganes++;
                }
                contjugadas = 0;
            }
            //validacion por si gana con la ficha en una esquina
            if (comodin == false & contganes >= 1)
            {
                ganar = true;
                goto Salir;

            }
            else if (comodin == true & contganes == 2)
            {
                ganar = true;
                goto Salir;
            }

            Salir:
            return ganar;
        }

    }
}
