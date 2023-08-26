using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    PlayerHandler handler;

    Camera cam;

    float x;
    float y;
    Vector3 velocity = Vector3.zero;
    float dampTime = 0.1f;

    float timeToFollowFalling = 0.4f;
    float currentTime;

    bool isCameraLocked;


    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();
    }
    private void Start()
    {
        cam = Camera.main;
        dampTime = 0.2f;
    }

    float current = 0;
    float total = 1.2f;

    private void FixedUpdate()
    {
        if (cam == null)
        {

           
            if(Camera.main != null)
            {
                cam = Camera.main;
            }
            else
            {
                cam = Camera.current;
            }

            return;
        }
        


        if (!handler.IsGrounded() && total > current)
        {
            //the camera does not follow.
            current += Time.deltaTime;
        }
        else
        {
            Vector3 camPos = new Vector3(transform.position.x + x, transform.position.y + 1.5f + y, -20);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, camPos, ref velocity, dampTime);
            current = 0;
        }
    }


    public void ControlCameraHorizontal(int dir)
    {
        if (dir != 0)
        {
            x = 3.5f * dir;
        }
    }

    public void ControlCameraVertical(float change)
    {
        y = change;
    }

}
