using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject explosion;
    private GameObject[] shootingTowers;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        explosion = GameObject.Find("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1));        

        if (player.gameObject != null && player.transform.position.z > transform.position.z)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.name == "Player")
        {
            Debug.Log("Collided with a red mine!");
            explosion.GetComponent<AudioSource>().Play();
            Destroy(otherCollider.gameObject);
        }
    }
}
