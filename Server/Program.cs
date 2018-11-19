using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace NetCodeAttempt
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var serverPort = 20000;
                var server = new UdpClient(serverPort);
                server.BeginReceive(DataRecieved, server);
                Console.WriteLine("Press ESC to stop");
                do
                {
                    while (!Console.KeyAvailable)
                    {
                    }
                }
                while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
 
        private static void DataRecieved(IAsyncResult result)
        {
            var client = (UdpClient)result.AsyncState;
            var receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            var receivedBytes = client.EndReceive(result, ref receivedIpEndPoint);
            var receivedText = ASCIIEncoding.ASCII.GetString(receivedBytes);

            Console.Write(receivedIpEndPoint + ": " + receivedText + Environment.NewLine);
        }
    }
}
