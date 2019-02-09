using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    public static float musicVolume;
    public static float sfxVolume;
    public static int song;
    static bool firstTime = true;
    bool curAlert = false;

    public AudioClip[] songs;

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            song = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            song = 0;
        }
        GetComponent<AudioSource>().clip = songs[song];
        if (firstTime)
        {
            firstTime = false;
            musicVolume = 1.0f;
            sfxVolume = 1.0f;
        }
        MusicVolume(musicVolume);
        GetComponent<AudioSource>().Play();
        Invoke("ChangeSFX", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Alert(bool alerted)
    {
        Debug.Log("Run alert function.");
        if (alerted && curAlert == false)
        {
            curAlert = true;
            GetComponent<AudioSource>().clip = songs[2];
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1);
            GetComponent<AudioSource>().clip = songs[1];
            GetComponent<AudioSource>().Play();
        }
        else if (!alerted)
        {
            curAlert = false;
            GetComponent<AudioSource>().clip = songs[song];
            GetComponent<AudioSource>().Play();
        }
        
    }

    public void ChangeSFX()
    {
        foreach(AudioSource source in FindObjectsOfType<AudioSource>())
        {
            if (source.gameObject.name != "MusicPlayer")
                source.volume = sfxVolume / 3;
        }
    }

    public void SFXVolume(float vol)
    {
        sfxVolume = vol;
        ChangeSFX();
    }
    
    public void MusicVolume(float vol)
    {
        musicVolume = vol;
        GetComponent<AudioSource>().volume = musicVolume / 2;
    }
}
