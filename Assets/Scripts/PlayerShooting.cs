using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
  
	float cooldownTimer;
	public float fireDelay;
	private float skillCost1;
	private float skillCost2;
	private float skillCost3;
    private int skillSelection;

    public Transform shotSpawn;
    public Transform shotSpawn2;
    public Transform shotSpawn3;
	public GameObject bulletPrefab;
    public GameObject fireball;
    public GameObject lightning;
	public Slider chargeBar;
	public Image skillIcon;
	public Sprite arcaneboltIcon;
	public Sprite fireballIcon;
	public Sprite lightningspellIcon;
    private float nextFire;

    // Use this for initialization
    void Start()
	{
		cooldownTimer = 0;
		skillSelection = 1;
		chargeBar.value = 0;
		skillCost1 = 2.0f;
		skillCost2 = 6.0f;
		skillCost3 = 10.0f;
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skillSelection = 1;
			skillIcon.sprite = arcaneboltIcon;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skillSelection = 2;
			skillIcon.sprite = fireballIcon;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skillSelection = 3;
			skillIcon.sprite = lightningspellIcon;
        }

        cooldownTimer -= Time.deltaTime;
        var pos = Input.mousePosition;
        pos.z = transform.position.z - Camera.main.transform.position.z;
        pos = Camera.main.ScreenToWorldPoint(pos);

        var q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
        Vector3 offsetPosition = q * new Vector3(0.1f, 0f, 0f);
        //Debug.Log("Offset: " + offsetPosition);

        offsetPosition = transform.position;
        //Debug.Log("Player: " + transform.position);
        //Debug.Log("Shot: " + offsetPosition);

		//arcanebolt
		if (Input.GetMouseButton (0) && cooldownTimer <= 0 && Environment.instance.getWhichSkill () == 1 && Environment.instance.getmanaChargeState () == false && skillSelection == 1) {
			cooldownTimer = fireDelay;
			Instantiate (bulletPrefab, offsetPosition, shotSpawn.rotation);
			Environment.instance.setCurrentMpAfterSkill (skillCost1);
		}

		if (Input.GetMouseButton (1) && cooldownTimer <= 0 && Environment.instance.getWhichSkill () == 1 && Environment.instance.getmanaChargeState () == false && skillSelection == 1) {
			cooldownTimer = fireDelay;
			if (chargeBar.value < 100) {
				chargeBar.value += 50;
			}

		} else if (Input.GetMouseButtonUp (1) && Environment.instance.getWhichSkill () == 1 && Environment.instance.getmanaChargeState () == false && skillSelection == 1 && chargeBar.value == 100) {
			for (int i = 0; i < 3; i++) {
				Instantiate (bulletPrefab, offsetPosition, shotSpawn.rotation);
			}
			chargeBar.value = 0;
			Environment.instance.setCurrentMpAfterSkill (skillCost2);
		} 


		//fireball
		if (Input.GetMouseButton(0) && cooldownTimer <= 0 && Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false && skillSelection == 2)
        {
            cooldownTimer = fireDelay;
            offsetPosition = transform.position;
            Instantiate (fireball, offsetPosition, shotSpawn.rotation);
            Environment.instance.setCurrentMpAfterSkill(skillCost1);

		}

		if (Input.GetMouseButton(1) && cooldownTimer <= 0 && Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false  && skillSelection == 2)
        {
            cooldownTimer = fireDelay;
            if (fireball.transform.localScale.x < 13)
            {
                fireball.transform.localScale += new Vector3(2, -2, 0);
                fireball.GetComponent<FireBall>().baseDamage += 0.5f;
				chargeBar.value += 20;
            }
				
        }
		else if (Input.GetMouseButtonUp(1) &&Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false && skillSelection == 2)
        {
            Instantiate(fireball, offsetPosition, q);
            fireball.transform.localScale = new Vector3(3, -3, 1);
            fireball.GetComponent<FireBall>().baseDamage = 1f;
			chargeBar.value = 0;
            Environment.instance.setCurrentMpAfterSkill(skillCost2);
        }

		//lightspell
		if (Input.GetMouseButton(0) && cooldownTimer <= 0 && Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false && skillSelection == 3)
        {
            cooldownTimer = fireDelay;
            Instantiate(lightning, offsetPosition, shotSpawn.rotation);
            Environment.instance.setCurrentMpAfterSkill(skillCost1);
		} 

		if (Input.GetMouseButton(1) && cooldownTimer <= 0 && Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false  && skillSelection == 3)
		{
			cooldownTimer = fireDelay;
			if (chargeBar.value < 100)
			{
				chargeBar.value += 35;
			}

		}
		else if (Input.GetMouseButtonUp(1) &&Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false && skillSelection == 3 && chargeBar.value == 100)
		{
			Instantiate(lightning, offsetPosition, shotSpawn.rotation);
			Instantiate(lightning, offsetPosition, shotSpawn2.rotation);
			Instantiate(lightning, offsetPosition, shotSpawn3.rotation);
			chargeBar.value = 0;
			Environment.instance.setCurrentMpAfterSkill(skillCost3);
		}
    }

    void FixedUpdate()
    {
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
    }



}
