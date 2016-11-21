using UnityEngine;
using System.Collections;
using ROSBridgeLib;
using System.Reflection;
using System;


public class ArseaViewer : MonoBehaviour {
	public string ROS_IP = "192.168.1.173";
	public int port = 9090;
	private ROSBridgeWebSocketConnection ros = null;

	// the critical thing here is to define our subscribers, publishers and service response handlers
	void Start() {
		// Set ROS
		ros = new ROSBridgeWebSocketConnection("ws://" + ROS_IP, port); 
		ros.Connect();
        ros.AddSubscriber(typeof(RobotNavSts));
    }

	// extremely important to disconnect from ROS. Otherwise packets continue to flow
	void OnApplicationQuit() {
		if(ros!=null) ros.Disconnect();
	}
	
	// Update is called once per frame in Unity
	void Update() {
		ros.Render();
    }
}
