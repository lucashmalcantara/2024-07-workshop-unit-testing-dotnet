using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Examples.Notification.Api.Domain.Models
{
    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        private readonly IEnumerable<string> _blockedTerms;
        private readonly Regex _emailRegex = new(pattern: "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", RegexOptions.Compiled);
        private const int MinSubjectLength = 3;


        public EmailMessage(string from, string to, string subject, string message, IEnumerable<string> blockedTerms)
        {
            From = from;
            To = to;
            Subject = subject;
            Message = message;
            _blockedTerms = blockedTerms;
        }

        public IReadOnlyCollection<string> Validate()
        {
            var errors = new List<string>();

            if (!IsValidEmail(From))
                errors.Add("Invalid 'from' e-mail.");

            if (!IsValidEmail(To))
                errors.Add("Invalid 'to' e-mail.");

            if (!IsValidSubject())
                errors.Add("The subject must have three or more characters.");

            if (ContainsBlockedTerm())
                errors.Add("The content of the message does not comply with our policies.");

            return errors;
        }

        private bool ContainsBlockedTerm()
        {
            return _blockedTerms.Any(b => Message.Contains(b, StringComparison.CurrentCultureIgnoreCase));
        }

        private bool IsValidSubject()
        {
            return Subject.Length >= MinSubjectLength;
        }

        private bool IsValidEmail(string email)
        {
            return _emailRegex.IsMatch(email);
        }
    }
}
