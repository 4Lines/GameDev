using System.Collections;
using System.Collections.Generic;
using System.Diagnostics ;
using UnityEngine;

public class FireBall : AbilitySystem  {

	private DamageOverTime dot;
	private const string aName = "Fire Ball";
	private const string aDescription = "A fire spell that deals DOT";
	// private FireBall fba;
	// private Stopwatch abilityCooldownTimer;
	private Rigidbody2D rb;
	private string targetTag = "Enemy";

	// public GameObject fireballPrefab;

	// ability parameter 

	public float baseDamage = 1f;
	public float speed = 15f;

	//DOT parameter
	private float effectDuration = 3f;
	private Stopwatch durationTimer = new Stopwatch();
	private float baseEffectDamage = 0.5f;
	private float damageTickDuration = 1f;

	void Start () {
		rb = GetComponent <Rigidbody2D> ();
		rb.velocity = transform.up * speed;
		dot = GetComponent <DamageOverTime> ();
		Destroy (gameObject, 3f);
	}
		
	public FireBall(): base(new BasicObjectInfo (aName, aDescription))
	{
		this.AbilityBehaviors.Add (new DamageOverTime (6f, baseDamage, 1f));
	}

	/*
	public void OnAbilityuse(GameObject obj)
	{
		abilityCooldownTimer = new Stopwatch ();
		abilityCooldownTimer.Start ();
		fba = new FireBall ();

		GameObject cast= Instantiate<GameObject>(fireballPrefab);
	}
	*/

	void Update()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag (targetTag)) {
			other.gameObject.GetComponent<EnemyScript> ().hitPoints -= baseDamage;
			StartCoroutine (DOT (other.gameObject));

		}	

	}

	private IEnumerator DOT (GameObject objectHit)
	{
		durationTimer.Start ();
		while (durationTimer.Elapsed.TotalSeconds <= effectDuration) {
			objectHit.GetComponent<EnemyScript>().hitPoints -= baseEffectDamage;
			DestroyImmediate(gameObject);
			yield return new WaitForSeconds(damageTickDuration);

		}

		durationTimer.Stop ();	
		durationTimer.Reset ();
	
		yield return null;

	}
}
