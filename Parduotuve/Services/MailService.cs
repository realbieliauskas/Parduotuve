using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Parduotuve.Data.Entities;
using Parduotuve.Data.Enums;
using Parduotuve.Data.Repositories;

namespace Parduotuve.Services;

public class MailService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    public MailService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    private SmtpClient GetClient()
    {
        SmtpClient client = new SmtpClient(_configuration["SmtpServerUrl"],int.Parse(_configuration["SmtpPort"]));
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(_configuration["SmtpUsername"], _configuration["SmtpPassword"]);
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        return client;
    }

    private MailMessage? GetNewArrivalMessage(Skin newSkin, User recipient)
    {
        MailMessage message = new MailMessage();

        MailAddress? temp;
        
        message.From = new MailAddress(_configuration["SmtpUsername"], "Parduotuvė");
        if (!MailAddress.TryCreate(recipient.Email, out temp))
        {
            return null;
        }
        message.To.Add(temp);
        message.Subject = "🔥 New Skin in Parduotuvė!";
        
        
        string htmlBody = $@"
<html>
<head>
  <style>
    body {{ font-family: Arial, sans-serif; }}
    .container {{
        text-align: center;
        padding: 20px;
    }}
    .header {{
        font-size: 24px;
        font-weight: bold;
        color: #ff4500;
    }}
    .price {{
        font-size: 18px;
        color: #333;
        margin-top: 5px;
    }}
    img {{
        margin-top: 15px;
        width: 400px;
        height: auto;
        border-radius: 10px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    }}
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>{newSkin.Name}</div>
    <div class='price'>Price: {newSkin.Price} €</div>
    <img src='{newSkin.CinematicSplashUrl}' alt='{newSkin.Name} Skin'>
  </div>
</body>
</html>
";

        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
        message.AlternateViews.Add(htmlView);
        return message;
    }
    public async Task BroadcastNewArrivalNotification(Skin newSkin)
    {
        List<Task> sendEmailTasks = new();
        foreach (User user in (await _userRepository.GetAllAsync()).Where(u => u.Role.Equals(UserRole.User)).ToList())
        {
            MailMessage? newArrivalMessage = GetNewArrivalMessage(newSkin, user);
            if (newArrivalMessage == null)
            {
                continue;
            }
            sendEmailTasks.Add(GetClient().SendMailAsync(newArrivalMessage));
        }
        await Task.WhenAll(sendEmailTasks.ToArray());
    }
}