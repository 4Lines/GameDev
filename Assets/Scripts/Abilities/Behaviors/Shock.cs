using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Shock : AbilityBehaviors {

	private const string aName = "Tentative Shock Effect";
	private const string aDescription = "Stop an object's movement temporarily upon impact";
	private const BehaviorStartTimes startTime = BehaviorStartTimes.Beginning;

	private float effectDuration;
	private Stopwatch durationTimer = new Stopwatch();
	private float baseEffectDamage;

	public Shock(float aRadius, float duration, float baseDamage) 
		: base(new BasicObjectInfo(aName, aDescription), startTime)
	{
		effectDuration = duration;
		baseEffectDamage = baseDamage;
	}

	public override void PerformBehavior(GameObject playerObject, GameObject objectHit)
	{
		StartCoroutine (STUN(objectHit));

	}

	private IEnumerator STUN(GameObject objectHit)
	{
		durationTimer.Start ();
		while (durationTimer.Elapsed.TotalSeconds <= effectDuration) {
			objectHit.GetComponent<EnemyScript> ().shot.SetActive(false);
			objectHit.GetComponent<EnemyScript> ().speed = 0f;
		}
		durationTimer.Stop ();
		durationTimer.Reset ();

		yield return null;

	}
}
