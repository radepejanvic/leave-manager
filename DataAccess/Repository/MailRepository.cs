using DataAccess.Repository.IRepository;
using MailKit.Search;
using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using static System.Environment;
using System.Runtime.InteropServices.JavaScript;
using Models.Models;
using System.Threading;
using MimeKit.Tnef;

namespace DataAccess.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly string _host, _username, _password;
        private readonly int _port;
        private readonly bool _ssl;
        private readonly ImapClient _client;

        public MailRepository()
        {
            _host = GetEnvironmentVariable("SMTP_HOST");
            _port = int.Parse(GetEnvironmentVariable("SMTP_PORT"));
            _username = GetEnvironmentVariable("SMTP_USERNAME");
            _password = GetEnvironmentVariable("SMTP_PASSWORD");
            _ssl = bool.Parse(GetEnvironmentVariable("SMTP_SSL"));
            _client = new ImapClient();
        }

        public async Task<IEnumerable<Email>> PollUnreadMailsAsync()
        {
            var messages = new List<Email>();

            using (_client)
            {
                await _client.ConnectAsync(_host, _port, _ssl);

                _client.AuthenticationMechanisms.Remove("XOAUTH2");

                await _client.AuthenticateAsync(_username, _password);

                var inbox = _client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadWrite);
                var results = inbox.Search(SearchOptions.All, SearchQuery.Not(SearchQuery.Seen));
                foreach (var uid in results.UniqueIds)
                {
                    var message = await inbox.GetMessageAsync(uid);

                    messages.Add(new Email()
                    {
                        Subject = message.Subject,
                        Sender = message.From.Mailboxes.FirstOrDefault()?.Address,
                        Body = message.TextBody
                    });

                    await inbox.AddFlagsAsync(uid, MessageFlags.Seen, true);
                }
                _client.Disconnect(true);
            }

            return messages;
        }
    }
}
