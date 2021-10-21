using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laserbeam : MonoBehaviour
{
    private float speed = 150;
    private float offset = 3;
    private GameObject player;
    private GameObject explosion;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        explosion = GameObject.Find("Explosion");

        transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            player.transform.position.z + offset
        );
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + (speed * Time.deltaTime)
        );
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.tag == "Tower" || otherCollider.tag == "Crate")
        {
            explosion.GetComponent<AudioSource>().Play();
            Destroy(otherCollider.gameObject);
        }
    }
}
