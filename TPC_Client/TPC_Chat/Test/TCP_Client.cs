using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TPC_Chat.Models;
using TPC_Chat.Repositories;

namespace TPC_Chat.Test
{
    public class TCP_Client
    {
        private const int port = 8888;
        private const string server = "127.0.0.1";

        public static string Connect()
        {
            try
            {
                var client = new TcpClient();
                client.Connect(server, port);

                var data = new byte[256];
                var response = new StringBuilder();
                var stream = client.GetStream();

                do
                {
                    var bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                } while (stream.DataAvailable); // пока данные есть в потоке

                var chatData = JsonConvert.DeserializeObject<ChatData>(response.ToString());

                // Закрываем потоки
                stream.Close();
                client.Close();

                return chatData.ToString();
            }
            catch (SocketException e)
            {
                return $"SocketException: {e}";
            }
            catch (Exception e)
            {
                return $"Exception: {e.Message}";
            }
        }
    }
}