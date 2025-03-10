using System;

using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace TCP_OBL_Client_JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientTcp.StartClient();
        }
    }
}
