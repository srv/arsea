/* 
 * @brief ARSEA Project
 * @author Francisco Bonin Font
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */
 // this class contains all the global variables. 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class Global_Variables { 
    public static double vel_linear_x;
    public static double vel_linear_y;
    public static float vel_linear_z;
    public static double yaw;
    // Use this for initialization
    public static bool stop_motors=false;
    public static bool start_motors = false;
    public static bool activate_control = false;
    public static bool change_camera = false;
    public static bool cameraindex = false;

}
