/* 
 * @brief ARSEA Project
 * @author Francisco Bonin Font
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

using ROSBridgeLib;
//using ROSBridgeLib.auv_msgs;
//using ROSBridgeLib.geometry_msgs;
using ROSBridgeLib.sensor_msgs;
//using System.Collections;
using SimpleJSON;
using UnityEngine;


public class RobotImage : ROSBridgeSubscriber
{
    

  public new static string GetMessageTopic()
  {
    return "/stereo_down/scaled_x2/left/image_color";
    //return "/cola2_navigation/nav_sts";
  }

  public new static string GetMessageType()
  {
    return "sensor_msgs/Image";
  }

  public new static ROSBridgeMsg ParseMessage(JSONNode msg)
  {
        Debug.Log("<color=green>INFO:</color> RobotImage parser!");
        return new ImageMsg(msg);
  }

  public new static void CallBack(ROSBridgeMsg msg)
  {
    Debug.Log("<color=green>INFO:</color> RobotImage CallBack!");
    ImageMsg image = (ImageMsg)msg;
    byte[] color_data=image.GetImage();
    uint n_rows = image.GetHeight();
    uint n_columns = image.GetWidth();
    uint step = image.GetRowStep();
    uint data_size = n_rows * step;
    GameObject robot = GameObject.Find("Robot"); // creates a GameObject (element of Unity type "model") 
    ArseaViewer viewer = (ArseaViewer)robot.GetComponent(typeof(ArseaViewer));
    viewer.Paint_image(color_data, data_size,n_rows, n_columns);


    }
}

