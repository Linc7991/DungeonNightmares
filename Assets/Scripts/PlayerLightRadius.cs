using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightRadius : MonoBehaviour {


	SpriteMask playerLightMask;
	Transform thisTransform;

	public float maxLightSizeX;
	public float maxLightSizeY;


	public float curLightRadius;

	public float decreaseInterval = .2f;
	public float minimumThreshhold = 0f; //Minimum size the light radius will be

    bool triggeredAngry = false;

	// Use this for initialization
	void Start () {
		thisTransform = this.GetComponent<Transform> ();
		maxLightSizeX = transform.localScale.x;
		maxLightSizeY = transform.localScale.y;

	}
	
	// Update is called once per frame
	void Update () {

		if (transform.localScale.x >= minimumThreshhold && transform.localScale.y >= minimumThreshhold)
		{
			transform.localScale -= new Vector3 (decreaseInterval * Time.deltaTime, decreaseInterval * Time.deltaTime, 0);
			curLightRadius = transform.localScale.x;
			if (triggeredAngry)
            {
                GameObject.FindGameObjectWithTag("Nightmare").GetComponent<Nightmare>().angry = false;
                GameObject.FindGameObjectWithTag("Nightmare").GetComponent<Nightmare>().ToggleEyes(false);
            }
		}
		else if (!triggeredAngry)
		{
            GameObject.FindGameObjectWithTag("Nightmare").GetComponent<Nightmare>().angry = true;
            GameObject.FindGameObjectWithTag("Nightmare").GetComponent<Nightmare>().ToggleEyes(true);
            triggeredAngry = true;
		}
		curLightRadius = transform.localScale.x;
		Debug.Log (curLightRadius);
			
	}

	//Refills lantern, radius returns to mx side
	public void Refill()
	{
		transform.localScale = new Vector3 (maxLightSizeX, maxLightSizeY, 0);
	}
}
