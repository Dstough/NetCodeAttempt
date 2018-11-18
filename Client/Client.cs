using System;
using System.Net;
using System.Net.Sockets;
namespace NetCodeAttempt
{
    public class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ESC to stop");
            try
            {
                do
                {
                    while (!Console.KeyAvailable)    
                    {
                    }
                    switch(Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.A:
                            
                        default:break;
                    }
                }
                while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
