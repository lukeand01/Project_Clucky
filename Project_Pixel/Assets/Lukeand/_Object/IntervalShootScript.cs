using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalShootScript : MonoBehaviour
{
    //
    [SerializeReference] GameObject projectilTemplate;


    [SerializeField] float current;
    [SerializeField] float minTotal;
    [SerializeField] float maxTotal;
    float total;
    bool hasStarted;
    private void Awake()
    {
        total = Random.Range(1, 3);
    }

    public void StartShooting()
    {
        hasStarted = true;
    }
    public void StopShooting()
    {
        hasStarted = false;
    }

    private void FixedUpdate()
    {
        if (!hasStarted) return;
        if(total > current)
        {         
            current += 0.02f;
        }
        else
        {
            CreateProjectil();
            current = 0;
            total = Random.Range(minTotal, maxTotal);
        }
    }


    void CreateProjectil()
    {
        GameObject newObject = Instantiate(projectilTemplate, transform.position, Quaternion.identity);
        DestroySelf destroy = newObject.GetComponent<DestroySelf>();

        if (destroy != null) destroy.SetUp(10);


    }
}
