using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace NetCodeAttempt
{
    public class ClientConnection
    {
        int clientConnectionPort { get; set; }
        IPEndPoint endPoint { get; set; }
        UdpClient client { get; set; }
        UdpState state { get; set; }

        public ClientConnection(UdpClient _client, IPEndPoint _endPoint)
        {
            clientConnectionPort = 20001;
            endPoint = new IPEndPoint(_endPoint.Address, clientConnectionPort);
            client = _client;
            state = new UdpState(client, endPoint);
        }
        ~ClientConnection()
        {
            client.Close();
            client.Dispose();
        }
    }
}