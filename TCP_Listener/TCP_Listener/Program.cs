using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using TCP_Listener.ConstVariables;
using TCP_Listener.Models;

namespace TCP_Listener
 {
     class Program
     {
         static TcpListener _listener;

         static void Main(string[] args)
         {
             AddServerAdmin();

             if (!File.Exists(Directory.GetCurrentDirectory() + @"\ClientData.json"))
                 using (FileStream fileStream = 
                     new FileStream(Directory.GetCurrentDirectory() + @"\ClientData.json", FileMode.Create)) { }

             try
             {
                 _listener = new TcpListener(Pathes.ServerIp, Pathes.ServerPort);
                 _listener.Start();
                 Console.WriteLine("Waiting for connection...");
 
                 while(true)
                 {
                     TcpClient client = _listener.AcceptTcpClient();
                     ClientObject clientObject = new ClientObject(client);

                     AllClients.Instance.ConnectClient(clientObject).Wait();
                     
                     Thread clientThread = new Thread(clientObject.GetMessage);
                     clientThread.Start();
                 }
             }
             catch(Exception ex)
             {
                 Console.WriteLine(ex.Message);
             }
             finally
             {
                 if(_listener!=null)
                     _listener.Stop();
             }
         }
        //"One or more errors occurred. (The process cannot access the file 'C:\\Users\\Админ\\Desktop\\ChatPack\\TCP_Listener\\TCP_Listener\\bin\\Debug\\netcoreapp2.1\\ClientData.json' because it is being used by another process.)"
        //		Message	"The process cannot access the file 'C:\\Users\\Админ\\Desktop\\ChatPack\\TCP_Listener\\TCP_Listener\\bin\\Debug\\netcoreapp2.1\\ClientData.json' because it is being used by another process."	string

        private static void AddServerAdmin()
         {
                 List<Client> clients = new List<Client>()
                 {
                     new Client()
                     {
                         Name = "Server Bot",
                         BackgroundMessageColor = "#334357",
                         RemoteEndPoint = "Server Bot",
                         NameColor = "#6AB3F3",
                         TextColor = "#eae9f0"
                     }
                 };
           
             string jsonData = JsonConvert.SerializeObject(clients);

             File.WriteAllText(Pathes.FilePath, jsonData);

             Console.WriteLine("Added new client in file\n");
        }
     }
 }