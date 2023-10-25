using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBullet : MonoBehaviour
{
    Vector3 dir;
    float speed;
    [SerializeField] float offSet;
    public void SetUp(Vector3 dir, float speed)
    {
        this.dir = dir;
        this.speed = speed;



        Vector3 targ = PlayerHandler.instance.transform.position;
        targ.z = 0f;
        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offSet));
        
    }
    private void Update()
    {
        transform.position += dir * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        collision.gameObject.GetComponent<IDamageable>().TakeDamage(0);
        Destroy(gameObject);

    }



}
