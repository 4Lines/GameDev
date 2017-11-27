using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMobility : MonoBehaviour {

    public int hitPoints = 10;
    public bool moving = false;
    public float speed;
    public float speed1;
    public float fireRate;
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    public float maxMp { get; set; }
    public float currentMp { get; set; }
    public float meleDamageValue = 2;
    public float tankDamage = 10;

    //"collision" LayerMask renamed to "collisionLayer". Will require collision layer to be reset in prefabs.
    public Slider healthBar;
    public Slider mpBar;
    public LayerMask collisionLayer;
    public GameObject shot;
    //public Transform shotSpawn;

    private bool manaCharge = false;
    private BoxCollider2D bc;
	private Rigidbody2D rb;
	private float nextFire;
    private float mpOrbPickupAmount = 10.0f;

    Animator anim;
   
    public float fRadius = 3.0f;
    public float ButtonCooler= 0.5f ; // Half a second before reset
    public int ButtonCount = 0;
    // Use this for initialization
    void Start () {
        bc = GetComponent<BoxCollider2D>();
		rb = GetComponent <Rigidbody2D> ();
        anim = GetComponent <Animator> ();

        maxHealth = 50;
        currentHealth = maxHealth;
        maxMp = 100;
        currentMp = maxMp;
        Environment.instance.setIntCurrentMp(currentMp);

        mpBar.value = calculateMp();
        healthBar.value = calculateHealth();
	}

    float calculateMp()
    {
        currentMp = Environment.instance.getCurrentMp();
        return currentMp / maxMp;
    }

   

    float calculateHealth()
    {
        return currentHealth / maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Bullet"))
        {
            hitPoints = hitPoints - 1;

        }
        if (collision.collider.gameObject.CompareTag("tank"))
        {
            currentHealth -= tankDamage;
            healthBar.value = calculateHealth();
            Vector3 translateVector = Vector3.Normalize(transform.position - collision.collider.transform.position) * 20f;
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, translateVector, (speed * Time.deltaTime), collisionLayer);
            if(raycast.transform == null)
                transform.Translate(translateVector * speed * Time.deltaTime, Space.World);

        }

        //Melee Damage Changed 11/26/2017
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= meleDamageValue;
            healthBar.value = calculateHealth();
            Vector3 translateVector = Vector3.Normalize((transform.position + (Vector3)bc.offset) - collision.collider.transform.position)*2.5f;
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, translateVector, (speed * Time.deltaTime), collisionLayer);
            if(raycast.transform == null)
                transform.Translate(translateVector * speed * Time.deltaTime, Space.World);
            
        }
        if (collision.collider.gameObject.CompareTag("enemyBullet"))
        {
            currentHealth -= meleDamageValue;
            healthBar.value = calculateHealth();
           /* Vector3 translateVector = Vector3.Normalize((transform.position - new Vector3(0f,.01f,0f)) - collision.collider.transform.position) * 1f;
            transform.Translate(translateVector * speed * Time.deltaTime, Space.World);
            Debug.Log(transform.position + " " + collision.collider.transform.position + " " + translateVector);
            */
        }
        Debug.Log(hitPoints);
        if (collision.collider.gameObject.CompareTag("HealthOrb"))
        {
            currentHealth += 2;
            healthBar.value = calculateHealth();
            Debug.Log("Pickup health");
            Debug.Log(currentHealth);

        }
        if (collision.collider.gameObject.CompareTag("MpOrb"))
        {
            Environment.instance.mpOrbPickup(mpOrbPickupAmount);
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (!Environment.instance.isDoingSetup())
        {
            movement();
            mpBar.value = calculateMp();

            float input_x = Input.GetAxisRaw("Horizontal");
            float input_y = Input.GetAxisRaw("Vertical");

            bool isWalking = (Mathf.Abs(input_x) + Mathf.Abs(input_y)) > 0;

            anim.SetBool("isWalking", isWalking);
            if (isWalking)
            {
                anim.SetFloat("x", input_x);
                anim.SetFloat("y", input_y);
            }

            if(currentHealth <= 0)
            {
                Environment.instance.gameOver();
            }
            
            if(currentMp <= 0)
            {

            }

            if(Environment.instance.getLevelUpReady())
            {
                //do level up stuff here
                Environment.instance.setLevelUpReady(false);
            }
        }
    }

	void FixedUpdate ()
    {
        /*
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(shotSpawn.position - mousePosition, Vector3.forward);

        shotSpawn.rotation = rot;
        shotSpawn.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z); 
     
    */
        
    }

     
    void movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position +(Vector3)bc.offset, Vector2.up, (speed * Time.deltaTime), collisionLayer);
            
            if (raycast.transform == null)
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.down, (speed * Time.deltaTime), collisionLayer);

            if (raycast.transform == null)
                transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.left, (speed * Time.deltaTime), collisionLayer);
          
            if (raycast.transform == null)
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)bc.offset, Vector2.right, (speed * Time.deltaTime), collisionLayer);
            
            if (raycast.transform == null)
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey (KeyCode.D) != true && Input.GetKey(KeyCode.A) != true && Input.GetKey(KeyCode.S) != true && Input.GetKey(KeyCode.W) != true)
        {
            moving = false;
        }
        /*
        if (Input.anyKeyDown)
        {

            if (ButtonCooler > 0 && ButtonCount == 1)
            {
                if(Input.GetKey(KeyCode.D))
                {
                    transform.position = new Vector3(transform.position.x+1f, transform.position.y + 1f, transform.position.z);
                    Debug.Log("double");
                }
            }
            else
            {
                ButtonCooler = 0.5f;
                ButtonCount += 1;
            }
        }

        if (ButtonCooler > 0)
        {

            ButtonCooler -= 1 * Time.deltaTime;

        }
        else
        {
            ButtonCount = 0;
        }
        */
    }
}
