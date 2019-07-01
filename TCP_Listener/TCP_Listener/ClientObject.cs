using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCP_Listener.ConstVariables;

namespace TCP_Listener
{
    public class ClientObject
    {
        public TcpClient client;
        private NetworkStream _stream;
        public ClientObject(TcpClient tcpClient)
        {
            client = tcpClient;
            _stream = client.GetStream();
        }

        public async Task SendMessage(string message)
        {
            try
            {
                await _stream.WriteAsync(Encoding.UTF8.GetBytes(message), 0,
                    Encoding.UTF8.GetBytes(message).Length);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error to send message to {client.Client.RemoteEndPoint}." +
                                  $"\nError message: {e.Message}\n");
            }
        }
        
        public async void GetMessage()
        {
            try
            {
                byte[] data = new byte[64]; // буфер для получаемых данных
                while (true)
                {
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = _stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (_stream.DataAvailable);
 
                    //Message
                    string message = builder.ToString();

                    if (await IsSpecialCommand(message))
                    {
                        Console.WriteLine("Special response sent to " + client.Client.RemoteEndPoint);
                        continue;
                    }
                    
                    Console.WriteLine($"Message from {client.Client.RemoteEndPoint}.\n" +
                                      $"The message: {message}\n");

                    await new AllClients().SendMessageToEveryone(message, client.Client.RemoteEndPoint.ToString());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error to send message from {client.Client.RemoteEndPoint} to all." +
                                  $"\nError message: {e.Message}\n");
            }
            finally
            {
                if (!client.Connected)
                {
                    AllClients.Instance.DisconnectClient(this);
                    _stream?.Close();
                    client?.Close();
                }
            }
        }

        private async Task<bool> IsSpecialCommand(string message)
        {
            if (message.Contains("\\RGB"))
            {
                Console.WriteLine("RGB info sent\n");
                await SendMessage(AllClients.Instance.ConvertMessageToJson(StaticPhrases.RGBColorPhrase, "Server Bot"));
                return true;
            }

            if (message.Contains("\\ClrJoke"))
            {
                Console.WriteLine("Color joke sent\n");
                int jokeNum = new Random().Next(0,12);
                await SendMessage(AllClients.Instance.ConvertMessageToJson($"Joke num #{jokeNum}\n" + StaticPhrases.Jokes[jokeNum], "Server Bot"));
                return true;
            }
            
            if (message.Contains("\\chgName") && IsDoubleArgumentMessageOk(message) && !message.Contains("\\chgNameClr"))
            {
                string newName = ReadSecondArgument(message);
                AllClients.Instance.ChangeClientsName(newName,
                    client.Client.RemoteEndPoint.ToString());
                await SendMessage(AllClients.Instance.ConvertMessageToJson($"Your name to {newName} successfully changed!", "Server Bot"));
                return true;
            }
            
            if (message.Contains("\\chgNameClr") && IsDoubleArgumentMessageOk(message))
            {
                string newColor = ReadSecondArgument(message);
                AllClients.Instance.ChangeClientsNameColor(newColor, client.Client.RemoteEndPoint.ToString());
                await SendMessage(AllClients.Instance.ConvertMessageToJson($"Your name color to {newColor} successfully changed!", "Server Bot"));
                return true;
            }
            
            if (message.Contains("\\chgTextClr") && IsDoubleArgumentMessageOk(message))
            {
                string newColor = ReadSecondArgument(message);
                AllClients.Instance.ChangeClientsTextColor(newColor, client.Client.RemoteEndPoint.ToString());
                await SendMessage(AllClients.Instance.ConvertMessageToJson($"Your text color to {newColor} successfully changed!", "Server Bot"));
                return true;
            }
            
            if (message.Contains("\\chgBackgClr") && IsDoubleArgumentMessageOk(message))
            {
                string newColor = ReadSecondArgument(message);
                AllClients.Instance.ChangeClientsMessageBackgroundColor(newColor, client.Client.RemoteEndPoint.ToString());
                await SendMessage(AllClients.Instance.ConvertMessageToJson($"Your message background color to {newColor} successfully changed!", "Server Bot"));
                return true;
            }

            if (message.Contains("\\rnd"))
            {
                string nameColor = GetRandomHexColorValue();
                string textColor = GetRandomHexColorValue();
                string messageBackgroundColor = GetRandomHexColorValue();
                AllClients.Instance.ChangeClientsSuit(nameColor, textColor, messageBackgroundColor,
                    client.Client.RemoteEndPoint.ToString());
                await SendMessage(AllClients.Instance.ConvertMessageToJson($"Your random suit is ready!\nNameColor: {nameColor}\nTextColor: {textColor}\n BackgroundMessageColor: {messageBackgroundColor}", "Server Bot"));
                return true;
            }

            if (message.Contains("\\help"))
            {
                await SendMessage(AllClients.Instance.ConvertMessageToJson(StaticPhrases.WelcomePhrase, "Server Bot"));
                return true;
            }

            if (message.Contains("\\info"))
            {
                await SendMessage(AllClients.Instance.ConvertMessageToJson(AllClients.Instance.GetInfo(), "Server Bot"));
                return true;
            }

            return false;
        }

        private string GetRandomHexColorValue()
        {
            Random rnd = new Random();
            Byte[] b = new Byte[3];
            rnd.NextBytes(b);
            Color c = Color.FromArgb(b[0],b[1],b[2]);
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
        
        private bool IsDoubleArgumentMessageOk(string message)
        {
            return message.Length < 25 && message.Contains("[") && message.Contains("]");
        }

        private string ReadSecondArgument(string message)
        {
            string returnValue = String.Empty;

            for (int i = 0; i < message.Length - 1; i++)
            {
                if (message[i+1] == '[')
                {
                    for (int j = i+2; j < message.Length; j++)
                    {
                        returnValue += message[j];
                        if (message[j + 1] == ']') break;
                    }
                }
            }

            return returnValue;
        }
        
        //"\\chgName [Name ++
        //"e.x. \\chgName 
        //" = = = = = = \n
        //"\\chgNameClr [# ++
        //"e.x. \\chgNameC
        //" = = = = = = \n
        //"\\chgTextClr [# ++
        //"e.x. \\chgTextC
        //" = = = = = = \n
        //"\\chgBackgClr [ ++
        //"e.x. \\chgBackg
        //" = = = = = = \n
        //"\\RGB - Gives s ++
        //" = = = = = = \n
        //"\\ClrJoke - Rea ++
        //\\help ++
    }
}
