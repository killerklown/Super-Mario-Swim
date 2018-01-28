using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float upForce;
    public float upSuperForce;

    public bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    public AudioSource swimSound;
    public bool godmode;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isDead = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isDead == false)
        {
            if(Input.GetMouseButtonDown(0))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Swim");
                swimSound.Play();
            }
            if (Input.GetMouseButtonDown(1))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upSuperForce));
                anim.SetTrigger("Swim");
                swimSound.Play();
            }
        }
	}

    void OnCollisionEnter2D()
    {
        if(godmode)
        {
            return;
        }

        rb2d.velocity = Vector2.zero;
        isDead = true;
        anim.SetTrigger("Die");
        GameControl.instance.BirdDied();
    }
}
