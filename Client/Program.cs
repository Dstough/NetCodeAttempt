using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace NetCodeAttempt
{
    public class Program
    {
        const int serverPort = 20000;
        const int clientPort = 20001;
        
        const string connectRequest = "connect";
        const string disconnectMessage = "disconnect";
        const string message = "Hello World";

        static void Main(string[] args)
        {
            var serverAddress = "localhost";
            var myEndPoint = new IPEndPoint(IPAddress.Any, clientPort);
            var client = new UdpClient(myEndPoint);

            try
            {
                Console.WriteLine("Press ESC to stop");
                while (true)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    else if (key == ConsoleKey.C)
                    {
                        client.Send(Encoding.ASCII.GetBytes(connectRequest), connectRequest.Length, serverAddress, serverPort);
                        client.BeginReceive(new AsyncCallback(ReceiveMessage), new UdpState(client, myEndPoint));
                    }
                    else if (key == ConsoleKey.M)
                    {
                        client.Send(Encoding.ASCII.GetBytes(message), message.Length, serverAddress, serverPort);
                    }
                    else if (key == ConsoleKey.X)
                    {
                        client.Send(Encoding.ASCII.GetBytes(disconnectMessage), disconnectMessage.Length, serverAddress, serverPort);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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

            Console.WriteLine("Server Says: " + receiveString);
            client.BeginReceive(new AsyncCallback(ReceiveMessage), state);
        }
    }
}
