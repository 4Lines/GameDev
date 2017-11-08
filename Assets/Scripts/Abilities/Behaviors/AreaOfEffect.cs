using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AreaOfEffect : AbilityBehaviors {

	private EnemyScript enemy;
	private const string aName = "Area of Effect";
	private const string aDescription = "Ability affects a certain raduis of area";
	private const BehaviorStartTimes startTime = BehaviorStartTimes.End;
	private float areaRadius;
	private float effectDuration;
	private Stopwatch durationTimer = new Stopwatch();
	private float baseEffectDamage;
	private bool isOccupied;
	private float damageTickDuration;

	public AreaOfEffect(float aRadius, float duration, float baseDamage) : base(new BasicObjectInfo(aName, aDescription), startTime)
	{
		areaRadius = aRadius;
		effectDuration = duration;
		baseEffectDamage = baseDamage;
		isOccupied = false;
	}

	public override void PerformBehavior(GameObject playerObject, GameObject objectHit)
	{
		BoxCollider2D bc = this.gameObject.GetComponent<BoxCollider2D > ();
 
		/*
		if (this.GameObject.getComponent<BoxCollider2D> () == null) {
			bc = this.GameObject.AddComponent<BoxCollider2D> ();
		} else {
			bc = this.GameObject.GetComponent<BoxCollider2D> ();
		}
		*/
		bc.edgeRadius = areaRadius;
		bc.isTrigger = true;
		StartCoroutine (AOE());

	}

	private IEnumerator AOE()
	{
		durationTimer.Start();
		while (durationTimer.Elapsed.TotalSeconds <= effectDuration) {
			if (isOccupied) {
				enemy.hitPoints -= baseEffectDamage; 
			}

			yield return new WaitForSeconds (damageTickDuration);
		}

		durationTimer.Stop ();
		durationTimer.Reset ();

		yield return null;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (isOccupied) {
			enemy.hitPoints -= baseEffectDamage;
		} else {
			isOccupied = true;
		}
	
	}

	private void OnTriggerExit(Collider other)
	{
		isOccupied = false;
	}

}
