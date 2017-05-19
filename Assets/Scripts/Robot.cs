/* 
 * @brief ARSEA Project
 * @author Miquel Massot Campos
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {
    public float speed = 0.1F;
    private bool first_time = true;
    private Vector3 next_position;
    private Quaternion next_rotation;
    public void AddWaypoint (Vector3 position, Quaternion rotation)
    {
        if (first_time)
        {
            transform.position = position;
            transform.rotation = rotation;
            first_time = false;
        }
        next_rotation = rotation;
        next_position = position;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position , next_position, step);
        transform.rotation = Quaternion.Lerp(transform.rotation, next_rotation, step);
    }
}