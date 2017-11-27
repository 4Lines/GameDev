using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private bool Death = false;
    private bool dropHealth = true;
    private bool dropMp = true;
    public float speed;
    public int hitPoints = 4;
    public float timer = 5.0f;
    public float chaseRange;
    //public Transform SpawnSpot;

    //Collision renamed to CollisionLayer. Will require prefabs to be reset.
    public LayerMask collisionLayer;
    public GameObject healthPrefab; 
    public GameObject mpPrefab;

    private float HealthDropRate = 0.4f;
    private float drop;
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private Transform target;

    Animator anim;

    //New boolean for giving EXP
    private bool expGranted = false;

    // Use this for initialization
    void Start()
    {
        drop = Random.Range(0f, 1f);
        Debug.Log(drop);
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Bullet"))
        {
            hitPoints = hitPoints - 1;
            Vector3 translateVector = Vector3.Normalize(transform.position - collision.collider.transform.position) * 2f;
            transform.Translate(translateVector * speed * Time.deltaTime, Space.World);
            Debug.Log(hitPoints);
        }
        if (collision.collider.gameObject.CompareTag("Enemy"))
            Debug.Log("Collision with Enemy");
        if(collision.collider.gameObject.CompareTag("Player"))
        {
            Vector3 translateVector = Vector3.Normalize((transform.position + (Vector3)bc.offset) - collision.collider.transform.position) * 2.5f;
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, translateVector, (speed * Time.deltaTime), collisionLayer);
            if (raycast.transform == null)
                transform.Translate(translateVector * speed * Time.deltaTime, Space.World);
        }
        Debug.Log(hitPoints);
    }

    void movement()
    {
        int xDir = 0, yDir = 0;
        if (Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            xDir = target.position.x > transform.position.x ? 1 : -1;
        else
            yDir = target.position.y > transform.position.y ? 1 : -1;
        if (xDir == -1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.left, (speed * Time.deltaTime), collisionLayer);
            if (raycast.transform == null)
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        else if (xDir == 1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.right, (speed * Time.deltaTime), collisionLayer);
            if (raycast.transform == null)
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        else if (yDir == -1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.down, (speed * Time.deltaTime), collisionLayer);
            if (raycast.transform == null)
                transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
        else if (yDir == 1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.up, (speed * Time.deltaTime), collisionLayer);
            if (raycast.transform == null)
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
    }

    /*void movementToSpawn()
    {
        int xDir = 0, yDir = 0;
        if (Mathf.Abs(SpawnSpot.position.x - transform.position.x) > Mathf.Abs(SpawnSpot.position.y - transform.position.y))
            xDir = SpawnSpot.position.x > transform.position.x ? 1 : -1;
        else
            yDir = SpawnSpot.position.y > transform.position.y ? 1 : -1;
        if (xDir == -1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.left, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        else if (xDir == 1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.right, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        else if (yDir == -1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.down, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
        else if (yDir == 1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.up, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
    }
    */
    // EXP addition added to death 11/26/2017.
    // Update is called once per frame
    void Update()
    {
        if (!Environment.instance.isDoingSetup())
        {

            if (Death != true)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (distanceToTarget < chaseRange)
                {
                    movement();
                }
                else
                {
                    //movementToSpawn();
                }
            }

            if (hitPoints < 1)
            {
                anim.SetBool("Death", true);
                Death = true;
                if (drop <= HealthDropRate)
                {
                    if (Death.Equals(true) && dropHealth.Equals(true))
                    {
                        Instantiate(healthPrefab, this.transform.position, this.transform.rotation);
                        dropHealth = false;
                    }
                }
                else
                {
                    if (Death.Equals(true) && dropMp.Equals(true))
                    {
                        Instantiate(mpPrefab, this.transform.position, this.transform.rotation);
                        dropMp = false;
                    }
                }
                if (!expGranted)
                {
                    Environment.instance.giveEXP(3);
                    expGranted = true;
                }
                Destroy(gameObject, 1.0f);
            }
            
        }
    }


}
