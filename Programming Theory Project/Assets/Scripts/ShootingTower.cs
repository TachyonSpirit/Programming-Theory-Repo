using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Example of inheritance
public class ShootingTower : Tower
{
    public GameObject projectilePrefab;

    new public void Start()
    {
        for (int i=0;i<10;i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = new Vector3(
                transform.position.x + (Random.Range(-5, 5)),
                transform.position.y + (Random.Range(-5, 5)),
                transform.position.z - (i*10)
            );
        }
    }

    // Update is called once per frame
    void Update()
    {
        // It seems to inherit the RotateTower() method properly :)
        RotateTower(25.0f);
        ShootingStuff();
    }

    // Below is a method the parent doesn't have.    
    // Press 'T' to make the towers shoot.
    void ShootingStuff()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Guess I'm also shooting now!");
        }        
    }

    // Below an example of polymorphism
    public override void RotateTower(float rotationSpeed)
    {
        //base.RotateTower(rotationSpeed);
        transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        Debug.Log("Applying RotateTower from ShootingTower class");
    }

}
