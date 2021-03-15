using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace webSocketSharp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private DeviceSocket deviceSocket = null; //associated websocket call for API data
        private DispatcherTimer timer = null; // timer to update UI with head pos/orientation

        //connect to hololens via portal
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (deviceSocket != null)
                deviceSocket.Close();

            deviceSocket = new DeviceSocket(txtUserName.Text, txtPassword.Text, txtAddressPort.Text);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        //update head position display on UI
        private void Timer_Tick(object sender, object args)
        {
            HeadMatrix.Text = deviceSocket.Result;
        }

        //disconnect websocket from hololens
        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            if (deviceSocket != null)
                deviceSocket.Close();
            timer.Stop();
        }
    }
}
