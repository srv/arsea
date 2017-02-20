/* 
 * @brief ARSEA Project
 * @author Miquel Massot Campos
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

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
    return "/navigation/nav_sts";
    //return "/cola2_navigation/nav_sts";
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
    //Debug.Log("<color=green>INFO:</color> RobotNavSts CallBack!");
    GameObject robot = GameObject.Find("Robot"); // creates a GameObject (element of Unity type "Robot") 

        /*new fbf 9/02/2017 */


    if (robot == null)
    {
      Debug.Log("<color=red>ERROR:</color> Can't find GameObject Robot. Create it in Unity");
    }
    else
    {
      NavStsMsg nav_sts = (NavStsMsg)msg;
      NEDMsg p = nav_sts.GetPosition();
      //Debug.Log("<color=green>INFO:</color> Position is " + p.ToString());
      RPYMsg o = nav_sts.GetOrientation();
      //Debug.Log("<color=green>INFO:</color> Orientation is " + o.ToString());
      robot.transform.position = new Vector3(p.GetNorth(), -p.GetDepth(), p.GetEast()); 
      robot.transform.rotation = Quaternion.Euler(o.GetRollDegrees(), 180.0f+o.GetYawDegrees(), o.GetPitchDegrees());
      PointMsg lin_vel = nav_sts.GetBodyVelocity(); // get the body velocity from the nav__sts message
      RPYMsg  ang_vel = nav_sts.GetOrientationRate();
      NEDMsg position = nav_sts.GetPosition();
      ArseaViewer viewer = (ArseaViewer)robot.GetComponent(typeof(ArseaViewer));
      viewer.Paint_speed(lin_vel, ang_vel, position);


            //canvas  
            // access to elements 'position' and 'orientation' of the transform characteristic associated to the 'Robot' Unity object.
            // canvas.GetComponent(Text) = 'My text;';
            /*draw here the pose and the velocity of the vehicle in the canvas */
            //   GameObject nav_sts_vx_text = GameObject.Find("NavStsSpeedX_txt");
            //       nav_sts_vx_text.GetComponent("Text").guiText
        }
    }
}

