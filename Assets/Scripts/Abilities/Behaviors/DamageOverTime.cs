using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DamageOverTime : AbilityBehaviors {

	private EnemyScript enemy;
	private const string aName = "Damage Over Time";
	private const string aDescription = "Spell damage is applied in an interval of time";
	private const BehaviorStartTimes startTime = BehaviorStartTimes.Beginning;

	private float effectDuration;
	private Stopwatch durationTimer = new Stopwatch();
	private float baseEffectDamage;
	private float damageTickDuration;

	public DamageOverTime(float duration, float baseDamage, float quantum) 
		: base(new BasicObjectInfo(aName, aDescription), startTime)
	{
		effectDuration = duration;
		baseEffectDamage = baseDamage;
		damageTickDuration = quantum;
	}

	public override void PerformBehavior(GameObject playerObject, GameObject objectHit)
	{
		StartCoroutine (DOT(objectHit));

	}

	private IEnumerator DOT (GameObject objectHit)
	{
		durationTimer.Start ();
		while (durationTimer.Elapsed.TotalSeconds <= effectDuration) {
			objectHit.GetComponent<EnemyScript>().hitPoints -= baseEffectDamage;

			yield return new WaitForSeconds(damageTickDuration);
		}
		durationTimer.Stop ();	
		durationTimer.Reset ();

		yield return null;

	}
}
