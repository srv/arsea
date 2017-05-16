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
    //public GameObject Imatge; 
    private ROSBridgeWebSocketConnection ros = null;
    public Text linear_robotVelocity;
    public Text angular_robotVelocity;
    public Text NED_position;
   // private Boolean _useJoysticks;


    // the critical thing here is to define our subscribers, publishers and service response handlers
    void Start() {
        // Set ROS
        Debug.Log("<color=green>INFO:</color> Connecting to " + ROS_IP + "...");
        ros = new ROSBridgeWebSocketConnection("ws://" + ROS_IP, port); 
        ros.Connect();

        //_useJoysticks = Input.GetJoystickNames().Length > 0;

        // Define ROS Subscribers and Publishers
        ros.AddSubscriber(typeof(RobotNavSts)); // add subscribers as defined in the corresponding classes
        ros.AddSubscriber(typeof(RobotPointCloud2));
        ros.AddSubscriber(typeof(RobotImage));
        // ros.AddPublisher(typeof(RobotBodyVelocityRequest));

        // Define Services and clients if any

       //Imatge.AddComponent<MeshRenderer>();



    }

    // extremely important to disconnect from ROS. Otherwise packets continue to flow
    void OnApplicationQuit() {
        if(ros!=null) ros.Disconnect();
    }
  
    // Update is called once per frame in Unity
    void Update() {
        ros.Render();                    
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
      //  pointCloudContainer = converter.GetPrefab();
        GameObject cloud_go = converter.GetPrefab(); // GetPrefab is a method of PointCloud2Prefab. Create a new Unity GameObject type PointCloud2Prefab 
        // and associate the converted Point Cloud
        Debug.Log("<color=green>INFO:</color> GetPrefab ");
        cloud_go.transform.position = new Vector3(0.0f, 0.0f, 0.0f); // set the position of the new GameObject in the origin. All point contained in the object are refered to the origin. 
        cloud_go.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1); // set the rotation of the GameObject in the origin.
      //  cloud_go.transform.position = this.transform.position;
     //   cloud_go.transform.rotation = this.transform.rotation;
        cloud_go.transform.parent = pointCloudContainer.transform; // fbf 22/02/2017. Link the cloud_go.transform computed in the script with the Unity gameobject
    ///    
        Debug.Log("<color=green>INFO:</color> set active ");
    //    pointCloudContainer.SetActive(true);
        cloud_go.SetActive(true); // activate Point Cloud in Unity
        yield return null;
    }
}
