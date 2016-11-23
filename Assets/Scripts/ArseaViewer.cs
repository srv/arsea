using UnityEngine;
using System.Collections;
using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using PointCloud;
using System.Reflection;
using System;


public class ArseaViewer : MonoBehaviour {
    public string ROS_IP = "192.168.1.173";
    public int port = 9090;
    public Transform robotToCamera;
    public Material matVertex;
    public GameObject pointCloudContainer;
    private ROSBridgeWebSocketConnection ros = null;

    // the critical thing here is to define our subscribers, publishers and service response handlers
    void Start() {
        // Set ROS
        Debug.Log("<color=green>INFO:</color> Connecting to " + ROS_IP + "...");
        ros = new ROSBridgeWebSocketConnection("ws://" + ROS_IP, port); 
        ros.Connect();
        ros.AddSubscriber(typeof(RobotNavSts));
        ros.AddSubscriber(typeof(RobotPointCloud2));
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
        StartCoroutine("DrawCloud", cloud_msg);
    }

    private IEnumerator DrawCloud(PointCloud2Msg cloud_msg)
    {
        PointCloud2Prefab converter = new PointCloud2Prefab(cloud_msg.GetCloud(), cloud_msg.GetHeader().GetSeq(), matVertex);
        GameObject cloud_go = converter.GetPrefab();
        cloud_go.transform.position = this.transform.position;
        cloud_go.transform.rotation = this.transform.rotation;
        cloud_go.transform.parent = pointCloudContainer.transform;
        cloud_go.SetActive(true);
        yield return null;
    }
}
