using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprite : MonoBehaviour {

    Vector3 pos1;
    Vector3 pos2;
    Vector3 dir;
    Animator anim;
    Nightmare nm;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        nm = GetComponent<Nightmare>();
        pos1 = transform.position;
    }
    // Update is called once per frame
    void FixedUpdate () {
        pos2 = transform.position;
        dir = Vector3.Normalize(pos2 - pos1);

        if (dir.x < 0)
        {
            //Moving left
            anim.StopPlayback();
            anim.Play("Side");
            GetComponent<SpriteRenderer>().flipX = true;
            nm.direction = new Vector3(-1, 0, 0);
        }
        else if (dir.x > 0)
        {
            //Moving right
            anim.StopPlayback();
            anim.Play("Side");
            GetComponent<SpriteRenderer>().flipX = false;
            nm.direction = new Vector3(1, 0, 0);
        }
        else if (dir.y > 0)
        {
            //Moving up
            anim.StopPlayback();
            anim.Play("Up");
            nm.direction = new Vector3(0, 1, 0);
        }
        else if (dir.y < 0)
        {
            //Moving down
            anim.StopPlayback();
            anim.Play("Down");
            nm.direction = new Vector3(0, -1, 0);
        }
        else
        {
            //Stopped
            anim.StartPlayback();
        }

        pos1 = pos2;
        //Debug.Log(dir);
	}
}
