/* 
 * @brief ARSEA Project
 * @author Miquel Massot Campos, Francisco Bonin Font
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

using UnityEngine;
using System.Collections;
using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using PointCloud;
using ROSBridgeLib.auv_msgs;
using ROSBridgeLib.geometry_msgs;
using ROSBridgeLib.std_msgs;



public class ArseaViewer : MonoBehaviour {
   public string ROS_IP = "192.168.1.115";
   // public string ROS_IP = "172.22.38.119";
    public int port = 9090;
    public Transform robotToCamera;
    public Material matVertex;
    public GameObject pointCloudContainer;
    private ROSBridgeWebSocketConnection ros = null;
    public bool motors_dissabled;

    void Start() {
        // Set ROS
        Debug.Log("<color=green>INFO:</color> Connecting to " + ROS_IP + "...");
        ros = new ROSBridgeWebSocketConnection("ws://" + ROS_IP, port);
        Global_Variables.stop_motors = false;
        // Define ROS Subscribers and Publishers
        ros.AddSubscriber(typeof(RobotNavSts)); // add subscribers as defined in the corresponding classes
        ros.AddSubscriber(typeof(RobotPointCloud2));
        ros.AddSubscriber(typeof(RobotImage));
        ros.AddPublisher(typeof(RobotBodyVelocityRequest));
       // ros.AddPublisher(typeof());
        ros.Connect();

        // Define Services and clients if any
    }

    // extremely important to disconnect from ROS. Otherwise packets continue to flow
    void OnApplicationQuit() {
        if(ros!=null) ros.Disconnect();
    }
  
    // Update is called once per frame in Unity
    void Update() {
        GameObject OVRtouchcontroller = GameObject.Find("OVR_touch_controller");
        // OVRtouchcontroller.
        // fill in the body velocity request message:
        // carregar un msg tipus body velocity request amb els continugts de Global Variables
        double vx = Global_Variables.vel_linear_x;
        double vy = Global_Variables.vel_linear_y;
        double vz = Global_Variables.vel_linear_z;

        // HeaderMsg header, GoalDescriptorMsg goal, TwistMsg twist, auv_msgs.Bool6AxisMsg disable_axis
        // twist for the boby velocity request: linear + angular speeds
        TwistMsg twist = new TwistMsg(new Vector3Msg(vx,vy,vz), new Vector3Msg(0.0, 0.0,0.0));
        Bool6AxisMsg dissable_axis = new Bool6AxisMsg(false,false,false,false,false,false); // dissable axis field for the body velocity request
        string requester = "/teleoperation";
        GoalDescriptorMsg goal = new GoalDescriptorMsg(requester, 0, 60); // id= 0, priority=60
        // header: int seq, TimeMsg stamp, string frame_id
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        double cur_time = ((System.DateTime.UtcNow - epochStart).TotalSeconds); // in seconds
        //print("cur_time" + cur_time);
        double decimalPart = cur_time % 1; // get the decimal part
        int secs  = (int)(cur_time-decimalPart); //take the integer part of the timestamp expressed in nanoseconds
        //print("seconds" + secs);
        int nsecs = (int)(decimalPart*1000000000); // take the nanoseconds
        //print("nsencs"+ nsecs);
        TimeMsg stamp = new TimeMsg(secs , nsecs);
        HeaderMsg header = new HeaderMsg(0,stamp,"/unity"); //frame id=/unity.

        BodyVelocityRequestMsg msg = new BodyVelocityRequestMsg(header, goal, twist,dissable_axis); // create a clase of message to be published
        //print("Body request" + twist);
        if (Global_Variables.activate_control) //send velocity commands only if the Unity Control is active
                ros.Publish(RobotBodyVelocityRequest.GetMessageTopic(), msg); // descomentar !! -- 18/09/2017

        //ros.Publish(RobotBodyVelocityRequest.GetMessageTopic(), twist); // descomentar !! -- 18/09/2017

        if (Global_Variables.stop_motors) // dissable motors if required with the "A" button
        {
          ros.CallService("/control/disable_thrusters");
          Global_Variables.stop_motors = false; // avoids to send the service all the time
        }
        if (Global_Variables.start_motors) // enable motors if required with button "B"
        {
            ros.CallService("/control/enable_thrusters");
            Global_Variables.start_motors = false; // avoids to send the service all the time
        }
        ros.Render();
    }

    public void PushCloud(PointCloud2Msg cloud_msg) {
        StartCoroutine("DrawCloud", cloud_msg); // starts a C-O-routine called "drawcloud"
    }

    private IEnumerator DrawCloud(PointCloud2Msg cloud_msg) {
        PointCloud2Prefab converter = new PointCloud2Prefab(cloud_msg.GetPoints(), cloud_msg.GetColors(), cloud_msg.GetHeader().GetSeq(), matVertex);
        GameObject cloud_go = converter.GetPrefab();   
        cloud_go.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        cloud_go.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1);
        cloud_go.transform.parent = pointCloudContainer.transform; 
        cloud_go.SetActive(true); // activate Point Cloud in Unity
        yield return null;
    }
}
