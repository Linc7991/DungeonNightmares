using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    GameObject nm;

	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (nm == null)
        {
            Debug.Log("LOOKING FOR PLAYER");
            nm = GameObject.FindGameObjectWithTag("Nightmare");
        }
        transform.position = nm.transform.position - nm.GetComponent<Nightmare>().direction * 0.8f;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().keys++;
            Destroy(GetComponentInChildren<Transform>().gameObject);
            Destroy(this);
        }
    }
}
