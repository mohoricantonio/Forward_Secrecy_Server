using ForwardSecrecy.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForwardSecrecy.UControls
{
    
    public partial class NewMailUC : UserControl
    {
        private Account LoggedUser;
        public NewMailUC(Account loggedUser)
        {
            InitializeComponent();
            LoggedUser = loggedUser;
        }

        private void NewMailUC_Load(object sender, EventArgs e)
        {
            txtSender.Text = LoggedUser.Email;
            txtName.Text = LoggedUser.Name;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(txtSendTo.Text != "" && txtSubject.Text != "" && richTxtBody.Text != "")
            {
                SendMail();
            }
        }
        private void SendMail()
        {

            MailMessage mail = new MailMessage(LoggedUser.Email , txtSendTo.Text, txtSubject.Text, richTxtBody.Text);
            SmtpClient client = new SmtpClient(LoggedUser.Host, LoggedUser.Port);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(LoggedUser.Email, LoggedUser.Password);
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
                MessageBox.Show("Mail was sent successfuly!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
