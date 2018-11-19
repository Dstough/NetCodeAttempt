using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace NetCodeAttempt
{
    public class Program
    {

        public struct UdpState
        {
            public UdpClient client;
            public IPEndPoint endPoint;
        }
        static void Main(string[] args)
        {
            try
            {
                var serverPort = 20000;

                var endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                var client = new UdpClient(endPoint);
                var state = new UdpState();
                state.endPoint = endPoint;
                state.client = client;

                client.BeginReceive(new AsyncCallback(ReceiveCallback), state);

                Console.WriteLine("Press ESC to stop");
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                {
                }
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ReceiveCallback(IAsyncResult ar)
        {
            var u = (UdpClient)((UdpState)(ar.AsyncState)).client;
            var e = (IPEndPoint)((UdpState)(ar.AsyncState)).endPoint;

            var receiveBytes = u.EndReceive(ar, ref e);
            var receiveString = Encoding.ASCII.GetString(receiveBytes);

            Console.WriteLine("Received: {0}", receiveString);
        }
    }
}
