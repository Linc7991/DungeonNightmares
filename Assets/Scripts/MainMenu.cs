using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public GameObject buttonCanvas;
	public GameObject howToPlayCanvas;
    public GameObject creditsWindow;
    public GameObject howToButon;
    public GameObject settingsWindow;
    public Image fadeOut;

	AudioSource source;

    bool fade = false;

	// Use this for initialization
	void Start () {
		buttonCanvas.gameObject.SetActive (true);

		source = GetComponent<AudioSource> ();
	}

    private void Update()
    {
        if (fade == true)
        {
            fadeOut.color = new Color(fadeOut.color.r, fadeOut.color.g, fadeOut.color.b, fadeOut.color.a + 0.01f);
            if (fadeOut.color.a >= 1)
            {
                SceneManager.LoadScene(1);
            }
        }
    }


    public void StartGame()
	{
        fadeOut.gameObject.SetActive(true);
        fade = true;
	}


	public void GoToSettings()
	{
        settingsWindow.SetActive(true);
        buttonCanvas.SetActive(false);
        howToButon.SetActive(false);
	}

	public void GoToHowToPlay()
	{
        if (!howToPlayCanvas.activeSelf)
        {
    		buttonCanvas.gameObject.SetActive (false);
    		howToPlayCanvas.gameObject.SetActive (true);

        }
        else
        {
            buttonCanvas.SetActive(true);
            howToPlayCanvas.SetActive(false);
        }
	}

	public void Back(GameObject currentWindow)
	{
        buttonCanvas.SetActive(true);
        currentWindow.SetActive(false);
        howToButon.SetActive(true);

	}

	public void GoToCredits()
	{
        creditsWindow.SetActive(true);
        buttonCanvas.SetActive(false);
        howToButon.SetActive(false);
	}

	public void PlaySound()
	{
		source.Play ();
	}

}
