/* 
 * @brief ARSEA Project
 * @author Francisco Bonin Font, Miquel Massot Campos
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
        camera2.gameObject.SetActive(false);
        camera1.enabled = true;
        camera2.enabled = false;
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
                camera1.enabled = false;
                camera2.enabled = true;
            }
            else
            {
                camera1.gameObject.SetActive(true);
                camera2.gameObject.SetActive(false);
                camera1.enabled = true;
                camera2.enabled = false;
                cameraindex = false;
            }
        }
    }
}
