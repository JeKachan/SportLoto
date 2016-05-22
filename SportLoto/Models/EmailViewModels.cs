using Postal;

namespace SportLoto.Models
{
    public class RegistrationEmail : Email
    {
        public string To { get; set; }
        public string UserName { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string CallbackUrl { get; set; }

        public RegistrationEmail(string viewName) : base(viewName) { }
    }
}