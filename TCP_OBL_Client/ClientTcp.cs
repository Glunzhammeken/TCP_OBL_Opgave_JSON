using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCP_OBL_Client_JSON
{
    public class ClientTcp
    {
        public static void StartClient()
        {
            string serverIp = "127.0.0.1";
            int port = 5000;

            try
            {
                using (TcpClient client = new TcpClient(serverIp, port))
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
                {
                    Console.WriteLine("Connected to server!");

                    bool isRunning = true;
                    
                    while (isRunning)
                    {
                        Console.WriteLine("Enter command (Random, Add, Subtract, close): ");
                        string command = Console.ReadLine();
                        

                        if (command == "close")
                        {
                            isRunning = false;
                        }
                        else if (command == "Random" || command == "Add" || command == "Subtract")
                        {
                            Console.WriteLine("Enter two numbers separated by space: ");
                            string[] parts = Console.ReadLine().Split(' ');
                            Json_Obj jsonObj = new Json_Obj { Method = command, Num1 = int.Parse(parts[0]), Num2 = int.Parse(parts[1]) };
                            string jsonString = JsonSerializer.Serialize(jsonObj);
                            writer.WriteLine(jsonString);
                            string msg = reader.ReadLine();

                            Json_Obj json_Obj = JsonSerializer.Deserialize<Json_Obj>(msg);
                            Console.WriteLine(json_Obj.Response);

                        }
                        else
                        {
                            Console.WriteLine("Unknown command.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("Client closed.");
        }
    }
}
