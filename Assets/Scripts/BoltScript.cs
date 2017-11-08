using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour	{

	private PlayerMobility player;
	private Rigidbody2D rb;
	private string targetTag = "Player";
	public float speed;
	public float baseDamage = 1f;

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody2D> ();
		rb.velocity = transform.up * speed;
		player = GetComponent <PlayerMobility> ();
		Destroy (gameObject, 3f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag (targetTag)) {
			other.gameObject.GetComponent<PlayerMobility>().hitPoints -= baseDamage;

			Destroy (gameObject);
		}	

	}
}
