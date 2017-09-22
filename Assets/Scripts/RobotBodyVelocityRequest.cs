/* 
 * @brief ARSEA Project
 * @author Francisco Bonin Font
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

using ROSBridgeLib;
using ROSBridgeLib.auv_msgs;
using ROSBridgeLib.geometry_msgs;
using ROSBridgeLib.nav_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;



public class RobotBodyVelocityRequest: ROSBridgePublisher
{

    public new static string GetMessageTopic()
    {
        return "/control/body_velocity_req";
        //return "/unity/twist"; 
    }

    public new static string GetMessageType()
    {
        return "auv_msgs/BodyVelocityReq";
       // return "geometry_msgs/Twist";
    }

    //public static string ToYAMLString(BodyVelocityRequestMsg msg)
    public static string ToYAMLString(TwistMsg msg)
    {
        return msg.ToYAMLString();
    }


    

}


