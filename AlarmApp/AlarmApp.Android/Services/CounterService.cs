using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlarmApp.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace AlarmApp.Droid.Services
{
    [Service]
    class CounterService : Service
    {
        private CancellationTokenSource _cts;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            Intent notificationIntent = new Intent(this, typeof (MainActivity));
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, 0);
            Notification notification = new Notification.Builder(this, MainActivity.CHANNEL_ID)
                .SetContentTitle("Counter Service")
                .SetContentText("App is counting now")
                .SetSmallIcon(Resource.Drawable.abc_ic_star_black_16dp)
                .SetContentIntent(pendingIntent)
                .Build();
            try
            {
                StartForeground(10000, notification);
            }
            catch (Exception ex)
            {

                throw;
            }

            Task.Run(() =>
            {
                try
                {
                    //INVOKE THE SHARED CODE
                    var counter = new Counter();
                    counter.RunCounter(_cts.Token).Wait();
                }
                catch (System.OperationCanceledException)
                {
                }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        var message = new CancelledAlaarm();
                        Device.BeginInvokeOnMainThread(
                            () => MessagingCenter.Send(message, "CancelledMessage")
                        );
                    }
                }

            }, _cts.Token);
            return StartCommandResult.Sticky;
        }
        public override void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();

                _cts.Cancel();
            }
            base.OnDestroy();
        }
    }
}