using UnityEngine;
using System.Collections;
using ROSBridgeLib;
using System.Reflection;
using System;


public class ArseaViewer : MonoBehaviour {
	private ROSBridgeWebSocketConnection ros = null;

	// the critical thing here is to define our subscribers, publishers and service response handlers
	void Start() {
		ros = new ROSBridgeWebSocketConnection("ws://192.168.1.173", 9090); 
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
