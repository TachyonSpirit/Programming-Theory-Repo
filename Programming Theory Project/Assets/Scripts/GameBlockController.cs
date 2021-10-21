using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBlockController : MonoBehaviour
{

    public float size;
    private float lifeTimer = 20;

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer<=0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
