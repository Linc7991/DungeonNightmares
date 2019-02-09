using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

    FloorGen floorGen;
    AudioSource nextFloor;

    public AudioClip open;
    public AudioClip locked;

	// Use this for initialization
	void Awake () {
		floorGen = GameObject.Find("FloorGen").GetComponent<FloorGen>();
        nextFloor = GameObject.Find("NextFloor").GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.tag == "Player" && other.GetComponent<PlayerController>().keys > 0)
        {
            
            GameObject.Find("FloorCounter").GetComponent<FloorCounter>().NextFloor();
            nextFloor.clip = open;
            nextFloor.Play();
            floorGen.Generate(floorGen.gridSize);
        }
        else if (other.tag == "Player")
        {
            nextFloor.clip = locked;
            nextFloor.Play();
        }
    }
}
