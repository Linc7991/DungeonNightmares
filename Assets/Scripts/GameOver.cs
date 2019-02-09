using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject cover;
    public GameObject nm;
    public GameObject back;

    bool started = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (started == true)
        {
            cover.GetComponent<Image>().color = new Color(0, 0, 0, cover.GetComponent<Image>().color.a - 0.005f);
            if (cover.GetComponent<Image>().color.a <= 0)
            {
                cover.SetActive(false);
                nm.GetComponent<Animator>().enabled = true;

            }
        }
    }

    public void Begin()
    {
        cover.SetActive(true);
        back.SetActive(true);
        StartCoroutine(WaitBlank());
    }

    IEnumerator WaitBlank()
    {
        yield return new WaitForSeconds(3);
        started = true;

    }


}
