using System;
using System.Net;
using System.Net.Sockets;
namespace NetCodeAttempt
{
    public struct UdpState
    {
        public UdpState(UdpClient _client, IPEndPoint _endPoint)
        {
            client = _client;
            endPoint = _endPoint;
        }
        public UdpClient client;
        public IPEndPoint endPoint;
    }
}