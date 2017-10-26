using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	public string targetTag;

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody2D> ();
		rb.velocity = transform.up * speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag (targetTag)) {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
