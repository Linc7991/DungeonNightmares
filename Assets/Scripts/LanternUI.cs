using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanternUI : MonoBehaviour {

	GameObject playerSpriteMask;

	float maxRadiusTransform;
	float curRadiusTransform;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (playerSpriteMask == null)
        {
            playerSpriteMask = GameObject.Find("PlayerSpriteMask");
            maxRadiusTransform = playerSpriteMask.GetComponent<PlayerLightRadius>().maxLightSizeX - 6;
        }
        else
        {
		curRadiusTransform = playerSpriteMask.GetComponent<PlayerLightRadius>().curLightRadius - 6;
		//curRadiusTransform = curRadiusTransform / maxRadiusTransform;
		//Debug.Log (curRadiusTransform);
		GetComponent<Image>().fillAmount = Mathf.Round((curRadiusTransform / maxRadiusTransform) * 100) / 100;
        }

	}
}
