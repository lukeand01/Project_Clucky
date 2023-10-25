using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFall : MonoBehaviour
{
    GameObject graphic;
    Vector3 originalPos;
    Rigidbody2D rb;
    bool hasUsed;
    [SerializeField] float shakeAmount = 10;

    private void Awake()
    {
        graphic = transform.GetChild(0).gameObject;
        originalPos = graphic.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && !hasUsed) return;

        hasUsed = true;
        StartCoroutine(ShakeProcess());
    }


    [ContextMenu("DEBUG START")]
    public void DEBUGSTART()
    {
        StartCoroutine(ShakeProcess());
    }

    IEnumerator ShakeProcess()
    {



        float x = 0;
        float y = 0;

        for (int i = 0; i < shakeAmount; i++)
        {

            x = Random.Range(-0.05f, 0.05f);

            graphic.transform.position = originalPos + new Vector3(x, y, 0);
            yield return new WaitForSeconds(0.1f);
        }

        graphic.transform.position = originalPos;

        rb.constraints = RigidbodyConstraints2D.None;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

}
