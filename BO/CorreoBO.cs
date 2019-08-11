using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

namespace BO
{
  public  class CorreoBO
    {
        /// <summary>
        /// Variable que contine los datos que queremos compartir
        /// </summary>
        /// 
        private MailMessage Email;
       
       Stopwatch stop = new Stopwatch();
        /// <summary>
        /// correo el cual envia a el to5 al que especifica el usuario
        /// </summary>
        ///
       string emisor = "en_manuel14@hotmail.com";
        /// <summary>
        /// contra del correo
        /// </summary>
        string contrasena= "24605017";
        /// <summary>
        /// metodo que envia un correo con el top5 de mejores timepos
        /// </summary>
        /// <param name="emisor">correo del emisor</param>
        /// <param name="contrasena">contra dell emisor</param>
        /// <param name="Body">mensaje </param>
        /// <param name="asunto">titulo</param>
        /// <param name="destinatario">correo del receptor</param>
  
        public void EnviarCorreo( string Body, string asunto, string destinatario, string ruta)
        {
            try
            {
                Email = new MailMessage();
                Email.To.Add(new MailAddress( destinatario));
                Email.From = new MailAddress(emisor);
                Email.Subject = asunto;
                Email.IsBodyHtml = true;
                Email.Body = Body;              
                SmtpClient cliente = new SmtpClient("smtp.live.com",587);
                if (ruta.Equals("")== false)
                {
                    Attachment archivo = new Attachment(ruta);
                    Email.Attachments.Add(archivo);

                }


                using (cliente)
                {
                    cliente.Credentials = new System.Net.NetworkCredential(emisor, contrasena);
                    cliente.EnableSsl = true;
                    cliente.Send(Email);
                }

                MessageBox.Show("El Correo se envio correctamente");
    }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }
    }
}
