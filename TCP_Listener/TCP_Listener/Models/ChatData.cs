using System;

namespace TCP_Listener.Models
{
    using System.Collections.Generic;

    namespace TPC_Chat.Models
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
            
            public override string ToString()
            {
                string returnData = String.Empty;
                foreach (var message in Messages)
                {
                    returnData +=$"{message.Name} \n" +
                                 $"{message.NameColor}\n" +
                                 $"{message.Text}\n" +
                                 $"{message.TextColor}\n" +
                                 $"{message.BackgroundMessageColor}  \n" +
                                 $"{message.IP}\n\n";
                }

                return returnData;
            }
        }
        
        
    }
}