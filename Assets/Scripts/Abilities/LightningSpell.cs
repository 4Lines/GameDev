using System.Collections;
using System.Collections.Generic;
using System.Diagnostics ;
using UnityEngine;

public class LightningSpell : AbilitySystem  {

	private EnemyScript enemy;
	private Shock stun;
	private const string aName = "Lightning Spell";
	private const string aDescription = "A lightning spell stuns an object on impact";
	private Rigidbody2D rb;
	private string targetTag = "Enemy";

	// ability parameter 
	public const float shockDuration = 0.5f;
	public const float baseDamage = 1f;
	public float speed = 20f;

	private Stopwatch durationTimer = new Stopwatch();

	void Start () {
		rb = GetComponent <Rigidbody2D> ();
		rb.velocity = transform.up * speed;
		Destroy (gameObject, 3f);
	}

	public LightningSpell(): base(new BasicObjectInfo (aName, aDescription))
	{
		this.AbilityBehaviors.Add (new Shock (2f, shockDuration, baseDamage));
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag (targetTag)) {
			other.gameObject.GetComponent<EnemyScript>().hitPoints -= baseDamage;
			other.gameObject.GetComponent<EnemyScript> ().speed = 0f;

			StartCoroutine (STUN(other.gameObject));

		}	

	}

	private IEnumerator STUN(GameObject objectHit)
	{
		
		yield return new WaitForSeconds (shockDuration);
		objectHit.GetComponent<EnemyScript> ().speed = 3f;
		Destroy (gameObject);
		yield return null;

	}
}
