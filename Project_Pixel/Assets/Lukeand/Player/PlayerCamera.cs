using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    [SerializeField] bool cantUseCamera;

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

    bool isForceFollow;
    float currentForceFollow;

    bool shouldCamNotFollowInAir;

    public void ForceFollow()
    {
        isForceFollow = true;

        currentForceFollow = 0.2f;
    }

    public void ControlCameraUse(bool choice)
    {
        cantUseCamera = choice;
    }

    private void FixedUpdate()
    {
        if (cantUseCamera) return;

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

        if (handler.isFallen)
        {
            return;
        }

        if(currentForceFollow > 0)
        {
            currentForceFollow -= Time.deltaTime;
        }


        if (handler.IsGrounded() && isForceFollow && currentForceFollow <= 0)
        {
            Debug.Log("force follow not");
            isForceFollow = false;
        }



        if (shouldCamNotFollowInAir && !handler.IsGrounded() && total > current && !isForceFollow)
        {
            //the camera does not follow.
            current += Time.deltaTime;
        }
        else
        {

            Vector3 camPos = Vector3.zero;

            bool shouldUpdateCam = GetRightMaginuteBetweenCameraAndPlayer() < 0.1f;

     

            if (shouldUpdateCam)
            {
                if (handler.IsGrounded() && PlayerHandler.instance.controller.isHoldingUp)
                {
                    camPos = new Vector3(transform.position.x + x, transform.position.y + 1.2f + y, -20);

                }
                if (handler.IsFalling() || PlayerHandler.instance.controller.isHoldingDown)
                {
                    camPos = new Vector3(transform.position.x + x, transform.position.y - 3.5f + y, -20);

                }
            }
            

            if(camPos == Vector3.zero)
            {
                camPos = new Vector3(transform.position.x + x, transform.position.y + 0.5f + y, -20);
            }
            
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, camPos, ref velocity, dampTime);
            current = 0;

        }
    }

    float GetRightMaginuteBetweenCameraAndPlayer()
    {
        Vector3 player = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 camPos = new Vector3(cam.transform.position.x, cam.transform.position.y, 0);

        return (camPos - player).magnitude;
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


    public void ForceCameraIntoTransform(Transform transform)
    {
        if(cam == null)
        {
            cam = Camera.main;
        }
 
        cam.transform.position = transform.position;
    }

    public bool IsCameraCentralized()
    {
        return cam.transform.position == transform.position;
    }

}



//how to better control camera.
//when the player is falling then it moves a bit down.