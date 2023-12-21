using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilBase : MonoBehaviour
{
    //

    Vector3 dir;
    float speed;

    private void FixedUpdate()
    {
        transform.position += dir * 0.05f * speed;
    }

    public void SetUp(Vector3 dir, float speed)
    {
        this.dir = dir;
        this.speed = speed;
    }



}
