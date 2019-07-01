using System;
using Xamarin.Forms;

namespace TPC_Chat.Models
{
    public class OutputMessage
    {
        public string NameColor { get; set; }
        public string TextColor { get; set; }
        public string BackgroundMessageColor { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public LayoutOptions IsItsUser { get; set; }
        public DateTime DateTime { get; set; }
    }
}