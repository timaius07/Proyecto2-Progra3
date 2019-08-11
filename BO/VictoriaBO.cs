using DAO;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class VictoriaBO
    {
        /// <summary>
        /// variable para instanciar la clase DAO
        /// </summary>
        VictoriaDAO vidao;
/// <summary>
/// variable para los minutos
/// </summary>
        int m = 0;
        /// <summary>
        /// variable para los segundos
        /// </summary>
        int s = 0;
        /// <summary>
        /// variable para los horas
        /// </summary>
        int h = 0;
        /// <summary>
        /// variable que contine la victoria del jugador #!
        /// </summary>
        Victoria vic1 = new Victoria();
        /// <summary>
        /// variable que contine la victoria del jugador #2
        /// </summary>
        Victoria vic2 = new Victoria();
        /// <summary>
        /// variable que contine la victoria del jugador #3
        /// </summary>
        Victoria vic3 = new Victoria();
        /// <summary>
        /// variable que contine la victoria del jugador #4
        /// </summary>
        Victoria vic4 = new Victoria();
        /// <summary>
        /// variable que contine la victoria del jugador #5
        /// </summary>
        Victoria vic5 = new Victoria();

        /// <summary>
        /// constructor
        /// </summary>
        public VictoriaBO()
        {
            vidao = new VictoriaDAO();
        }
        /// <summary>
        /// guarda una victoria ya al finalizar la partida
        /// </summary>
        /// <param name="resul">contiene el tiempo en segudos</param>
        /// <param name="Id_jugador">id del jugador que gano la partida</param>
        public void GuardarVictoria(int resul, int Id_jugador)
        {
            vidao.GuardarVictoria(resul, Id_jugador);
        }
        /// <summary>
        /// trae todas las victorias ganadas de un jugador
        /// </summary>
        /// <param name="id">id de jugador</param>
        /// <returns>devuelve una lista de Victorias de un juga</returns>
        public List<Victoria> CargarVictoria_Jugador(int id)
        {
            List<Victoria> lisv = vidao.CargarVictoria_Jugador(id);

            return lisv;
        }
        /// <summary>
        /// convierte una cantidad de segundos el tiempo con formato HH/MM/SS
        /// </summary>
        /// <param name="resulSegundos">cantidad de segundos</param>
        /// <returns></returns>
        public string ConvertirTiempo(int resulSegundos)
        {
            string numero = "";
            s = 0;
            m = 0;
            h = 0;
            for (int i = 0; i < resulSegundos; i++)
            {

                s++;
                if (s == 59)
                {
                    m += 1;
                    s = 0;
                }
                if (m == 59)
                {
                    h += 1;
                    m = 0;
                }


            }
            string hh = Convert.ToString(h);
            string mm = Convert.ToString(m);
            string ss = Convert.ToString(s);
            numero = hh + " : " + m + " : " + s;
            return numero;
        }
        /// <summary>
        /// carga todas la victorias de la base de datos
        /// </summary>
        /// <returns>lista de todas las victorias </returns>
        public List<Victoria> CargarTop5Victorias()
        {
            List<Victoria> lisV = vidao.CargarTop5Victoria();


            return lisV;

        }
        /// <summary>
        /// extrae un los 5 mejores tiempos de todas las victorias
        /// </summary>
        /// <param name="lisV">lista de todas las victorias</param>
        /// <returns>matriz con los 5 mejores tiempos</returns>
        public Victoria[] Top5(List<Victoria> lisV)
        {
            int con = 0;
            Victoria[] top5 = new Victoria[5];
            bool verfica = false;
            bool ver =  false;

            for (int i = 0; i < 5; i++)
            {
                top5[i] = new Victoria();
            }
            foreach (Victoria vict in lisV)
            {
                Victoria vic = lisV[con];

                int acum = 0;
                int acum1 = 0;
                int acum2 = 0;
                int acum3 = 0;
                int idJuAcum = 0;
                int idJuAcum1 = 0;
                int idJuAcum2 = 0;
                int idJuAcum3 = 0;
                ver = false;

                for (int i = 0; i < 5; i++)
                {
                    if (top5[i].SegundoTotales == 0 && verfica == false)
                    {
                        top5[i].SegundoTotales = vic.SegundoTotales;
                        top5[i].id_jugador = vic.id_jugador;
                        verfica = true;
                    }

                    if (top5[i].SegundoTotales != 0 && vic.SegundoTotales < top5[i].SegundoTotales && ver== false)
                    {
                        int x = i;
                        acum = top5[x].SegundoTotales;
                        idJuAcum = top5[x].id_jugador;
                        top5[x].SegundoTotales = vic.SegundoTotales;
                        top5[x].id_jugador = vic.id_jugador;
                        x++;
                        if (x < 5)
                        {
                            acum1 = top5[x].SegundoTotales;
                            idJuAcum1 = top5[x].id_jugador;
                            top5[x].SegundoTotales = acum;
                            top5[x].id_jugador = idJuAcum;
                        }
                        x++;
                        if (x < 5)
                        {
                            acum2 = top5[x].SegundoTotales;
                            idJuAcum2 = top5[x].id_jugador;
                            top5[x].SegundoTotales = acum1;
                            top5[x].id_jugador = idJuAcum1;
                        }
                        x++;
                        if (x < 5)
                        {
                            acum3 = top5[x].SegundoTotales;
                            idJuAcum3 = top5[x].id_jugador;
                            top5[x].SegundoTotales = acum2;
                            top5[x].id_jugador = idJuAcum2;
                        }
                        x++;
                        if (x < 5)
                        {
                            top5[x].SegundoTotales = acum3;
                            top5[x].id_jugador = idJuAcum3;
                        }
                        ver = true;
                    }
                }
                con++;
                verfica = false;
            }
            return top5;
        }

    }
}
