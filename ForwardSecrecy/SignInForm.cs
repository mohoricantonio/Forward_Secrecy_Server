using ForwardSecrecy.Classes;
using System.Net.Mail;

namespace ForwardSecrecy
{
    public partial class SignInForm : Form
    {
        public SignInForm()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string port = txtPort.Text;
            string email = txtEmailLogin.Text;
            string password = txtPasswordLogin.Text;
            string host = txtHost.Text;
            string pop3host = txtPOP3host.Text;
            string pop3port = txtPOP3port.Text;

            if (name == "" || host == "" || port == "" || email == "" || password == "" || pop3host == "" || pop3port == "") 
            {
                MessageBox.Show("Plese enter all data!");
            }
            else
            {
                if(ValidateCredentials(host, int.Parse(port), email, password))
                {
                    Account loggedUser = new Account(name, password, email, host, int.Parse(port), int.Parse(pop3port), pop3host);
                    EmailHomeForm form = new EmailHomeForm(loggedUser);
                    this.Hide();
                    form.ShowDialog();
                    
                    txtPort.Text = "";
                    txtEmailLogin.Text = "";
                    txtHost.Text = "";
                    txtPasswordLogin.Text = "";
                    txtName.Text = "";
                    txtPOP3port.Text = "";
                    txtPOP3host.Text = "";
                    this.Show();
                }
            }
        }
        private bool ValidateCredentials(string host, int port, string email, string password)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(host, port, false);
                    client.Authenticate(email, password);
                    client.Disconnect(true);

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

            }
        }

        private void SignInForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}