using System;
using System.Diagnostics;
using System.Text;

using WebSocketSharp; //warning cuz the official version is prerelease
using Newtonsoft.Json;


namespace webSocketSharp
{
    class DeviceSocket
    {
        WebSocket webSocket = null;

 
        public string Head { get; set; }
        public string Origin { get; set; }


        //websocket to get head and orientation position from hololens
        public DeviceSocket()
        {

            // rest call for the head and origin camera position and orientation.
            // notably different from the documented API (/api/holographic/perception/client)
            webSocket = new WebSocket("ws://127.0.0.1:10080/ext/perception/client?clientmode=passive");

            //need to specify origin, which would be the home page?
            webSocket.Origin = "http://127.0.0.1:10080";

            bool preAuth = true;
            webSocket.SetCredentials("rbrown", "ARL 300456", preAuth);

            webSocket.OnError += (sender, e) =>
            {
                Debug.WriteLine("Error: " + e.Message);
            };

            webSocket.OnMessage += (sender, e) =>
            {
                PerceptionClient perceptionClient = JsonConvert.DeserializeObject<PerceptionClient>(e.Data);

                int index = 0;
                StringBuilder sbO = new StringBuilder();
                StringBuilder sbH = new StringBuilder();

                //OriginToAttachedFor -> JSON tag for the 4x4 transformation matrix of the origin
                //HeadToAttachedFor -> same, for the head.
                //not sure of the difference really

                //last row of origin is the position vector
                sbO.Append("Origin position\n\n");
                for (int i = 3; i<4; i++)
                {
                    for (int j = 0; j<3; j++)
                    {
                        sbO.Append(string.Format("{0,8:###.00000}", perceptionClient.OriginToAttachedFor[index]) );

                        index++;
                    }
                    sbO.Append("\n");
                }
                Origin = sbO.ToString();

                //convert the 4x4 transformation matrix to quaternion (ignore position data)
                float[] HeadQuat = transformToQuaternion(perceptionClient.HeadToAttachedFor);
                
                sbH.Append("Head Rotation Quaternion\n\n");

                for (int i = 0; i < 4; i++)
                {
                    sbH.Append(string.Format("{0,8:###.00000}", HeadQuat[i]));
                }

                Head = sbH.ToString();

            };

            webSocket.OnClose += (sender, e) => 
            {
                Debug.WriteLine("Closed: " + e.Reason);
            };

            webSocket.Connect();

        }

        public void Close()
        {
            webSocket.Close();
        }

        private float[] transformToQuaternion(float[] m)
        {
            double qw = Math.Sqrt(0.25 * (1.0 + m[0] + m[5] + m[10]));

            double qx = (m[9] - m[6]) * (0.24 / qw);
            double qy = (m[2] - m[8]) * (0.24 / qw);
            double qz = (m[4] - m[1]) * (0.24 / qw);
            return new float[] { (float)qw, (float)qx, (float)qy, (float)qz };
        }
    }

}
