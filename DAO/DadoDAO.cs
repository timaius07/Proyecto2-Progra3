using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DadoDAO
    { /// <summary>
    /// metodo para tirar el dado
    /// </summary>
    /// <param name="imagenOrigen"></param>
    /// <param name="imagenSuperpuesta"></param>
    /// <param name="anchorelativo"></param>
    /// <param name="altorelativo"></param>
    /// <param name="posrelativaX"></param>
    /// <param name="posrelativaY"></param>
    /// <param name="relativo"></param>
    /// <returns></returns>
        public MemoryStream Mezclar_Imagenes_Dado(Stream imagenOrigen, Stream imagenSuperpuesta, double anchorelativo, double altorelativo, double posrelativaX, double posrelativaY, bool relativo)
        {
            // Convierte a image el fichero original y el que vamos a superponer
            System.Drawing.Image original = System.Drawing.Image.FromStream(imagenOrigen);
            System.Drawing.Image superpuesta = System.Drawing.Image.FromStream(imagenSuperpuesta);

            // Decidimos las dimensiones en base a si son relativas o absolutas
            double posicionX;
            double posicionY;
            double ancho;
            double alto;

            if (relativo)
            {
                posicionX = posrelativaX * original.Width;
                posicionY = posrelativaY * original.Height;
                ancho = anchorelativo * original.Width;
                alto = altorelativo * original.Height;
            }
            else
            {
                posicionX = posrelativaX;
                posicionY = posrelativaY;
                ancho = anchorelativo;
                alto = altorelativo;
            }

            // A mezclar se ha dicho
            System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(imagenOrigen);
            Bitmap bmPhoto = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppRgb);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            grPhoto.SmoothingMode = SmoothingMode.HighSpeed;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rec = new Rectangle(0, 0, original.Width, original.Height);
            grPhoto.DrawImage(imgPhoto, rec, rec, GraphicsUnit.Pixel);
            grPhoto.DrawImage(superpuesta, new Rectangle((int)posicionX, (int)posicionY, (int)ancho, (int)alto), new Rectangle(0, 0, superpuesta.Width, superpuesta.Height), GraphicsUnit.Pixel);

            MemoryStream mm = new MemoryStream();
            bmPhoto.Save(mm, System.Drawing.Imaging.ImageFormat.Png);

            // Cerramos todo lo cerrable

            original.Dispose();
            imgPhoto.Dispose();
            bmPhoto.Dispose();
            grPhoto.Dispose();

            return mm;
        }


   
    }


}
