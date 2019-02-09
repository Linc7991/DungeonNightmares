using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMGameOver : MonoBehaviour {

    float laughCount = 0;

    public void IncreaseAnimFloat()
    {
        laughCount++;
        if (laughCount > 2)
        {
            GetComponent<Animator>().StartPlayback();
        }


    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
