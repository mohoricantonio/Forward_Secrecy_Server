using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
            ReceivedMailUC uc = new ReceivedMailUC(LoggedUser);
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
            ReceivedMailUC uc = new ReceivedMailUC(LoggedUser);
            AddUserControl(uc);
        }

        private void btnSendKey_Click(object sender, EventArgs e)
        {
            ServerConnection server = new ServerConnection();
            if (server.ConnectToServer() == 0)
            {
                server.SendMessageToServer(CreateKeyPair());
                MessageBox.Show(server.ReadMessageFromServer());
                server.CloseConnectionToServer();
            }
        }
        private string CreateKeyPair()
        {
            string returnMe = "";

            byte[] publicKey;
            byte[] privateKey;

            using (ECDiffieHellmanCng edc = new ECDiffieHellmanCng(CngKey.Create(CngAlgorithm.ECDiffieHellmanP256, null,
                new CngKeyCreationParameters { ExportPolicy = CngExportPolicies.AllowPlaintextExport })))
            {
                edc.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                edc.HashAlgorithm = CngAlgorithm.Sha256;
                publicKey = edc.Key.Export(CngKeyBlobFormat.EccPublicBlob);
                privateKey = edc.Key.Export(CngKeyBlobFormat.EccPrivateBlob);
            }

            StringBuilder stringBuilder = new StringBuilder(publicKey.Length *2);
            foreach (byte b in publicKey)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            DateTime now = DateTime.Now.AddDays(1);

            returnMe = "save!/&" + LoggedUser.Email + "!/&" + stringBuilder.ToString() + "!/&" + now.ToString("dd.MM.yyyy. HH:mm:ss");

            return returnMe;
        }
    }
}
