using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private GameObject theDoor;
    private BoxCollider2D boxCollider;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        boxCollider = GetComponent<BoxCollider2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            animator.enabled = true;
            animator.Play("DoorAnim", 0, 0f);
            Invoke("letThrough", 0.5f);
        }
            
    }

    private void letThrough()
    {
        gameObject.layer = 0;
        Invoke("closeDoor", 0.5f);
    }

    private void closeDoor()
    {
        gameObject.layer = 8;
        animator.SetTrigger("DoorRev");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
