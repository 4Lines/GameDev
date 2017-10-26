using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public float speed;
    public int hitPoints = 10;
    public LayerMask collision;

    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private Transform target;

    // Use this for initialization
    void Start () {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void movement()
    {
        int xDir = 0, yDir = 0;
        if(Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            xDir = target.position.x > transform.position.x ? 1 : -1;
        else
            yDir = target.position.y > transform.position.y ? 1 : -1;
        if(xDir == -1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.left, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        else if(xDir == 1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.right, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        else if(yDir == -1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.down, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
        else if(yDir == 1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.up, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(!Environment.instance.isDoingSetup())
            movement();
	}
}
