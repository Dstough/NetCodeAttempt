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
                var message = "Hello World";
                var hostName = "localhost";
                var hostPort = 20000;
                var client = new UdpClient();

                client.Connect(hostName, hostPort);

                Console.WriteLine("Press ESC to stop");
                while (Console.ReadKey(true).Key != ConsoleKey.Escape)
                {
                    client.Send(Encoding.ASCII.GetBytes(message), message.Length);
                    Console.WriteLine("Sent Message.");
                }
                client.Close();
                client.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
