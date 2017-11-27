using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    float cooldownTimer = 0;
    public float fireDelay = 0.1f;
    private float skillCost1 = 2.0f;
    private float skillCost2 = 10.0f;

    private int skillSelection;

    public Transform shotSpawn;
    public Transform shotSpawn2;
    public Transform shotSpawn3;
    public GameObject fireball;
    private float fireRate = 0.5f;
    private float nextFire;

    // Use this for initialization
    void Start()
    {
        skillSelection = 1;
    }

    // Update stuff placed in if statement. Will only happen if not doing dialogue. 11/26/2017.
    // Update is called once per frame
    void Update()
    {
        if (!Environment.instance.isDoingSetup())
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                skillSelection = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                skillSelection = 2;
            }

            cooldownTimer -= Time.deltaTime;
            /* Vector3 shootDir;
            shootDir = Input.mousePosition;
            shootDir.z = 0.0f;
            shootDir = Camera.main.ScreenToWorldPoint(shootDir);
            shootDir = shootDir - transform.position;
            */


            if (Input.GetButtonDown("Fire1") && cooldownTimer <= 0 && Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false && skillSelection == 1)
            {
                cooldownTimer = fireDelay;

                var pos = Input.mousePosition;
                pos.z = transform.position.z - Camera.main.transform.position.z;
                pos = Camera.main.ScreenToWorldPoint(pos);

                var q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
                Vector3 offsetPosition = q * new Vector3(0.5f, 0f, 0f);
                //Debug.Log("Offset: " + offsetPosition);

                offsetPosition = transform.position;
                //Debug.Log("Player: " + transform.position);
                //Debug.Log("Shot: " + offsetPosition);
                Instantiate(bulletPrefab, offsetPosition, q);
                //Debug.Log("FIRE");

                Environment.instance.setCurrentMpAfterSkill(skillCost1);
            }
            /*
            if (Input.GetButtonDown("Fire1") && cooldownTimer <= 0 && Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false && skillSelection == 2)
            {
                var pos = Input.mousePosition;
                pos.z = transform.position.z - Camera.main.transform.position.z;
                pos = Camera.main.ScreenToWorldPoint(pos);

                var q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
                Vector3 offsetPosition = q * new Vector3(0.5f, 0f, 0f);
                //Debug.Log("Offset: " + offsetPosition);

                offsetPosition = transform.position;
                Instantiate (fireball, offsetPosition, q);

                if (Input.GetMouseButton(1) && Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    if (fireball.transform.localScale.x < 19)
                    {
                        fireball.transform.localScale += new Vector3(2, -2, 0);
                        fireball.GetComponent<FireBall>().baseDamage += 1.5f;
                    }

                }
                else if (Input.GetMouseButtonUp(1))
                {
                    Instantiate(fireball, offsetPosition, q);
                    fireball.transform.localScale = new Vector3(5, -5, 1);
                    fireball.GetComponent<FireBall>().baseDamage = 1f;
                }
                    Environment.instance.setCurrentMpAfterSkill(skillCost2);
            }*/
        }

    }
    void FixedUpdate()
    {/*
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        Vector3 relativePos = mousePosition - shotSpawn.position;

        shotSpawn.localPosition = new Vector3(Mathf.Clamp(relativePos.x, 0, 0), Mathf.Clamp(relativePos.y, 0, 0), 0);
        shotSpawn.rotation = rot;
        shotSpawn.eulerAngles = new Vector3(0, 0, shotSpawn.eulerAngles.z);

        shotSpawn2.localPosition = new Vector3(Mathf.Clamp(relativePos.x, 0, 0), Mathf.Clamp(relativePos.y, 0, 0), 0);
        shotSpawn2.rotation = rot;
        shotSpawn2.eulerAngles = new Vector3(0, 0, shotSpawn.eulerAngles.z + 10);

        shotSpawn3.localPosition = new Vector3(Mathf.Clamp(relativePos.x, 0, 0), Mathf.Clamp(relativePos.y, 0, 0), 0);
        shotSpawn3.rotation = rot;
        shotSpawn3.eulerAngles = new Vector3(0, 0, shotSpawn.eulerAngles.z - 10);
        
    }*/
    }
}
