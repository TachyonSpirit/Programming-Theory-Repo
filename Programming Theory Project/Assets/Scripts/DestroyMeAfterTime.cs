using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeAfterTime : MonoBehaviour
{
    private float lifeTime = 3.0f;

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <=0)
        {
            Destroy(gameObject);
        }
    }
}
