using System;
namespace NetCodeAttempt
{
    public class Server
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
                        //TODO: wait for requests.
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
