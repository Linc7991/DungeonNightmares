using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBox : MonoBehaviour {

    PlayerController player;
    public GameObject key;

    bool playAudio = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        else if (player.keys > 0)
        {
            key.SetActive(true);
            if (!playAudio)
            {
                playAudio = true;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            key.SetActive(false);
            playAudio = false;
        }
	}
}
