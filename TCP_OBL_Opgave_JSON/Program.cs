    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
using TCP_OBL_Server_JSON;

namespace TCP_Obl_Opgave
{
    class Program
    {

        static int port = 5000;

        static async Task Main(string[] args)
        {

            Task serverTask = Task.Run(() => StartServer());


            await serverTask;
        }

        static async Task StartServer()
        {
            Console.WriteLine("Starting TCP server...");
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine($"Server is running on port {port}...");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected!");
                Task.Run(() => ClientHandler.HandleClient(client));
            }
        }
    }
}
