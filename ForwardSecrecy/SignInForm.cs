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
            string port = txtPort.Text;
            string email = txtEmailLogin.Text;
            string password = txtPasswordLogin.Text;
            string host = txtHost.Text;

            if(host =="" || port == "" || email == "" || password == "")
            {
                MessageBox.Show("Plese enter all data!");
            }
            else
            {
                if(ValidateCredentials(host, int.Parse(port), email, password))
                {
                    EmailHomeForm form = new EmailHomeForm(email);
                    this.Hide();
                    form.ShowDialog();
                    
                    txtPort.Text = "";
                    txtEmailLogin.Text = "";
                    txtHost.Text = "";
                    txtPasswordLogin.Text = "";
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
    }
}