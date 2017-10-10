using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobility : MonoBehaviour {

	public float speed;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
    public bool moving = false;
    public LayerMask collision;

    private BoxCollider2D bc;
	private Rigidbody2D rb;
	private float nextFire;

	// Use this for initialization
	void Start () {
        bc = GetComponent<BoxCollider2D>();
		rb = GetComponent <Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update()
	{
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); //as GameObject;
        }
        movement();
    }

	void FixedUpdate () {

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, shotSpawn.transform.eulerAngles.z);
        rb.angularVelocity = 0;


    }

    void movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.up, (speed * Time.deltaTime), collision);
            if(raycast.transform == null)
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.down, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.left, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.right, (speed * Time.deltaTime), collision);
            if (raycast.transform == null)
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey (KeyCode.D) != true && Input.GetKey(KeyCode.A) != true && Input.GetKey(KeyCode.S) != true && Input.GetKey(KeyCode.W) != true)
        {
            moving = false;
        }
    }
}
