using ROSBridgeLib;
using ROSBridgeLib.auv_msgs;
using ROSBridgeLib.geometry_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;

public class RobotNavSts : ROSBridgeSubscriber
{
  public new static string GetMessageTopic()
  {
    //return "/navigation/nav_sts";
    return "/cola2_navigation/nav_sts";
  }

  public new static string GetMessageType()
  {
    return "auv_msgs/NavSts";
  }

  public new static ROSBridgeMsg ParseMessage(JSONNode msg)
  {
    return new NavStsMsg(msg);
  }

  public new static void CallBack(ROSBridgeMsg msg)
  {
    Debug.Log("<color=green>INFO:</color> RobotNavSts CallBack!");
    GameObject robot = GameObject.Find("Robot");
    if (robot == null)
    {
      Debug.Log("<color=red>ERROR:</color> Can't find GameObject Robot");
    }
    else
    {
      NavStsMsg nav_sts = (NavStsMsg)msg;
      NEDMsg p = nav_sts.GetPosition();
      //Debug.Log("<color=green>INFO:</color> Position is " + p.ToString());
      RPYMsg o = nav_sts.GetOrientation();
      //Debug.Log("<color=green>INFO:</color> Orientation is " + o.ToString());
      robot.transform.position = new Vector3(p.GetNorth(), -p.GetDepth(), p.GetEast());
      robot.transform.rotation = Quaternion.Euler(o.GetRollDegrees(), o.GetYawDegrees(), o.GetPitchDegrees());
    }
  }
}

