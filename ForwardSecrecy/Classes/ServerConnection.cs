using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ForwardSecrecy.Classes
{
    public class ServerConnection
    {
        private TcpClient? Client { get; set; }
        private NetworkStream? Stream;
        bool Connected;
        public ServerConnection()
        {
            Client = null;
            Stream = null;
            Connected = false;
        }
        public int ConnectToServer()
        {
            try
            {
                Client = new TcpClient("192.46.236.42", 54000); //60500 stari 54000 novi
                Stream = Client.GetStream();
                Connected = true;
                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
                
            }
        }
        public void SendMessageToServer(string message)
        {
            if (Connected)
            {
                int byteCount = Encoding.ASCII.GetByteCount(message + 1);
                byte[] sendData = Encoding.ASCII.GetBytes(message);
                Stream.Write(sendData, 0, sendData.Length);
                MessageBox.Show("Key sent!");
            }
            
        }
        public string ReadMessageFromServer()
        {
            string returnMe = "";
            if (Connected)
            {
                StreamReader sr = new StreamReader(Stream);
                returnMe = sr.ReadLine();
            }
            

            return returnMe;
        }
        public void CloseConnectionToServer()
        {
            if (Connected)
            {
                Stream.Close();
                Client.Close();
                Connected = false;
            }
        }
    }
}
