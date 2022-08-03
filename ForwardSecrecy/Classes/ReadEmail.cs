using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Pop3;

namespace ForwardSecrecy.Classes
{
    public class ReadEmail
    {
        public Account LoggedUser { get; set; }
        public int Fetched { get; set; }
        public ReadEmail(Account loggedUser)
        {
            LoggedUser = loggedUser;
            Fetched = 0;
        }

        public List<MimeKit.MimeMessage> FetchAllMessages()
        {
            List<MimeKit.MimeMessage> returnMe = new List<MimeKit.MimeMessage>();

            using (Pop3Client client = new Pop3Client())
            {
                try
                {
                    client.Connect(LoggedUser.POP3host, LoggedUser.POP3port, true);
                    client.Authenticate(LoggedUser.Email, LoggedUser.Password);
                    for(int i = client.GetMessageCount()-1; i>=0 && i>client.GetMessageCount() - 51; i--)
                {
                        returnMe.Add(client.GetMessage(i));
                        Fetched++;
                }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

                return returnMe;
        }
        public int NumberOfMessages()
        {
            int returnMe = 0;
            using (Pop3Client client = new Pop3Client())
            {
                try
                {
                    client.Connect(LoggedUser.POP3host, LoggedUser.POP3port, true);
                    client.Authenticate(LoggedUser.Email, LoggedUser.Password);
                    returnMe = client.GetMessageCount();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return returnMe;
        }
        public MimeKit.MimeMessage FetchMessage(int index)
        {
            MimeKit.MimeMessage returnMe = null;
            using (Pop3Client client = new Pop3Client())
            {
                try
                {
                    client.Connect(LoggedUser.POP3host, LoggedUser.POP3port, true);
                    client.Authenticate(LoggedUser.Email, LoggedUser.Password);
                    returnMe = client.GetMessage(index);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return returnMe;
        }

    }
}
