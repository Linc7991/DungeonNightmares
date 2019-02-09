using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Slider musicSlider;
    public Slider sfxSlider;

	// Use this for initialization
	void Awake () {
        musicSlider.value = MusicPlayer.musicVolume;
        sfxSlider.value = MusicPlayer.sfxVolume;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
