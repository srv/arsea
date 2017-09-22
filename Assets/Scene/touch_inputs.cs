/* 
 * @brief ARSEA Project
 * @author Francisco Bonin Font
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class touch_inputs : MonoBehaviour
{
    public OVRInput.Controller Controller;
    public float vel_linear_x;
    public float vel_linear_y;
    public float vel_linear_z;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            Debug.Log(" Pushed button 'X', Dissable motors"); // send order to stop all motors. 
            Global_Variables.stop_motors = true;
        }
        if (OVRInput.Get(OVRInput.Button.Four))
        {
            Debug.Log(" Pushed button 'Y', Enable motors"); // send order to enable all motors. 
            Global_Variables.start_motors = true;
        }
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            Debug.Log(" Pushed button 'B', Activate Unity Remote Control "); // send order to activate Unity remote Control. 
            Global_Variables.activate_control = true;
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log(" Pushed button 'A', Deactivate Unity Remote control"); // send order to deactivate Unity remote Control. 
            Global_Variables.activate_control = false;
        }

        if (OVRInput.Get(OVRInput.Button.Start))
        {
            Debug.Log(" Pushed button 'Start', change camera");  
            Global_Variables.change_camera = true;
        }

        // fbf 22_09_2017 --> the axis configuration of the Primary and SecondaryThumstick is : y axis is the vertical and x axis is the horizontal. Positive x are to the right and negative to the left. Positive y are upwards and negative downwards.
        //Vector2 touchAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick) * Time.deltaTime;
        Vector2 touchAxis_h = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick); // got the touch 2D axis values of the left controller to navigate in the horizontal plane
        Global_Variables.vel_linear_x = touchAxis_h.y;
        Global_Variables.vel_linear_y = touchAxis_h.x;
        // print("Selected velocity: " + Global_Variables.vel_linear_x + " Y: " + Global_Variables.vel_linear_y); // print the selected direction of motion.
      //  Debug.Log("Axis Values X, Y: " + touchAxis_h.y + touchAxis_h.x);

        Vector2 touchAxis_v = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick); // got the touch 2D axis values of the right controller to navigate in the vertical direction
        Global_Variables.vel_linear_z = touchAxis_v.y; ;
        // print("Selected velocity: " + Global_Variables.vel_linear_x + " Y: " + Global_Variables.vel_linear_y); // print the selected direction of motion.
      //  Debug.Log("Axis Values Z: " + touchAxis_v.y);

        //if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) // detect when the left index finger trigger has been pressed more than halfway
        //{
        //    Vector3 worldDirection = OVRInput.GetLocalControllerRotation(Controller) * Vector3.forward; // OVRInput.GetLocalControllerRotation(Controller) recuperates 
        //   // Vector3 worldPosition =  OVRInput.GetLocalControllerPosition(Controller) * Vector3.forward * Time.deltaTime; // OVRInput.GetLocalControllerRotation(Controller) recuperates 
        //    //the orientation of the right remote is a quaternion. Then, multiplied by a generic forward direction, we get a direction vector with the got orientation. 
        //    print("Selected Direction. X: " + worldDirection.x + " Y: " + worldDirection.y + " Z: " + worldDirection.z); // print the selected direction of motion.
        //    // this would be the velocity
        //    // vel_linear_x = touchAxis.x;                                                         
        //    // vel_linear_y = touchAxis.y;
        //    Global_Variables.vel_linear_x = worldDirection.x;
        //    Global_Variables.vel_linear_y = worldDirection.y;
        //    Global_Variables.vel_linear_z = worldDirection.z;
        //    print("Selected velocity: X: " + Global_Variables.vel_linear_x + " Y: " + Global_Variables.vel_linear_y + " z: " + Global_Variables.vel_linear_z); // print the selected direction of motion.
        //}
    }
}
