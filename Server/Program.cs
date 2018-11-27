using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
namespace NetCodeAttempt
{
    public class Program
    {
        public static List<ClientConnection> clients { get; set; }

        static void Main(string[] args)
        {
            var serverConnetionPort = 20000;
            var endPoint = new IPEndPoint(IPAddress.Any, serverConnetionPort);
            var client = new UdpClient(endPoint);
            var state = new UdpState
            (
                new UdpClient(endPoint),
                new IPEndPoint(IPAddress.Any, serverConnetionPort)
            );

            try
            {
                client.BeginReceive(new AsyncCallback(ReceiveConnectRequest), state);
                Console.WriteLine("Press ESC to stop the server");
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                {
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
                client.Dispose();
            }
        }

        public static void ReceiveConnectRequest(IAsyncResult ar)
        {
            var client = (UdpClient)((UdpState)(ar.AsyncState)).client;
            var endPoint = (IPEndPoint)((UdpState)(ar.AsyncState)).endPoint;
            var state = new UdpState
            (
                (UdpClient)((UdpState)(ar.AsyncState)).client,
                (IPEndPoint)((UdpState)(ar.AsyncState)).endPoint
            );

            var receiveBytes = client.EndReceive(ar, ref endPoint);
            var receiveString = Encoding.ASCII.GetString(receiveBytes);

            if (receiveString == "connect")
            {
                Console.WriteLine("Client Connected.");
                clients.Add(new ClientConnection(client, endPoint));
            }

            client.BeginReceive(new AsyncCallback(ReceiveConnectRequest), state);
        }
    }
}
