/* 
 * @brief ARSEA Project
 * @author Francisco Bonin Font
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

using UnityEngine;

public class SwitchCamera : MonoBehaviour {
    public Camera camera1;
    public Camera camera2;
    bool cameraindex = false;

    // Use this for initialization
    void Start () {
        //turn on the first camera
        camera1.gameObject.SetActive(true);
        Debug.Log("Camera Active: " + camera1.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cameraindex = !cameraindex; 
            if (cameraindex)
            {
                camera1.gameObject.SetActive(false);
                camera2.gameObject.SetActive(true);
            }
            else
            {
                camera1.gameObject.SetActive(true);
                camera2.gameObject.SetActive(false);
                cameraindex = false;
            }
        }
    }
}
