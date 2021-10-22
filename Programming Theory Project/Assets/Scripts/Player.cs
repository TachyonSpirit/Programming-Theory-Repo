using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject laserbeam;
    
    private float moveHorizontalSpeed = 40;
    private float moveVerticalSpeed = 40;
    private float forwardSpeed = 25;
    private bool cameraAngle;
    private GameObject gameCamera;
    private GameObject gameController;
    
    private GameObject powerUpSound;
    private GameObject laserbeamSound;
    private GameObject explosionSound;

    void Start()
    {
        cameraAngle = true;

        gameCamera = GameObject.Find("Main Camera");
        gameController = GameObject.Find("GameController");

        powerUpSound = GameObject.Find("PowerUp");
        explosionSound = GameObject.Find("Explosion");
        laserbeamSound = GameObject.Find("Laser");
    }

    void Update()
    {
        //
        // ABSTRACTION
        //
        // Making small modular pieces of code should make it easier
        // for future developers to maintain the code and understand
        // how the application actually works.
        //
        MovePlayerForward();
        MovePlayerUpDownLeftRight();
        SwitchCameraView();
        FireLaser();
    }

    private void MovePlayerForward()
    {
        transform.position = new Vector3(
        transform.position.x,
        transform.position.y,
        transform.position.z + (forwardSpeed * Time.deltaTime)
        );
    }

    private void MovePlayerUpDownLeftRight()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(
            transform.position.x + (moveHorizontalSpeed * Time.deltaTime),
            transform.position.y,
            transform.position.z
            );
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(
            transform.position.x - (moveHorizontalSpeed * Time.deltaTime),
            transform.position.y,
            transform.position.z
            );
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(
            transform.position.x,
            transform.position.y + (moveVerticalSpeed * Time.deltaTime),
            transform.position.z
            );
        }

        if (transform.position.y > 20)
        {
            transform.position = new Vector3(
                transform.position.x,
                20,
                transform.position.z
            );
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(
            transform.position.x,
            transform.position.y - (moveVerticalSpeed * Time.deltaTime),
            transform.position.z
            );
        }
    }

    private void SwitchCameraView()
    {
        // Switch camera view
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (cameraAngle)
            {
                gameCamera.transform.position = new Vector3(60, 30, transform.position.z + 10);
                gameCamera.transform.Rotate(0, -72, 0);
                cameraAngle = false;
            }
            else
            {
                gameCamera.transform.position = new Vector3(0, 30, transform.position.z - 50);
                gameCamera.transform.Rotate(0, +72, 0);
                cameraAngle = true;
            }
        }
    }

    private void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(laserbeam);
            laserbeamSound.GetComponent<AudioSource>().Play();
        }
    }

    public void OnTriggerEnter(Collider otherCollider)
    {
        //Debug.Log(otherCollider.tag);
        if (otherCollider.tag == "Ground" || otherCollider.tag == "Wall" || otherCollider.tag == "Tower" || otherCollider.tag == "Crate")
        {
            explosionSound.GetComponent<AudioSource>().Play();
            GameObject.Destroy(this.gameObject);
        }
        else if (otherCollider.tag == "Fuel")
        {
            // Add to the fuel-counter and update the fuel amount text on the screen
            gameController.GetComponent<GameController>().fuelCounter += 50;
            gameController.GetComponent<GameController>().fuelCounterText.text = "Fuel: " + gameController.GetComponent<GameController>().fuelCounter + "%";
            powerUpSound.GetComponent<AudioSource>().Play();
            Destroy(otherCollider.gameObject);
        }
    }

}
