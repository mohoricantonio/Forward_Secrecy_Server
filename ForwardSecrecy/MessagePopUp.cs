using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForwardSecrecy
{
    public partial class MessagePopUp : Form
    {
        string message;
        public MessagePopUp(string message)
        {
            InitializeComponent();
            this.message = message;
        }

        private void MessagePopUp_Load(object sender, EventArgs e)
        {
            rtxtMessage.Text = message;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
