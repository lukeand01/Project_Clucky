using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimatorScript : MonoBehaviour
{

    public static BackgroundAnimatorScript instance; //DOING THIS BECAUSE IM OUT OF TIME. FIX LATER

    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform targetPoint;
    [SerializeField] Transform despawnPoint;

    GameObject currentBackground;
    GameObject nextBackground;
    [SerializeField] GameObject[] backgrounds;
    List<int> lastBackgroundIndexList = new();

    //randomly create stuff.

    float currentTimeBtwMoves;
    [SerializeField] float totalTimeBtwMoves;

    [SerializeField]float moveSpeed;

    bool hasStarted;

    private void Awake()
    {
        instance = this;


       // targetPoint.transform.position = new Vector3(Screen.width * 0.1f, 0, 0); 



    }

    private void Start()
    {

        Debug.Log("called this");
        //spawn first.
        currentBackground = Instantiate(backgrounds[0], targetPoint.transform.position, Quaternion.identity);
        nextBackground = SpawnNextBackground();
        nextBackground.transform.parent = transform;

        if (nextBackground == null) Debug.Log("failed to get the nextbackground");

        hasStarted = true;
    }


    public void ForceSpawnIfEmpty()
    {
        Debug.Log("tried to force it");

        if (nextBackground == null)
        {
            Debug.Log("no need");
            return;
        }

        currentBackground = Instantiate(backgrounds[0], targetPoint.transform.position, Quaternion.identity);
        nextBackground = SpawnNextBackground();
        nextBackground.transform.parent = transform;
    }

    GameObject SpawnNextBackground()
    {
        int index = GetNextBackgroundIndex();

        if(index == -1)
        {
            Debug.LogError("something went wrong");
            return null;
        }
        //
        lastBackgroundIndexList.Add(index);

        GameObject newObject = Instantiate(backgrounds[index], spawnPoint.transform.position, Quaternion.identity);
        return newObject;

    }

    int GetNextBackgroundIndex()
    {
        GameObject nextBackground = null;

        int breakLimit = 0;

        while(nextBackground == null)
        {
            breakLimit += 1;

            if(breakLimit > 10000)
            {
                Debug.LogError("Something happened that made the nextbackground break");
                return -1;
            }

            int randomChoice = Random.Range(0, backgrounds.Length);

            if (IsIndexInList(randomChoice))
            {
                continue;
            }
            else
            {
                return randomChoice;
            }

        }

        return -1;
    }

    bool IsIndexInList(int index)
    {
        foreach (var item in lastBackgroundIndexList)
        {
            if (item == index) return true;
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (!hasStarted) return;


        if(totalTimeBtwMoves > currentTimeBtwMoves)
        {
            //we do nothing just count it up.
            currentTimeBtwMoves += 0.01f;
            return;
        }

        if(currentBackground == null || nextBackground == null)
        {
            Debug.Log("a backgroun wasnt spawned");
            return;
        }
        //then we slowly move the fella. when
        float distance = Vector2.Distance(nextBackground.transform.position, targetPoint.transform.position);
        if(distance > 0.05f)
        {
            //move both next and current backgrounds.
            currentBackground.transform.position = Vector3.MoveTowards(currentBackground.transform.position, despawnPoint.position, moveSpeed * 0.1f);
            nextBackground.transform.position = Vector3.MoveTowards(nextBackground.transform.position, targetPoint.position, moveSpeed * 0.1f);
        }
        else
        {
            EndOfMovement();
            //remove the
        }


    }

    void EndOfMovement()
    {
        Destroy(currentBackground);
        currentBackground = nextBackground;
        nextBackground = SpawnNextBackground();

        if(lastBackgroundIndexList.Count > 2)
        {
            lastBackgroundIndexList.RemoveAt(0);
        }
      
        currentTimeBtwMoves = 0;
    }

}
