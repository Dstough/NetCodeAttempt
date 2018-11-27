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
            public UdpState(UdpClient _client, IPEndPoint _endPoint)
            {
                client = _client;
                endPoint = _endPoint;
            }
            public UdpClient client;
            public IPEndPoint endPoint;
        }
        static void Main(string[] args)
        {
            try
            {
                var serverPort = 20000;
                var state = new UdpState
                (
                    new IPEndPoint(IPAddress.Any, serverPort),
                    new UdpClient(endPoint)
                );
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
            finally
            {
                client.Dispose();
            }
        }

        public static void ReceiveCallback(IAsyncResult ar)
        {
            var state = new UdpState
            (
                (UdpClient)((UdpState)(ar.AsyncState)).client,
                (IPEndPoint)((UdpState)(ar.AsyncState)).endPoint
            );
            state.endPoint = endPoint;
            state.client = client;

            var receiveBytes = client.EndReceive(ar, ref endPoint);
            var receiveString = Encoding.ASCII.GetString(receiveBytes);

            Console.WriteLine("Received: {0}", receiveString);
            client.BeginReceive(new AsyncCallback(ReceiveCallback), state);
        }
    }
}
