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
using UnityEngine.UI;

public class RobotNavSts : ROSBridgeSubscriber
{
    public new static string GetMessageTopic()
    {
        return "/navigation/nav_sts";
        //return "/rema_ros/nav_sts";
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
        GameObject model = GameObject.Find("Model"); 
        GameObject robot = GameObject.Find("Robot");
        Robot r = (Robot)model.GetComponent(typeof(Robot));

        if (model == null)
        {
            Debug.Log("<color=red>ERROR:</color> Can't find GameObject Robot. Create it in Unity");
        }
        else
        {
            NavStsMsg nav_sts = (NavStsMsg)msg;
            NEDMsg p = nav_sts.GetPosition();
            RPYMsg o = nav_sts.GetOrientation();
            robot.transform.position = new Vector3(-p.GetNorth(), 0, p.GetEast());

            Vector3 next_position = new Vector3(-p.GetNorth(), -p.GetDepth(), p.GetEast());
            Quaternion next_orientation = Quaternion.Euler(o.GetRollDegrees() + 90f, o.GetYawDegrees(), o.GetPitchDegrees());
            r.AddWaypoint(next_position, next_orientation);

            //model.transform.position = new Vector3(-p.GetNorth(), -p.GetDepth(), p.GetEast());
            //model.transform.rotation = Quaternion.Euler(o.GetRollDegrees() + 90f, o.GetYawDegrees(), o.GetPitchDegrees());

            GameObject ned_canvas = GameObject.Find("NED");
            Text NED_text = ned_canvas.GetComponent<Text>();
            int dp = 2;
            string format = string.Format("#.{0} m;-#.{0} m", new string('#', dp));
            NED_text.text = p.GetNorth().ToString(format) +
                             "\n" + p.GetEast().ToString(format) +
                             "\n" + p.GetDepth().ToString(format) +
                             "\n" + nav_sts.GetAltitude().GetData().ToString(format);
        }
    }
}

