using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;
using TPC_Chat.Models;
using TPC_Chat.Repositories;
using Xamarin.Forms;

namespace TPC_Chat
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private ChatData ChatData = new ChatData();
        public string IsConnected { get; set; } = "Chat";
        public ObservableCollection<OutputMessage> OutputMessages { get; set; }

        public MainPage()
        {
            InitializeComponent();
            OutputMessages = new ObservableCollection<OutputMessage>();

            ListView_OnRefreshing(this, null);
            BindingContext = this;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(InputMessage.Text)) return;
                await ChatData.WriteMessage(InputMessage.Text);
                InputMessage.Text = String.Empty;
            }
            catch (Exception exception)
            {
                await DisplayAlert("Error sending message", exception.Message, "Ok");
            }
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            try
            {
                ChatData.Disconnect();
                ChatData.Connect();
                ListView.IsRefreshing = false;
                await ChatData.GetData(OutputMessages, ListView);
            }
            catch (SocketException ex)
            {
                await DisplayAlert("Socket exception", ex.Message, "Ok");
            }
            catch (Exception exception)
            {
                await DisplayAlert("Error", exception.Message, "Ok");
            }
        }
    }
}