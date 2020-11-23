using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaKlas
{
    public class ServerEchoAPM : ServerEcho
    {
        public delegate void TransmissionDataDelegate(NetworkStream stream);
        
        public ServerEchoAPM(IPAddress IP, int port) : base(IP, port)
        {
        }

        protected override void AcceptClient()
        {
            while (true)
            {
                TcpClient tcpClient = TcpListener.AcceptTcpClient();
                Stream = tcpClient.GetStream();
                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);
                //callback style
                //transmissionDelegate.BeginInvoke(Stream, TransmissionCallback, tcpClient); // <- nie dziala
                // async result style
                // IAsyncResult result = transmissionDelegate.BeginInvoke(Stream, null, null); // <- nie dziala
                Task task = Task.Run(() => transmissionDelegate(Stream));
                ////operacje......
                //while (!result.IsCompleted) ;
                ////sprzątanie
            }
        }

        private void TransmissionCallback(IAsyncResult ar)
        {
            // sprzątanie
        }
        
        protected override void BeginDataTransmission(NetworkStream stream)
        {
            byte[] buffer = new byte[Buffer_size];
            UI ui = new UI();
            while (true)
            {
                try
                {
                    byte[] menu = System.Text.Encoding.ASCII.GetBytes(ui.GetMenu());
                    stream.Write(menu, 0, menu.Length);

                    int msg_len = stream.Read(buffer, 0, 1024);
                    string msg = System.Text.Encoding.UTF8.GetString(buffer);
                    string ans = ui.Answer(msg);
                    byte[] answer = System.Text.Encoding.ASCII.GetBytes(ans);
                    stream.Write(answer, 0, answer.Length);
                    msg_len = stream.Read(buffer, 0, 1024);
                    Array.Clear(buffer, 0, buffer.Length);
                }
                catch (IOException e)
                {
                    break;
                }
            }
        }
        public override void Start()
        {
            StartListening();
            //transmission starts within the accept function
            AcceptClient();
        }

    }
}
