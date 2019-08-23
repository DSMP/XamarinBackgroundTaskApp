using AlarmApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlarmApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CounterPage : ContentPage
    {
        public CounterPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<CounterMessageModel>(this, "TickedMessage", alarmMessage =>
             {
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     label.Text = alarmMessage.Seconds.ToString();
                 }); 
             });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var message = new StartLongRunningTaskMessage();
            MessagingCenter.Send(message, "StartLongRunningTaskMessage");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var message = new StopLongRunningTaskMessage();
            MessagingCenter.Send(message, "StopLongRunningTaskMessage");
        }
    }
}