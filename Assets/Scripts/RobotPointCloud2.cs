/* 
 * @brief ARSEA Project
 * @author Miquel Massot Campos
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using System.Collections;
using SimpleJSON;
using PointCloud;
using UnityEngine;

public class RobotPointCloud2 : ROSBridgeSubscriber
{
    public static Material matVertex;
    private static int _i = 0;

    public new static string GetMessageTopic()
    {
        //return "/rema_ros/frame_points";
        return "/cloud";
        ///return "/stereo_down/scaled_x2/points2";
        //return "/octomap_server_local_plan/octomap_point_cloud_centers";
    }

    public new static string GetMessageType()
    {
        return "sensor_msgs/PointCloud2"; // define the type of message. We wait for a PointCloud2 contained in the 'points' topic

    }

    public new static ROSBridgeMsg ParseMessage(JSONNode msg)
    {
        return new PointCloud2Msg(msg);
    }

    public new static void CallBack(ROSBridgeMsg msg)
    {
        if ( _i % 5 == 0)
        {
            PointCloud2Msg cloud_msg = (PointCloud2Msg)msg;
            GameObject robot = GameObject.Find("Robot"); // "Robot" --> 
            ArseaViewer viewer = (ArseaViewer)robot.GetComponent(typeof(ArseaViewer));
            viewer.PushCloud(cloud_msg);
        }
        _i = _i + 1;
    }
}

