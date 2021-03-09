using Newtonsoft.Json;

using System.Diagnostics;

namespace webSocketSharp
{
    //JSON deserialized class for the Hololens rest API
    //ws://127.0.0.1:10080/ext/perception/client?clientmode=passive

    public class PerceptionClient
    {
        public int TrackingState { get; set; }
        public cCamera Camera { get; set; }
        public cControllerData ControllerData { get; set; }
        public cLSRPlane LSRPlane { get; set; }
        public float[] HeadToAttachedFor { get; set; }
        public float[] OriginToAttachedFor { get; set; }
    }

    public class cControllerData
    {

    }

    public class cFrustum
    {
        public float[] Bottom { get; set; }
        public float[] Far { get; set; }
        public float[] Left { get; set; }
        public float[] Near { get; set; }
        public float[] Right { get; set; }
        public float[] Top { get; set; }
    }

    public class cDisplay
    {
        public cFrustum Frustum { get; set; }
        public float[] FocusPointDisplay { get; set; }
        public float[] FocusPointNormalDisplay { get; set; }
        public float[] Projection { get; set; }
        public float[] View { get; set; }
    }

    public class cCamera
    {
        public cDisplay[] Displays { get; set; }
    }

    public class cLSRPlane
    {
        public float distance { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    public class PerceptionClientJsonTest
    {
        public PerceptionClientJsonTest()
        {
            string demoJson = "{  'TrackingState': 3,  'Camera': {  'Displays': [{  'Frustum': {  'Bottom': [-0.051801, -0.938592, 0.341118, 0.096521],  'Far': [0.161880, -0.108995, -0.980773, -14.932893],  'Left': [-0.978711, 0.035061, 0.202229, -0.001346],  'Near': [-0.161880, 0.108995, 0.980773, 0.229674],  'Right': [0.852517, 0.045374, 0.520726, 0.059935],  'Top': [-0.027835, 0.989556, 0.141437, -0.057139]  },  'FocusPointDisplay': [0.027431, -0.055531, 0.681227],  'FocusPointNormalDisplay': [0.027496, 0.018920, -0.999443],  'Projection': [2.517434, 0.000000, 0.000000, 0.000000, 0.000000, 3.942319, 0.000000, 0.000000, 0.026739, 0.022574, -1.010101, -1.000000, 0.000000, 0.000000, -0.151515, 0.000000],  'View': [0.986714, 0.013233, -0.161879, 0.000000, 0.004548, 0.994024, 0.108994, 0.000000, 0.162359, -0.108279, 0.980768, 0.000000, 0.032239, -0.079692, 0.079658, 1.000000]  }, {  'Frustum': {  'Bottom': [-0.038760, -0.936097, 0.349601, 0.097126],  'Far': [0.151884, -0.110772, -0.982171, -14.933148],  'Left': [-0.972330, 0.027587, 0.231978, 0.060137],  'Near': [-0.151884, 0.110772, 0.982171, 0.230036],  'Right': [0.866618, 0.054504, 0.495987, -0.001130],  'Top': [-0.036070, 0.990279, 0.134340, -0.057670]  },  'FocusPointDisplay': [0.027431, -0.055531, 0.681227],  'FocusPointNormalDisplay': [0.027496, 0.018920, -0.999443],  'Projection': [2.510000, 0.000000, 0.000000, 0.000000, 0.000000, 3.934346, 0.000000, 0.000000, -0.026656, 0.003352, -1.010101, -1.000000, 0.000000, 0.000000, -0.151515, 0.000000],  'View': [0.988389, 0.001509, -0.151883, 0.000000, 0.015503, 0.993717, 0.110772, 0.000000, 0.151101, -0.111838, 0.982167, 0.000000, -0.032242, -0.079922, 0.080020, 1.000000]  }  ]  },  'ControllerData': {},  'LSRPlane': {  'distance': 0.681144,  'x': 0.027496,  'y': 0.018920,  'z': -0.999443  },  'HeadToAttachedFor': [0.987598, 0.010029, 0.156735, 0.000000, -0.005502, 0.999565, -0.029288, 0.000000, -0.156963, 0.028058, 0.987211, 0.000000, 0.013141, 0.070723, -0.087326, 1.000000],  'OriginToAttachedFor': [1.000005, 0.000028, 0.000374, 0.000000, -0.000027, 1.000005, -0.000055, 0.000000, -0.000376, 0.000053, 1.000003, 0.000000, 0.033230, -0.006217, 0.002964, 1.000000] } ";
            PerceptionClient pc = JsonConvert.DeserializeObject<PerceptionClient>(demoJson);

            Debug.WriteLine(pc.TrackingState);
        }
    }
}
