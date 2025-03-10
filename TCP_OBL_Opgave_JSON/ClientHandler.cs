using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TCP_OBL_Server_JSON
{
    public class ClientHandler
    {
        public static void HandleClient(TcpClient tcpClient)
        {
            Console.WriteLine($"Client connected: {tcpClient.Client.RemoteEndPoint}");
            NetworkStream ns = tcpClient.GetStream();

            using (StreamReader reader = new StreamReader(ns))
            using (StreamWriter writer = new StreamWriter(ns) { AutoFlush = true })
            {
                bool isRunning = true;

                while (isRunning)
                {
                    string? msg = reader.ReadLine();
                    
                    Json_Obj json_Obj = JsonSerializer.Deserialize<Json_Obj>(msg);
                    if (json_Obj.Method == "Random")
                    {
                        Random random = new Random();
                        writer.WriteLine($"{random.Next(json_Obj.Num1, json_Obj.Num2)}");


                    }
                    else if (json_Obj.Method == "Add")
                    {
                        writer.WriteLine($"{json_Obj.Num1 + json_Obj.Num2}");
                    }
                    else if (json_Obj.Method == "Subtract")
                    {
                        
                        writer.WriteLine($"{json_Obj.Num1 -json_Obj.Num2 }");
                    }
                    else if (json_Obj.Method == "close")
                    {
                        isRunning = false;
                        writer.WriteLine("Connection closed.");
                    }
                    else
                    {
                        writer.WriteLine("Unknown command.");
                    }
                }
            }
            tcpClient.Close();
        }
    }
}
