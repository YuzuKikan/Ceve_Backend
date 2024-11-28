using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Service
{
    public class EmailService
    {
        public static string EnviarCodigoEmail(string email)
        {
            try
            {
                string EmailOrigen = "remusalbertoiorga@gmail.com";
                string EmailDestino = email;
                string Contraseña = "aion pbou kduj ijed"; // contraseña de aplicación, generada para el correo por Google
                string newCode = CodeService.GenerarCodigoAleatorio();

                // Crear el mensaje de correo
                MailMessage mailMessage = new MailMessage(EmailOrigen, EmailDestino, "Código de confimación", $"Se le ha enviado el siguiente código: {newCode}");
                mailMessage.IsBodyHtml = true;

                // Configurar el cliente SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                // smtp.Host = "smtp-gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

                smtp.Send(mailMessage);

                smtp.Dispose();

                return newCode; // Indicar que el correo se envió correctamente [se ha generado CODE y retorna String]
            }
            catch (Exception e)
            {
                return null; // Indicar que hubo un error [no se ha generado CODE]
            }

        }
    }
}
