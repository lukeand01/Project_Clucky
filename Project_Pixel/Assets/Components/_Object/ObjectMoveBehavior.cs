using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveBehavior : MonoBehaviour
{
    //this is a beehavior for moving from one side to another.



    public List<Vector3> dirList = new();
    Vector3 lastPos;
    int currentIndex;

    public float totalStopTime;
    public float totalTimeToStart;
    float currentTimeToStart;
    [SerializeField] float speed;
    [SerializeField] bool onlyOneMove;

     bool hasStarted;
    [SerializeField] bool isBlocked;

    private void Start()
    {
        
    }

    public void StartMoving()
    {
        isBlocked = false;
    }


    private void Update()
    {
        if (hasStarted) return;
        if (isBlocked) return;


        if(totalTimeToStart > currentTimeToStart)
        {
            currentTimeToStart += Time.deltaTime;
        }
        else
        {
            hasStarted = true;
            StartCoroutine(MovingProcess());
        }
    }

    IEnumerator MovingProcess()
    {
        lastPos = transform.position;

        while(transform.position != lastPos + dirList[currentIndex])
        {
            //so while you are not that position.
            //you move to that position
            transform.position = Vector3.MoveTowards(transform.position, lastPos + dirList[currentIndex], speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        IncreaseIndex();

        //then we wait a bit and start again.
        float currentStopTime = 0;

        while(totalStopTime > currentStopTime)
        {
            currentStopTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }


        if (onlyOneMove) yield break;

        StartCoroutine(MovingProcess());


    }

    void IncreaseIndex()
    {
        currentIndex += 1;

        if(currentIndex >= dirList.Count)
        {
            currentIndex = 0;
        }
    }


}
