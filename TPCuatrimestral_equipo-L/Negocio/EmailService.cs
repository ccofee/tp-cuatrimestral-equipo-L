using System.Net.Mail;

namespace Negocio
{
    public class EmailService
    {
        public void EnviarMail(string destinatario, string asunto, string cuerpo)
        {
            using (MailMessage mensaje = new MailMessage())
            {
                mensaje.To.Add(destinatario);
                mensaje.Subject = asunto;
                mensaje.Body = cuerpo;
                mensaje.IsBodyHtml = false;

                using (SmtpClient cliente = new SmtpClient())
                {
                    cliente.Send(mensaje);
                }
            }
        }
    }
}
