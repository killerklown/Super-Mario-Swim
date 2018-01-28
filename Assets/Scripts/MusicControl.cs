using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour {

    public AudioSource deadSound;
    public AudioSource seaSound;

    public GameObject mario;
    private Bird marioscript;
    private bool playedDeath = false;

	// Use this for initialization
	void Start () {
        marioscript = mario.GetComponent<Bird>();
        if (!marioscript.isDead)
        {
            seaSound.Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
        marioscript = mario.GetComponent<Bird>();
        if(marioscript.isDead && !playedDeath)
        {
            seaSound.volume = 0.0f;
            deadSound.Play();
            playedDeath = true;
        }
    }
}
