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
        public Account(string name, string password, string email, string host, int port)
        {
            Name = name;
            Password = password;
            Email = email;
            Host = host;
            Port = port;
        }
    }
}
