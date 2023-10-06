using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spyke : MonoBehaviour
{
    [SerializeField] float totalTimeToStart = 0;
    [SerializeReference] float totalTimeBtwActivations;
    float currentTimeBtwActivations;
    float currentTimeToStart = 0;


    float currentTillDanger;
    float totalTillDanger = 0.35f;

    bool isWorking;
    public bool isActive;

    Animator anim;
    BoxCollider2D myCollider;


    bool isReady;

    Vector3 originalPos;

    private void Awake()
    {
        originalPos = transform.position;
        myCollider = GetComponent<BoxCollider2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        anim.SetBool("IsActive", isActive);

        StartCoroutine(FirstProcess());
    }

    //i want the thing to shake.
   

    IEnumerator FirstProcess()
    {
        Debug.Log("first");
        yield return new WaitForSeconds(totalTimeToStart);
        anim.SetBool("IsWorking", true);
        StartCoroutine(TrueProcess());
    }

    IEnumerator TrueProcess()
    {
        Debug.Log("started this");
        yield return new WaitForSeconds(totalTimeBtwActivations);
        if (isActive)
        {
            isActive = false;
            anim.SetBool("IsActive", false);
        }
        else
        {
            isActive = true;
            anim.SetBool("IsActive", true);


            //shake.

            float current = 0;
            float total = 0.15f;

            while (total > current)
            {
                current += Time.deltaTime;

                float offset = Random.Range(-0.025f, 0.025f);

                transform.position = new Vector3(transform.position.x + offset, transform.position.y, 0);
                yield return new WaitForSeconds(Time.deltaTime);
                Debug.Log("this");
            }

            transform.position = originalPos;

        }
        Debug.Log("done");
        myCollider.enabled = isActive;

        StartCoroutine(TrueProcess());
    }


    //change animation which will take care of the rest.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 3) return;

        IDamageable damage = collision.gameObject.GetComponent<IDamageable>();

        if (damage == null) return;

        damage.TakeDamage(0);
    }
}
