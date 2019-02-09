using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorCounter : MonoBehaviour {

    int curFloor;
    public Text floorText;

	// Use this for initialization
	void Start () {
        curFloor = 1;
        floorText.text = "FLOOR: " + curFloor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextFloor()
    {
        curFloor++;
        floorText.text = "FLOOR: " + curFloor;
    }
}
