using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode right;
    public KeyCode left;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(forward))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(backward))
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey(left))
        {
            transform.Translate( -speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(right))
        {
            transform.Translate( speed * Time.deltaTime, 0, 0);
        }
    }
}
