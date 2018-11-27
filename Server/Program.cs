using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
namespace NetCodeAttempt
{
    public class Program
    {
        const int serverPort = 20000;
        const int clientPort = 20001;
        public static List<IPEndPoint> clients { get; set; }

        static void Main(string[] args)
        {
            var endPoint = new IPEndPoint(IPAddress.Any, serverPort);
            var client = new UdpClient(endPoint);
            var state = new UdpState
            (
                client,
                endPoint
            );

            try
            {
                clients = new List<IPEndPoint>();
                client.BeginReceive(new AsyncCallback(ReceiveMessage), state);
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

        public static void ReceiveMessage(IAsyncResult ar)
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
                clients.Add(new IPEndPoint(endPoint.Address, clientPort));
                Console.WriteLine("Client Connected.");
            }
            else
            {
                //TODO: Spin up a thread to do this so the server doesn't hang.
                Console.WriteLine("Client Sent: " + receiveString);
                foreach(var clientEndPoint in clients)
                {
                    client.SendAsync(receiveBytes, receiveBytes.Length, clientEndPoint);
                }
            }
            client.BeginReceive(new AsyncCallback(ReceiveMessage), state);
        }
    }
}
