using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using System.Collections;
using SimpleJSON;
using PointCloud;
using UnityEngine;

public class RobotPointCloud2 : ROSBridgeSubscriber
{
    public static Material matVertex;

    public new static string GetMessageTopic()
    {
        return "/stereo_down/points2";
    }

    public new static string GetMessageType()
    {
        return "sensor_msgs/PointCloud2";
    }

    public new static ROSBridgeMsg ParseMessage(JSONNode msg)
    {
        return new PointCloud2Msg(msg);
    }

    public new static void CallBack(ROSBridgeMsg msg)
    {
        Debug.Log("<color=green>INFO:</color> RobotPointCloud2 CallBack!");
        PointCloud2Msg cloud_msg = (PointCloud2Msg)msg;
        GameObject robot = GameObject.Find("Robot");
        ArseaViewer viewer = (ArseaViewer)robot.GetComponent(typeof(ArseaViewer));
        viewer.PushCloud(cloud_msg);
    }
}

