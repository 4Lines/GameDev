using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAndSounds : MonoBehaviour {

    public AudioSource soundEffectSource;
    public AudioSource musicSource;
    public static MusicAndSounds instance = null;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
