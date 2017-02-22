/* 
 * @brief ARSEA Project
 * @author Francisco Bonin Font
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

using UnityEngine;
using System.Collections;
using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using PointCloud;
using System.Reflection;
using UnityEngine.UI;
using ROSBridgeLib.auv_msgs;
using ROSBridgeLib.geometry_msgs;

public class ArseaViewer : MonoBehaviour {
    //public string ROS_IP = "192.168.1.9"; // ip xesc xarxa WIFI 5G router intern
    public string ROS_IP = "172.22.38.119"; // this is the UIB xesc's ip address 
    public int port = 9090;
    public Transform robotToCamera;
    public Material matVertex;
    public GameObject pointCloudContainer; // fbf 22/02/2017 gameobject pointCloudContainer is attached to the GameObject PointCloud in Unity.
    private ROSBridgeWebSocketConnection ros = null;
    public Text linear_robotVelocity;
    public Text angular_robotVelocity;
    public Text NED_position;
   // private Boolean _useJoysticks;


    // the critical thing here is to define our subscribers, publishers and service response handlers
    void Start() {
        // Set ROS
        //_useJoysticks = Input.GetJoystickNames().Length > 0;
        Debug.Log("<color=green>INFO:</color> Connecting to " + ROS_IP + "...");
        ros = new ROSBridgeWebSocketConnection("ws://" + ROS_IP, port); 
        ros.Connect();
        ros.AddSubscriber(typeof(RobotNavSts)); // add subscribers as defined in the corresponding classes
        ros.AddSubscriber(typeof(RobotPointCloud2));

       // ros.AddPublisher(typeof(RobotBodyVelocityRequest));

       linear_robotVelocity.text = "0.0";
       angular_robotVelocity.text = "0.0";
    }

    // extremely important to disconnect from ROS. Otherwise packets continue to flow
    void OnApplicationQuit() {
        if(ros!=null) ros.Disconnect();
    }
  
    // Update is called once per frame in Unity
    void Update() {

        //ROSBridgeLib.nav_msgs.BodyVelocityRequestMsg msg = new ROSBridgeLib.nav_msgs.BodyVelocityRequestMsg(new ROSBridgeLib.std_msgs.HeaderMsg(0, TimeMsg stamp, string frame_id), 
          //  new ROSBridgeLib.nav_msgs.GoalDescriptorMsg("Unity_Teleop", 1, 40), new TwistMsg(new Vector3Msg(0.0, 0.0, 0.0), new Vector3Msg(0.0, 0.0, 0.0)), new Bool6AxisMsg(true,true,true,false,false, true)));

       // ros.Publish(RobotBodyVelocityRequest.GetMessageTopic(), msg);
        ros.Render();
                            
                             
    }

    public void Paint_speed(PointMsg vel, RPYMsg ang_vel, NEDMsg position)
    {
        linear_robotVelocity.text = "(" + System.Math.Round(vel.GetX(),2) + ", " + System.Math.Round(vel.GetY(), 2) + ", " + System.Math.Round(vel.GetZ(), 2) + ")";
        angular_robotVelocity.text = "(" + System.Math.Round(ang_vel.GetRoll(),2) + ", " + System.Math.Round(ang_vel.GetPitch(),2) + ", " + System.Math.Round(ang_vel.GetYaw(),2) + ")";
        NED_position.text = "(" + System.Math.Round(position.GetNorth(),2) + ", " + System.Math.Round(position.GetEast(),2) + ", " + System.Math.Round(position.GetDepth(),2) + ")";
    }

    public void PushCloud(PointCloud2Msg cloud_msg)
    {
        StartCoroutine("DrawCloud", cloud_msg); // starts a C-O-routine called "drawcloud"
    }

    private IEnumerator DrawCloud(PointCloud2Msg cloud_msg)
    {
        Debug.Log("<color=green>INFO:</color> Point cloud Prefab ");
        PointCloud2Prefab converter = new PointCloud2Prefab(cloud_msg.GetCloud(), cloud_msg.GetHeader().GetSeq(), matVertex);
        Debug.Log("<color=green>INFO:</color> GetPrefab ");
        // fbf 22/02/2017 simplified the conexion between the unity Gameobject and the resulting pointcloud . Direct connexion instead of using an intermediate Gameobject. 
        pointCloudContainer = converter.GetPrefab();
      ///  GameObject cloud_go = converter.GetPrefab(); // GetPrefab is a method of PointCloud2Prefab. Create a new Unity GameObject type PointCloud2Prefab 
        // and associate the converted Point Cloud
        Debug.Log("<color=green>INFO:</color> GetPrefab ");
     ///   cloud_go.transform.position = new Vector3(0.0f, 0.0f, 0.0f); // set the position of the new GameObject in the origin. All point contained in the object are refered to the origin. 
    ///    cloud_go.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1); // set the rotation of the GameObject in the origin.
      //  cloud_go.transform.position = this.transform.position;
     //   cloud_go.transform.rotation = this.transform.rotation;
    ///    cloud_go.transform.parent = pointCloudContainer.transform; // fbf 22/02/2017. Link the cloud_go.transform computed in the script with the Unity gameobject
    ///    
        Debug.Log("<color=green>INFO:</color> set active ");
        pointCloudContainer.SetActive(true);
        /// cloud_go.SetActive(true); // activate Point Cloud in Unity
        yield return null;
    }
}
