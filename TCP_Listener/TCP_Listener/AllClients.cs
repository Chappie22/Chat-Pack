using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TCP_Listener.ConstVariables;
using TCP_Listener.Models;
using TCP_Listener.Models.TPC_Chat.Models;

namespace TCP_Listener
{
    public class AllClients
    {
        public static readonly AllClients ClassClients = new AllClients();
        public static  AllClients Instance { get { return ClassClients; } }
        
        private List<ClientObject> ClientObjects = new List<ClientObject>();

        public async Task ConnectClient(ClientObject clientObject)
        {
            Console.WriteLine($"Client {clientObject.client.Client.RemoteEndPoint} connected to server.\n");
            if (ClientObjects.Count >= 1)
                await SendMessageToEveryone($"{clientObject.client.Client.RemoteEndPoint} entered the chat room!\n" +
                                            $"There are {ClientObjects.Count + 1} member(s) in chat.", "Server Bot");
            ClientObjects.Add(clientObject);
            AddClientInDb(clientObject);
            await clientObject.SendMessage(ConvertMessageToJson(StaticPhrases.WelcomePhrase, "Server Bot"));
        }
        
        public async void DisconnectClient(ClientObject clientObject)
        {
            Console.WriteLine($"Client {clientObject.client.Client.RemoteEndPoint} disconnected from server.\n");
            ClientObjects.Remove(clientObject);
            if (ClientObjects.Count >= 1)
                await SendMessageToEveryone($"{clientObject.client.Client.RemoteEndPoint} left the chat room.\n" +
                                          $"There are {ClientObjects.Count} member(s) in chat.", "Server Bot");
        }

        public string ConvertMessageToJson(string messageData, string senderRemoteEndPoint)
        {
            Message message = new Message()
            {
                DateTime = DateTime.Now,
                IP = senderRemoteEndPoint,
                Text = messageData
            };

            Client client = IsClientExists(senderRemoteEndPoint);
            
            if (client != null)
            {
                message.Name = client.Name;
                message.NameColor = client.NameColor;
                message.TextColor = client.TextColor;
                message.BackgroundMessageColor = client.BackgroundMessageColor;
            }
            else
            {
                message.Name = message.IP;
                message.NameColor = "#469bd4";
                message.TextColor = "#31d4b8";
                message.BackgroundMessageColor = "#bd771c";
            }
            
            try
            {
                return JsonConvert.SerializeObject(new ChatData(){Messages = new List<Message>(){message}});
            }
            catch (Exception e)
            {
                Console.WriteLine("Convert message error: \n");
                Console.WriteLine(e);
                return String.Empty;
            }
        }

        public async Task SendMessageToEveryone(string messageData, string senderRemoteEndPoint)
        {
            string chatDataString = ConvertMessageToJson(messageData, senderRemoteEndPoint);
            
            Console.WriteLine($"Message {messageData} sent to:");
                
            foreach (var tcpClient in Instance.ClientObjects)
            {
                Console.WriteLine("-> " + tcpClient.client.Client.RemoteEndPoint);
                await tcpClient.SendMessage(chatDataString);
            }
        }

        private Client IsClientExists(string senderRemoteEndPoint)
        {
            string data = File.ReadAllText(Pathes.FilePath);
                
            if (string.IsNullOrEmpty(data)) return null;
            
            List<Client> clients =
                JsonConvert.DeserializeObject<List<Client>>(data);

            foreach (var client in clients)
            {
                if (client.RemoteEndPoint == senderRemoteEndPoint) return client;
            }

            return null;
        }

        private void AddClientInDb(ClientObject clientObject)
        {
            string remoteEndPoint = clientObject.client.Client.RemoteEndPoint.ToString();
            if (IsClientExists(remoteEndPoint) != null) return;
            
            Client client = new Client()
            {
                Name = remoteEndPoint,
                BackgroundMessageColor = "#9510c2",
                RemoteEndPoint = remoteEndPoint,
                NameColor = "#f2f2f2",
                TextColor = "#e3c68f"
            };
            
        
                List<Client> clients =  JsonConvert.DeserializeObject<List<Client>>(File.ReadAllText(Pathes.FilePath));
                clients.Add(client);
                AddClientsInFile(clients);
           
        }

        public void ChangeClientsSuit(string nameColor, string textColor, string backgroundMessageColor, string remoteEndPoint)
        {
            Client client = IsClientExists(remoteEndPoint);
            client.NameColor = nameColor;
            client.TextColor = textColor;
            client.BackgroundMessageColor = backgroundMessageColor;
            UpdateClient(client);
            Console.WriteLine($"Client {remoteEndPoint} changed his suit:\n NameColor: {nameColor}\n TextColor: {textColor}\n BackgroundMessageColor: {backgroundMessageColor}\n");
        }

        public void ChangeClientsName(string name, string remoteEndPoint)
        {
            Client client = IsClientExists(remoteEndPoint);
            client.Name = name;
            UpdateClient(client);
            Console.WriteLine($"Client {remoteEndPoint} changed his name to {name}\n");
        }
        
        public void ChangeClientsNameColor(string color, string remoteEndPoint)
        {
            Client client = IsClientExists(remoteEndPoint);
            client.NameColor = color;
            UpdateClient(client);
            Console.WriteLine($"Client {remoteEndPoint} changed his name color to {color}\n");
        }
        
        public void ChangeClientsMessageBackgroundColor(string color, string remoteEndPoint)
        {
            Client client = IsClientExists(remoteEndPoint);
            client.BackgroundMessageColor = color;
            UpdateClient(client);
            Console.WriteLine($"Client {remoteEndPoint} changed his message background color to {color}\n");
        }
        
        public void ChangeClientsTextColor(string color, string remoteEndPoint)
        {
            Client client = IsClientExists(remoteEndPoint);
            client.TextColor = color;
            UpdateClient(client);
            Console.WriteLine($"Client {remoteEndPoint} changed his text color to {color}\n");
        }

        public string GetInfo()
        {
            string returnValue = $" = = {ClientObjects.Count} member(s) = = \n";
            for (int i = 0; i < ClientObjects.Count; i++)
            {
                returnValue += $"{i+1} -> {ClientObjects[i].client.Client.RemoteEndPoint}\n";
            }

            return returnValue;
        }

        private void UpdateClient(Client client)
        {
            List<Client> clients =
                JsonConvert.DeserializeObject<List<Client>>(File.ReadAllText(Pathes.FilePath));

            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].RemoteEndPoint == client.RemoteEndPoint)
                {
                    clients.Remove(clients[i]);
                    clients.Add(client);
                }
            }
            
            AddClientsInFile(clients);
        }
        
        private void AddClientsInFile(List<Client> clients)
        {
            string jsonData = JsonConvert.SerializeObject(clients);
            File.WriteAllText(Pathes.FilePath, jsonData);
            Console.WriteLine("Added new client in file\n");
        }
    }
}