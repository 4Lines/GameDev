using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobility : MonoBehaviour {

	private float speed = 40f;
	public GameObject shot;
	public Transform shotSpawn;
	public Transform shotSpawn2;
	public Transform shotSpawn3;
	private float fireRate = 0.5f;
	public float hitPoints = 10f;

	private Rigidbody2D rb;
	private float nextFire;

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update()
	{
		/*Fire ball ability executiom
		if (Input.GetMouseButton(0) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); //as GameObject;
		}

		if (Input.GetMouseButton (1) && Time.time > nextFire) { 
			nextFire = Time.time + fireRate;
			if (shot.transform.localScale.x < 19){
				shot.transform.localScale += new Vector3 (2, -2, 0);
				shot.GetComponent<FireBall> ().baseDamage += 1.5f;
			}
			
		} else if (Input.GetMouseButtonUp (1)) {
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); 
			shot.transform.localScale = new Vector3 (5, -5, 1);
			shot.GetComponent<FireBall> ().baseDamage = 1f;
		} */


		//Lightning Spell execution
		if (Input.GetMouseButton(0) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			Instantiate (shot, shotSpawn2.position, shotSpawn2.rotation);
			Instantiate (shot, shotSpawn3.position, shotSpawn3.rotation);
		} 

		if (hitPoints <= 0f) // die
			Destroy (gameObject);
	}
		
	void FixedUpdate () {
		
		var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rot = Quaternion.LookRotation (transform.position - mousePosition, Vector3.forward);

		transform.rotation = rot;
		transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);
		rb.angularVelocity = 0;

		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rb.AddForce(movement * speed) ;

	}

}
