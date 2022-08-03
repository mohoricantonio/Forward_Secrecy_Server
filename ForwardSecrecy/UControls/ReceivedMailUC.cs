using ForwardSecrecy.Classes;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForwardSecrecy.UControls
{
    public partial class ReceivedMailUC : UserControl
    {
        private Account LoggedUser;
        private ReadEmail read;
        public ReceivedMailUC(Account loggedUser)
        {
            InitializeComponent();
            this.LoggedUser = loggedUser;
            read = new ReadEmail(LoggedUser);
        }

        private void ReceivedMailUC_Load(object sender, EventArgs e)
        {
            FillRcvMail(read);
        }
        private void FillRcvMail(ReadEmail read)
        {
            List<string> from = new List<string>();
            List<string> subject = new List<string>();
            List<string> id = new List<string>();
            int i = read.NumberOfMessages() - 1;
            foreach(MimeMessage m in read.FetchAllMessages())
            {
                id.Add(i.ToString());
                from.Add(m.From.ToString());
                subject.Add(m.Subject.ToString());
                i--;
            }
            for(int j = 0; j < read.Fetched; j++)
            {
                dgvRcvMail.Rows.Add(id[j], from[j], subject[j]);
            }
        }

        private void btnOpenMessage_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(dgvRcvMail.CurrentRow.Cells[0].Value);
            MessagePopUp form = new MessagePopUp(read.FetchMessage(index).TextBody.ToString());
            form.ShowDialog();
        }
    }
}
