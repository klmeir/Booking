﻿namespace Booking.Application.Ports
{
    public class EmailRequest
    {        
        public List<string> To { get; }
        public string Subject { get; }
        public string? Body { get; }
        public string? From { get; }
        public string? DisplayName { get; }
        public List<string> Cc { get; }

        public EmailRequest(List<string> to, string subject, string? body = null, string? from = null, string? displayName = null, List<string>? cc = null)
        {
            To = to;
            Subject = subject;
            Body = body;
            From = from;
            DisplayName = displayName;
            Cc = cc ?? new List<string>();
        }
    }
}
