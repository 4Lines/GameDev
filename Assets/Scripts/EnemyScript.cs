using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public float speed;
	public Transform target;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float chaseRange;
	public Transform[] patrolPoints;
	public float hitPoints = 10f;

	private float nextFire;
	private Rigidbody2D rb;


	Transform currentPatrolPoint;
	int currentPatrolIndex;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		currentPatrolIndex = 0;
		currentPatrolPoint = patrolPoints [currentPatrolIndex];
	}

	void Update ()
	{
		float distanceToTarget = Vector3.Distance (transform.position, target.position);
		if (distanceToTarget < chaseRange) {
			Chase ();
		} else {
			Patrolling ();
		}

		if (hitPoints <= 0f) // die
			Destroy (gameObject);
	}

	void Chase() //Chase Player AI
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}

		Vector3 targetDirection = target.position - transform.position;
		float chaseAngle = Mathf.Atan2 (targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
		Quaternion q2 = Quaternion.AngleAxis (chaseAngle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, q2, 180);

		transform.Translate (Vector3.up * Time.deltaTime * speed);
	}

	void Patrolling() //Enemy Patrolling AI
	{
		transform.Translate (Vector3.up * Time.deltaTime * speed);
		if (Vector3.Distance (transform.position, currentPatrolPoint.position) < 0.1f) {
			if (currentPatrolIndex + 1 < patrolPoints.Length) {
				currentPatrolIndex++;
			} else {
				currentPatrolIndex = 0;
			}
			currentPatrolPoint = patrolPoints [currentPatrolIndex];
		}
		Vector3 patrolPointDirection = currentPatrolPoint.position - transform.position;
		float angle = Mathf.Atan2 (patrolPointDirection.y, patrolPointDirection.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 180f);
	}
	
}
