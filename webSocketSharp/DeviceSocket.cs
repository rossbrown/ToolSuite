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

 
        public string Result { get; set; }
        
        public string IP = "127.0.0.1:10080";
        //public string IP = "192.172.0.22";

        public string userName = "rbrown";
        public string password = "ARL 300456";

        public string positionCommand = "/ext/perception/client?clientmode=passive";

        public DeviceSocket(string userName, string password, string IP)
        {
            this.userName = userName;
            this.password = password;
            this.IP = IP;

            Result = "";
            // rest call for the head and origin camera position and orientation.
            // notably different from the documented API (/api/holographic/perception/client)
            webSocket = new WebSocket("ws://" + IP + positionCommand); 

            //need to specify origin, which would be the home page?
            webSocket.Origin = "http://" + IP;

            bool preAuth = true;
            webSocket.SetCredentials(userName, password, preAuth);

            webSocket.OnError += (sender, e) =>
            {
                Debug.WriteLine("Error: " + e.Message);
                Result = "Error: " + e.Message;
            };

            webSocket.OnMessage += (sender, e) =>
            {
                PerceptionClient perceptionClient = JsonConvert.DeserializeObject<PerceptionClient>(e.Data);

                int index = 0;
                StringBuilder results = new StringBuilder();

                //OriginToAttachedFor -> JSON tag for the 4x4 transformation matrix of the origin
                //HeadToAttachedFor -> same, for the head.
                //not sure of the difference really

                //last row of origin is the position vector
                results.Append("Origin position\n\n");
                for (int i = 3; i<4; i++)
                {
                    for (int j = 0; j<3; j++)
                    {
                        results.Append(string.Format("{0,8:###.00000}", perceptionClient.OriginToAttachedFor[index]) );

                        index++;
                    }
                    results.Append("\n");
                }

                results.Append("\n");

                //convert the 4x4 transformation matrix to quaternion (ignore position data)
                float[] HeadQuat = transformToQuaternion(perceptionClient.HeadToAttachedFor);

                results.Append("Head Rotation Quaternion\n\n");

                for (int i = 0; i < 4; i++)
                {
                    results.Append(string.Format("{0,8:###.00000}", HeadQuat[i]));
                }

                Result = results.ToString();

            };

            webSocket.OnClose += (sender, e) => 
            {
                Debug.WriteLine("Closed: " + e.Reason);
                Result = "Closed: " + e.Reason;
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
