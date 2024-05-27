using System.Net.Mail;
using System.Net;

namespace Randevuburada.WebApi.Model.MailModel
{
    public class MailDto
    {
        public string senderAddress { get; set; }
        public string password { get; set; }
        public string receiverAddress { get; set; }

        public MailDto(string SenderAddress, string Password, string ReceiverAddress)
        {
            this.senderAddress = SenderAddress;
            this.password = Password;
            this.receiverAddress = ReceiverAddress;
        }

        public void SendMail(string message, string subject)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Port = 587; // Şifrelenmemiş Port: 587, TLS/SSL Portu: 465
                smtpClient.Host = "randevuburada.com"; // Giden posta sunucusu
                smtpClient.EnableSsl = false; // SSL/TLS kullanılacak
                smtpClient.UseDefaultCredentials = false; // Varsayılan kimlik bilgileri kullanılmayacak
                smtpClient.Credentials = new NetworkCredential(senderAddress, password); // Giden posta sunucusu için kimlik doğrulama
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; // Mail'in teslim yöntemi
                smtpClient.Timeout = 20000; // Bağlantı zaman aşımı süresi (milisaniye cinsinden)

                mailMessage.From = new MailAddress(senderAddress); // Gönderen kişinin mail adresi
                mailMessage.To.Add(receiverAddress); // Alıcının mail adresi
                mailMessage.Subject = subject; // Mail'in konusu
                mailMessage.Body = message; // Mail'in içeriği
                mailMessage.IsBodyHtml = true; // HTML içeriği kullan

                smtpClient.Send(mailMessage); // Mail gönderme işlemi
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
