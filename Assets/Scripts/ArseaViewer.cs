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

public class ArseaViewer : MonoBehaviour {
    public string ROS_IP = "192.168.1.170";
    public int port = 9090;
    public Transform robotToCamera;
    public Material matVertex;
    public GameObject pointCloudContainer;
    private ROSBridgeWebSocketConnection ros = null;


    void Start() {
        // Set ROS
        Debug.Log("<color=green>INFO:</color> Connecting to " + ROS_IP + "...");
        ros = new ROSBridgeWebSocketConnection("ws://" + ROS_IP, port); 
        ros.Connect();


        // Define ROS Subscribers and Publishers
        ros.AddSubscriber(typeof(RobotNavSts)); // add subscribers as defined in the corresponding classes
        ros.AddSubscriber(typeof(RobotPointCloud2));
        ros.AddSubscriber(typeof(RobotImage));
        // ros.AddPublisher(typeof(RobotBodyVelocityRequest));

        // Define Services and clients if any
    }

    // extremely important to disconnect from ROS. Otherwise packets continue to flow
    void OnApplicationQuit() {
        if(ros!=null) ros.Disconnect();
    }
  
    // Update is called once per frame in Unity
    void Update() {
        ros.Render();                    
    }

    public void PushCloud(PointCloud2Msg cloud_msg) {
        StartCoroutine("DrawCloud", cloud_msg); // starts a C-O-routine called "drawcloud"
    }

    private IEnumerator DrawCloud(PointCloud2Msg cloud_msg) {
        PointCloud2Prefab converter = new PointCloud2Prefab(cloud_msg.GetCloud(), cloud_msg.GetHeader().GetSeq(), matVertex);
        GameObject cloud_go = converter.GetPrefab();   
        cloud_go.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        cloud_go.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1);
        cloud_go.transform.parent = pointCloudContainer.transform; 
        cloud_go.SetActive(true); // activate Point Cloud in Unity
        yield return null;
    }
}
