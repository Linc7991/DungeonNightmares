using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player"))
		{
			other.gameObject.GetComponentInChildren<PlayerLightRadius> ().Refill ();	
			Destroy (this.gameObject);
		}
	}
}
