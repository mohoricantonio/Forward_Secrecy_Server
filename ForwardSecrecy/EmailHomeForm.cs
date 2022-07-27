using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ForwardSecrecy.Classes;
using ForwardSecrecy.UControls;

namespace ForwardSecrecy
{
    public partial class EmailHomeForm : Form
    {
        private Account LoggedUser;
        public EmailHomeForm(Account loggedUser)
        {
            InitializeComponent();
            LoggedUser = loggedUser;
        }

        private void btnMailRcv_Click(object sender, EventArgs e)
        {
            ReceivedMailUC uc = new ReceivedMailUC();
            AddUserControl(uc);
            
        }
        private void AddUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnNewMail_Click(object sender, EventArgs e)
        {
            NewMailUC uc = new NewMailUC(LoggedUser);
            AddUserControl(uc);
        }

        private void EmailHomeForm_Load(object sender, EventArgs e)
        {
            ReceivedMailUC uc = new ReceivedMailUC();
            AddUserControl(uc);
        }
    }
}
