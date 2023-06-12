using Steam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Service;

public class EmailSend
{
    public int SendCodeEmail(string email)
    {
        int code = new Random().Next(10000, 99999);

        string fromMail = "pudgearcane5@gmail.com";
        string fromPassword = "pwauepbjjpoovets";

        var strComputerName = "MonoTb";

        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = strComputerName;
        message.To.Add(new MailAddress(email));
        message.Body = "<html><body>";
        message.Body += code;
        message.Body += "<br>";
        message.Body += "<b>Have a good day!<b>";
        message.IsBodyHtml = true;

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromMail, fromPassword),
            EnableSsl = true,
        };

        smtpClient.Send(message);

        return code;
    }

}
