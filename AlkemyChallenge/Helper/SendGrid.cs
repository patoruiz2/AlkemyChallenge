﻿using AlkemyChallenge.Model.ViewModel;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace AlkemyChallenge.Helper
{
    public static class SendGrid 
    {
        public static void Main2(SendEmailViewModel model)
        {
            Execute(model).Wait();
        }
        static async Task Execute(SendEmailViewModel model)
        {
            
            var apiKey = Environment.GetEnvironmentVariable("API_KEY_ALKEMYCHALLENGE");
            
            if (apiKey == null)
            {
                Environment.SetEnvironmentVariable("API_KEY_ALKEMYCHALLENGE",
                    "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");
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(model.From, "Patricio Ruiz");
            var subject = model.Subject;
            var to = new EmailAddress(model.To, "Patricio Ruiz");
            var plainTextContent = model.PlainTextContent;
            var htmlContent = model.htmlContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

    }
}
