using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForwardSecrecy.Classes
{
    public class Account
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public int POP3port { get; set; }
        public string POP3host { get; set; }
        public Account(string name, string password, string email, string host, int port, int pop3port, string pop3host)
        {
            Name = name;
            Password = password;
            Email = email;
            Host = host;
            Port = port;
            POP3port = pop3port;
            POP3host = pop3host;
        }
    }
}
