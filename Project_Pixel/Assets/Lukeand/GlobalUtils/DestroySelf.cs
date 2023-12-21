using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    float current = 0;
    float total = 0;

    public void SetUp(float total)
    {
        this.total = total;
    }

    private void Start()
    {
        if (total == 0) total = 30;
    }

    private void Update()
    {
        if (total <= 0) return;

        current += Time.deltaTime;

        if (current > total) Destroy(gameObject);
    }


}
