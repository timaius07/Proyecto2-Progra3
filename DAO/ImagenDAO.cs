using Entidades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAO
{
    public class ImagenDAO
    {/// <summary>
    /// inserta un imagen en la base de datos
    /// </summary>
    /// <param name="imagen">imagen seleccionada</param>
    /// <param name="cn">coneccion abierta</param>
    /// <returns></returns>
        public int InsertarFoto(Imagen imagen, NpgsqlConnection cn)
        {
            int id = 0;
            try
            {
                string sql = @"INSERT INTO imagen (imagen) VALUES (:imagen) RETURNING id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, cn);
                // arreglo de bytes
                MemoryStream stream = new MemoryStream();

                imagen.Foto.Save(stream, ImageFormat.Jpeg);
                byte[] PIC = stream.ToArray();

                cmd.Parameters.AddWithValue(":imagen", PIC);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return id;
        }
        /// <summary>
        /// carga una imagen de la base de datos con el id
        /// </summary>
        /// <param name="id_foto">id de la foto</param>
        /// <returns></returns>
        public Imagen CargarFoto(int id_foto)
        {
            Imagen f = new Imagen();
            try
            {
                f.Id = id_foto;
                using (NpgsqlConnection cn = new NpgsqlConnection(Configuracion.CadenaConexion))
                {
                    cn.Open();
                    string sql = @"select imagen from imagen where id = :id";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue(":id", id_foto);

                    byte[] foto = new byte[0];
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        foto = (byte[])reader["imagen"];
                        MemoryStream stream = new MemoryStream(foto);
                        f.Foto = Image.FromStream(stream);
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return f;

        }




    }
}
