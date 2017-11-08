using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBehaviors : MonoBehaviour {

	private BasicObjectInfo objectInfo;
	private BehaviorStartTimes startTime;

	public AbilityBehaviors(BasicObjectInfo basicInfo, BehaviorStartTimes sTime)
	{
		objectInfo = basicInfo;
		startTime = sTime;
	}

	public enum BehaviorStartTimes
	{
		Beginning,
		Middle,
		End
	}

	public virtual void PerformBehavior(Vector3 startPosition)
	{
		Debug.LogWarning ("NEED TO ADD BEHAVIOR");
	}

	public virtual void PerformBehavior(GameObject playerObject, GameObject objectHit)
	{
		Debug.LogWarning ("NEED TO ADD BEHAVIOR");
	}

	public BasicObjectInfo AbilityBehaviorInfo {
		get { return objectInfo; }
	}

	public BehaviorStartTimes AbilityBehaviorStartTime
	{
		get{ return startTime; }
	}

}
