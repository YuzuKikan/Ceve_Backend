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
        public static bool EnviarCodigoEmail(string email, string code)
        {
            try
            {
                string EmailOrigen = "remusalbertoiorga@gmail.com";
                string EmailDestino = email;
                string Contraseña = "aion pbou kduj ijed"; // contraseña de aplicación, generada para el correo por Google

                // Crear el mensaje de correo
                MailMessage mailMessage = new MailMessage(EmailOrigen, EmailDestino, "Código de confimación", $"Se le ha enviado el siguiente código: {code}");
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

                return true; // Indicar que el correo se envió correctamente
            }
            catch (Exception ex)
            {
                return false; // Indicar que hubo un error
            }

        }
    }
}
