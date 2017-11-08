using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilitySystem : MonoBehaviour  {

	private BasicObjectInfo objectInfo;
	private List<AbilityBehaviors> behaviors;
	private bool requiresTarget;
	private bool CastOnSelf;
	private int cooldown; //in sec
	private GameObject particleEffect;
	private float castTime;
	private float cost;
	private AbilityType type;

	public enum AbilityType
	{
		Spell,
		Melee
	}

	public AbilitySystem(BasicObjectInfo aBasicInfo)
	{
		objectInfo = aBasicInfo;
		behaviors = new List<AbilityBehaviors>();
		cooldown = 0;
		requiresTarget = false;
		CastOnSelf = false;
	}

	public AbilitySystem(BasicObjectInfo aBasicInfo, List<AbilityBehaviors> aBehaviors)
	{
		objectInfo = aBasicInfo;
		behaviors = aBehaviors;
		cooldown = 0;
		requiresTarget = false;
		CastOnSelf = false;
	}

	public AbilitySystem(BasicObjectInfo aBasicInfo, List<AbilityBehaviors> aBehaviors, 
		bool aRequireTarget, int aCooldown, GameObject aParticleE)
	{
		objectInfo = aBasicInfo;
		behaviors = new List<AbilityBehaviors>();
		behaviors = aBehaviors;
		cooldown = aCooldown;
		requiresTarget = aRequireTarget ;
		CastOnSelf = false;
		particleEffect = aParticleE;
	}

	public BasicObjectInfo AbilityInfo 
	{
		get { return objectInfo; }
	}

	public int AbilityCooldown
	{
		get{ return cooldown;}
	}

	public List<AbilityBehaviors> AbilityBehaviors
	{
		get { return behaviors; }
	}

	public void UseAbility()
	{
		
	}
}
