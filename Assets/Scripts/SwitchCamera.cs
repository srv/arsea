/* 
 * @brief ARSEA Project
 * @author Francisco Bonin Font, Miquel Massot Campos
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour {
    public Camera Camera1;
    public Camera Camera2;
    // Use this for initialization
    void Start () {
        //turn on the first camera
        Camera1.gameObject.SetActive(true);
       // Camera2.gameObject.SetActive(false);
        Camera1.enabled = true;
        Debug.Log("Camera Active: " + Camera1.gameObject.name);
	}
	
	// Update is called once per frame
	void Update ()
    {
       // print("Switch Image");
        if (Global_Variables.change_camera)
        {
           // print("Key C pressed");
            Global_Variables.cameraindex = !Global_Variables.cameraindex; 
            if (Global_Variables.cameraindex)
            {
                print("activo camera 1");
                Camera1.gameObject.SetActive(false);
                Camera2.gameObject.SetActive(true);
                Camera1.enabled = false;
                Camera2.enabled = true;
            }
            else
            {
                print("activo camera 2");
                Camera1.gameObject.SetActive(true);
                Camera2.gameObject.SetActive(false);
                Camera1.enabled = true;
                Camera2.enabled = false;
            }
        }
     Global_Variables.change_camera = false;
    }
}
