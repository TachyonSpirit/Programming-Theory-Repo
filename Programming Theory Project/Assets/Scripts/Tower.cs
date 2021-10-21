using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RotateTower(50.0f);
    }

    public virtual void RotateTower(float rotationSpeed)
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        Debug.Log("Applying RotateTower from Tower class");
    }    
}
