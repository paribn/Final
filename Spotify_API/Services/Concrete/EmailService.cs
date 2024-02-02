﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MimeKit.Text;
using Spotify_API.DTO.Account;
using Spotify_API.Entities;
using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class EmailService : IEmailService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;

        public EmailService(UserManager<AppUser> userManager,
            IConfiguration config,
            IFileService fileService)
        {
            _userManager = userManager;
            _configuration = config;
            _fileService = fileService;
        }

        public void ForgotPassword(AppUser user, string link, ForgotPasswordDto forgotPasswordDto)
        {
            // create message
            var message = new MimeMessage();

            message.From.Add(MailboxAddress.Parse(_configuration.GetSection("Smtp:Mail").Value));

            message.To.Add(MailboxAddress.Parse(forgotPasswordDto.Email));

            message.Subject = "Reset Password";

            string emailBody = string.Empty;
            string path = "wwwroot/templates/confirm.html";

            emailBody = _fileService.ReadFile(path, emailBody);

            emailBody = emailBody.Replace("{{link}}", link).Replace("{{FullName}}", user.FullName);

            message.Body = new TextPart(TextFormat.Html) { Text = emailBody };


            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("Smtp:Server").Value, int.Parse(_configuration.GetSection("Smtp:Port").Value), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("Smtp:Mail").Value, _configuration.GetSection("Smtp:Password").Value);
            smtp.Send(message);

            smtp.Disconnect(true);

        }
    }
}
