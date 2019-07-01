using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TPC_Chat.ConstVariables;
using TPC_Chat.Models;
using Xamarin.Forms;
using LayoutOptions = Xamarin.Forms.LayoutOptions;

namespace TPC_Chat.Repositories
{
    public class Message
    {
        public string Name { get; set; }
        public string NameColor { get; set; }

        public string Text { get; set; }
        public string TextColor { get; set; }

        public string BackgroundMessageColor { get; set; }
        public string IP { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class ChatData
    {
        public List<Message> Messages = new List<Message>();

        [JsonIgnore] private TcpClient _client;
        [JsonIgnore] private NetworkStream _stream;

        public void Connect()
        {
            _client = new TcpClient();
            _client.Connect(ServerAndPort.ServerIp, ServerAndPort.ServerPort);
            _stream = _client.GetStream();
        }

        public async Task WriteMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            await _stream.WriteAsync(Encoding.UTF8.GetBytes(message), 0,
                Encoding.UTF8.GetBytes(message).Length);
            _stream.Flush();
        }

        public async Task GetData(ObservableCollection<OutputMessage> outputMessages, ListView listView)
        {
            while (_client.Connected)
            {
                var data = new byte[512];
                var response = new StringBuilder();

                do
                {
                    var bytes = await _stream.ReadAsync(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                } while (_stream.DataAvailable);

                if (string.IsNullOrEmpty(response.ToString())) continue;

                try
                {
                    var chatData = JsonConvert.DeserializeObject<ChatData>(response.ToString());
                    foreach (var chatDataMessage in chatData.Messages)
                    {
                        OutputMessage outputMessage = FillOutputMessage(chatDataMessage);
                        if (outputMessage == null) break;
                    
                        outputMessages.Add(outputMessage);
                        listView.ScrollTo(outputMessages[outputMessages.Count - 1], ScrollToPosition.End, true);
                    }
                }
                catch (Exception e)
                {
                    //No problemo :)
                }
                
            }
        }

        private OutputMessage FillOutputMessage(Message message)
        {

            if (string.IsNullOrEmpty(message.Text)) return null;
            
            var outputMessage = new OutputMessage()
            {
                Name = message.Name,
                Text = message.Text,
                DateTime = message.DateTime,
                NameColor = message.NameColor,
                BackgroundMessageColor = message.BackgroundMessageColor,
                TextColor = message.TextColor,
                IsItsUser = message.IP.Substring(message.IP.Length - 5) == _client.Client.LocalEndPoint.ToString().Substring(_client.Client.LocalEndPoint.ToString().Length-5)
                    ? LayoutOptions.Start
                    : LayoutOptions.End
            };

            return outputMessage;
        }

        public void Disconnect()
        {
            if (_client == null) return;
            if (!_client.Connected) return;

            _client.Dispose();
        }

        public override string ToString()
        {
            var returnData = string.Empty;
            foreach (var message in Messages)
                returnData += $"{message.Name} \n" +
                              $"{message.NameColor}\n" +
                              $"{message.Text}\n" +
                              $"{message.TextColor}\n" +
                              $"{message.BackgroundMessageColor}  \n" +
                              $"{message.IP}\n\n";

            return returnData;
        }
    }
}