using Application.Abstractions.Authentication;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Infrastructure.Authentication;

public class EmailSender : IEmailSender
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public EmailSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUsername = smtpUsername;
        _smtpPassword = smtpPassword;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Create the MimeMessage object
        using var emailMessage = new MimeMessage();

        // Set the sender and recipient
        emailMessage.From.Add(new MailboxAddress("Identity App", _smtpUsername));
        emailMessage.To.Add(new MailboxAddress("Recipient Name", email));

        // Set the subject and body
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("html") { Text = htmlMessage };

        // Send the email
        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}
