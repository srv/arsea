using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class touch_direc_rot : MonoBehaviour {

    // Use this for initialization
    public OVRInput.Controller Controller;
	// Update is called once per frame
	void Update () {
        transform.localPosition = OVRInput.GetLocalControllerPosition(Controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(Controller);
    }
}
