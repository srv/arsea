using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class move_sparus : MonoBehaviour
{

    // Use this for initialization
    public OVRInput.Controller Controller_auv;
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = OVRInput.GetLocalControllerPosition(Controller_auv);
        transform.localRotation = OVRInput.GetLocalControllerRotation(Controller_auv);
    }
}
