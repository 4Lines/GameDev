using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour {

	public float speed;
	public Transform target;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float chaseRange;
	public Transform[] patrolPoints;
    public int hitPoints = 4;

	private float nextFire;
	private Rigidbody2D rb;

	Transform currentPatrolPoint;
	int currentPatrolIndex;

    private Transform t;
    private Transform player;
    private float range = 10.0f;

    //New boolean added for experience points.
    private bool expGranted = false;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D> ();
		currentPatrolIndex = 0;
		currentPatrolPoint = patrolPoints [currentPatrolIndex];
        t = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Bullet"))
        {
            hitPoints = hitPoints - 1;
            Vector3 translateVector = Vector3.Normalize(transform.position - collision.collider.transform.position) * 5f;
            transform.Translate(translateVector * speed * Time.deltaTime, Space.World);
            Debug.Log(hitPoints);

        }
    }

    private float Distance()
    {
        return Vector3.Distance(t.position, player.position);
    }

    // EXP addition added to death 11/26/2017.
    void Update()
    {

        //Chase Player AI
        if (!Environment.instance.isDoingSetup())
        {
            if (Distance() < range)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //if (distanceToTarget < chaseRange)
                //{
                Vector3 targetDirection = target.position - transform.position;
                float chaseAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                Quaternion q2 = Quaternion.AngleAxis(chaseAngle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q2, 180);

                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    Instantiate(shot, shotSpawn.position, Quaternion.RotateTowards(transform.rotation, q2, 180));
                }



                //transform.Translate(Vector3.up * Time.deltaTime * speed);

                //}
                //else
                //{

                //Enemy Patrolling AI
                Vector3 Direction = currentPatrolPoint.position - transform.position;
                Direction = Quaternion.Inverse(transform.rotation) * Direction;
                Direction.Normalize();
                Direction = Direction * 5;
                transform.Translate(Direction * Time.deltaTime * speed);
                if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.1f)
                {
                    if (currentPatrolIndex + 1 < patrolPoints.Length)
                    {
                        currentPatrolIndex++;
                    }
                    else
                    {
                        currentPatrolIndex = 0;
                    }
                    currentPatrolPoint = patrolPoints[currentPatrolIndex];
                }
                Vector3 patrolPointDirection = currentPatrolPoint.position - transform.position;
                float angle = Mathf.Atan2(patrolPointDirection.y, patrolPointDirection.x) * Mathf.Rad2Deg - 90f;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);

                //}
                if (!expGranted)
                {
                    Environment.instance.giveEXP(3);
                    expGranted = true;
                }
                if (hitPoints < 1)
                {
                    Destroy(gameObject, 0.5f);
                }
            }
        }
    }

		/*
	void FixedUpdate () {
		float z = Mathf.Atan2 ((target.transform.position.y - transform.position.y), 
			          (target.transform.position.x - transform.position.x)) *
		          Mathf.Rad2Deg - 90;
		transform.eulerAngles = new Vector3 (0, 0, z);
		rb.AddForce (gameObject.transform.up * speed);
	}
	*/
	
}
