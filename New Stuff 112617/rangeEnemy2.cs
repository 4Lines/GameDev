using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeEnemy2 : MonoBehaviour {
    //public Transform shotSpawn;
    //public float fireRate;
    //public GameObject shot;
    public Transform target;
    public int hitPoints = 2;

    public GameObject enemy;                // The enemy prefab to be spawned.
    public float nextSpawn = 0f;
    public float spawnRate = 2f;
    // How long between each spawn.
    public Transform[] spawnPoints;

    private float range = 20.0f;

    private Transform t;
    private Transform player;

    //New boolean for granting EXP
    private bool expGranted = false;

    // Use this for initialization
    void Start () {
        t = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Bullet"))
        {
            hitPoints = hitPoints - 1;
            
        }
    }

    private float Distance()
    {
        return Vector3.Distance(t.position, player.position);
    }
    // EXP addition added to death 11/26/2017.
    // Update is called once per frame
    void Update () {
        if (!Environment.instance.isDoingSetup())
        {
            if (Distance() < range)
            {
                if (Time.time > nextSpawn)
                {
                    nextSpawn = Time.time + spawnRate;
                    Spawn();
                }
                if (!expGranted)
                {
                    Environment.instance.giveEXP(3);
                    expGranted = true;
                }
                if (hitPoints < 1)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    

    void Spawn()
    {

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
    
}
