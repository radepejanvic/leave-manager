using System.Reflection.Metadata.Ecma335;

namespace Models.Models
{
    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Sender { get; set; }

        public override string ToString()
        {
            return $"Subject: {Subject}\nBody: {Body}\n Sender: {Sender}";
        }
    }
}
