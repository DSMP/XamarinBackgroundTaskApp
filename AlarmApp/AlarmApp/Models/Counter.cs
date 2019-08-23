using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlarmApp.Models
{
    public class Counter
    {
        public async Task RunCounter(CancellationToken token)
        {
            await Task.Run(async () => {

                for (long i = 0; i < long.MaxValue; i++)
                {
                    token.ThrowIfCancellationRequested();

                    await Task.Delay(1000);

                    var message = new CounterMessageModel
                    {
                        Seconds = i
                    };
                    Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send<CounterMessageModel>(message, "TickedMessage");
                    });
                }
            }, token);
        }
    }
}
