using System.IO;
using System.Net;

namespace TCP_Listener.ConstVariables
{
    public class Pathes
    {
        public static int ServerPort { get; } = 8888;
        public static IPAddress ServerIp { get; } = IPAddress.Any;
        public static string FilePath = Directory.GetCurrentDirectory() + @"/ClientData.json";
    }
}