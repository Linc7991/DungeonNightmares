using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {


	bool isPaused  = false;
	public GameObject pauseMenu;
	public GameObject gameOverMenu;
    public GameObject settingsMenu;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            MusicPlayer.song = 0;
        }
    }

    // Use this for initialization
    void Start () {
		pauseMenu.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause"))
		{
			TogglePause ();
		}
	}

	public void Pause()
	{
		Time.timeScale = 0;
		isPaused = true;
		pauseMenu.gameObject.SetActive (true);
	}


	public void UnPause()
	{
		Time.timeScale = 1;
		isPaused = false;
		pauseMenu.gameObject.SetActive (false);

	}

	void TogglePause()
	{
		if (!isPaused)
			Pause ();
		else
			UnPause ();
	}

    public void LoadSettings()
    {
        settingsMenu.SetActive(!settingsMenu.gameObject.activeInHierarchy);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }


	public void RestartLevel()
	{
		Time.timeScale = 1;
		SceneManager.LoadSceneAsync (1);
		gameOverMenu.gameObject.SetActive (false);
	}

	public void Quit()
	{

		Time.timeScale = 1;
		pauseMenu.gameObject.SetActive (false);
		SceneManager.LoadScene ("MainMenu");
	}
}
