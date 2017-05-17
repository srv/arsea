﻿/* 
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
        Debug.Log("<color=green>INFO:</color> NavStatus Message Callback");
        //Debug.Log("<color=green>INFO:</color> RobotNavSts CallBack!");
        GameObject model = GameObject.Find("Model"); // creates a GameObject (element of Unity type "model") 
        GameObject robot = GameObject.Find("Robot"); // creates a GameObject (element of Unity type "model") 

        /*new fbf 9/02/2017 */


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
            model.transform.position = new Vector3(-p.GetNorth(), -p.GetDepth(), p.GetEast()); // fbf 21/02/2017 in order to rotate the vehicle with respect its own vertical axis, the 

            // angular position contained in the NavStatus must be set to the model transform, instead of to the robot transform, otherwise the rotation is with respect to the 
            // world systemo of coordinates with a rotation radius equal to the distance between the robot and the origin.
            model.transform.rotation = Quaternion.Euler(o.GetRollDegrees() + 90f, o.GetYawDegrees(), o.GetPitchDegrees()); //fbf  21/02/2017 needed transform to keep the vehicle unrotated
                                                                                                                           //robot.transform.rotation = Quaternion.Euler(o.GetRollDegrees(), 180.0f + o.GetYawDegrees(), o.GetPitchDegrees());

            PointMsg lin_vel = nav_sts.GetBodyVelocity(); // get the body velocity from the nav__sts message
            RPYMsg ang_vel = nav_sts.GetOrientationRate();
            NEDMsg position = nav_sts.GetPosition();
            //ArseaViewer viewer = (ArseaViewer)robot.GetComponent(typeof(ArseaViewer));
            //viewer.Paint_speed(lin_vel, ang_vel, position);

            GameObject ned_canvas = GameObject.Find("NED");

            Text NED_text = ned_canvas.GetComponent<Text>();
            int dp = 2;
            string format = string.Format("#.{0} m;-#.{0} m", new string('#', dp));
            NED_text.text = p.GetNorth().ToString(format) +
                             "\n" + p.GetEast().ToString(format) +
                             "\n" + p.GetDepth().ToString(format) +
                             "\n" + nav_sts.GetAltitude().GetData().ToString(format);


            //canvas  
            // access to elements 'position' and 'orientation' of the transform characteristic associated to the 'Robot' Unity object.
            // canvas.GetComponent(Text) = 'My text;';

            /*draw here the pose and the velocity of the vehicle in the canvas */
            //   GameObject nav_sts_vx_text = GameObject.Find("NavStsSpeedX_txt");
            //       nav_sts_vx_text.GetComponent("Text").guiText
        }
    }
}

