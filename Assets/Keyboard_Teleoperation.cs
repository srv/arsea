/**
 * Manage the actions for key push in the keyboard teleoperation
 * @brief ARSEA Project
 * @author Francisco Bonin Font
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 * --> axis: [x][y][z][roll][pitch][yaw][u][v][w][p][q][r]
 */


using UnityEngine;
using System.Collections;

public class Keyboard_Teleoperation : MonoBehaviour {
    private float[] desired_vel= new float[4] ; // declare matrix of floats initialized to zeros
    private int TWIST_U, TWIST_V, TWIST_W, TWIST_R; // velocity matrix indexes corresponding to teleoperator axis
    //The frame rate is high, so the increment or decrement of every
    //  speed component is quick if the key keeps pressed. Tune the increment from the users console to adjust the speed variation in each axis when the key is pressed
    public float increment;
    // Use this for initialization
    void Start () {
        // velocity matrix indexes
        TWIST_U = 0; // axis X 
        TWIST_V = 1; // axis Y
        TWIST_W = 2; // axis Z
        TWIST_R = 3; // axis Yaw
        // stop vehicle and the beggining
        for (int i = 0; i < 4; i++)
        {
            desired_vel[i] = (float)0.0;
        }
    }

    // Update is called once per frame. 
    void Update () {

        if (Input.GetKey(KeyCode.W)) // if push the W ke --> required forward motion in the U axis
        {
            desired_vel[TWIST_U] = desired_vel[TWIST_U] + increment;

        }
        if (Input.GetKey(KeyCode.S))  // required backward motion
        {
            desired_vel[TWIST_U] = desired_vel[TWIST_U] - increment;
        }

        if (Input.GetKey(KeyCode.A))  // required turn left
        {
            desired_vel[TWIST_R] = desired_vel[TWIST_R] - increment;
        }

        if (Input.GetKey(KeyCode.D))  // required turn right
        {
            desired_vel[TWIST_R] = desired_vel[TWIST_R] + increment;
        }

        if (Input.GetKey(KeyCode.LeftArrow))  // required turn left
        {
            desired_vel[TWIST_V] = desired_vel[TWIST_V] - increment;
        }

        if (Input.GetKey(KeyCode.RightArrow))  // required turn right
        {
            desired_vel[TWIST_V] = desired_vel[TWIST_V] + increment;
        }

        if (Input.GetKey(KeyCode.Space)) // Stop required
        {
            for (int i = 0; i < 4; i++) {
                desired_vel[i] = (float)0.0;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow)) // emerge required 
        {
            desired_vel[TWIST_W] = desired_vel[TWIST_W] - increment;
        }

        if (Input.GetKey(KeyCode.DownArrow)) // submerge required 
        {
            desired_vel[TWIST_W] = desired_vel[TWIST_W] + increment;
        }

        // Saturate values between -1 and 1
        for (int i = 0; i < 4; i++)
        {
            if (desired_vel[i] > 1.0)
                desired_vel[i] = (float)1.0;
            if (desired_vel[i] < -1.0)
                desired_vel[i] = (float)-1.0;
        }
        // Copy values into teleoperation format
        Debug.Log("desired velocity"+ desired_vel[0] + ", " + desired_vel[1] + ", " + desired_vel[2] + ", " + desired_vel[3]);
    }
}
