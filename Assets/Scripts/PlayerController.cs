using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    enum Direction { up, down, left, right};

    Direction dir = Direction.down;

    Rigidbody2D rb;
    Animator anim;
    GameObject mobile;

    public float speed = 1f;
    public int keys = 0;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
#if !UNITY_ANDROID
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            rb.velocity = Vector2.zero;
            if (dir == Direction.up)
            {
                anim.Play("PlayerIdleBack");
            }
            else if (dir == Direction.right || dir == Direction.left)
            {
                anim.Play("PlayerIdleSide");
            }
            else
            {
                anim.Play("PlayerIdleFront");
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            //Moving left
            anim.Play("Side");
            dir = Direction.left;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            //Moving right
            anim.Play("Side");
            dir = Direction.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            //Moving up
            anim.Play("Up");
            dir = Direction.up;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            //Moving down
            anim.Play("Down");
            dir = Direction.down;
        }

        if (rb.velocity.magnitude > 0 && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
        else if (rb.velocity.magnitude == 0)
        {
            GetComponent<AudioSource>().Stop();
        }
#endif
#if UNITY_ANDROID
        Debug.Log("This is Android.");
        rb.velocity = new Vector2(, Input.GetAxis("Vertical"));
        

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            rb.velocity = Vector2.zero;
            if (dir == Direction.up)
            {
                anim.Play("PlayerIdleBack");
            }
            else if (dir == Direction.right || dir == Direction.left)
            {
                anim.Play("PlayerIdleSide");
            }
            else
            {
                anim.Play("PlayerIdleFront");
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            //Moving left
            anim.Play("Side");
            dir = Direction.left;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            //Moving right
            anim.Play("Side");
            dir = Direction.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            //Moving up
            anim.Play("Up");
            dir = Direction.up;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            //Moving down
            anim.Play("Down");
            dir = Direction.down;
        }

        if (rb.velocity.magnitude > 0 && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
        else if (rb.velocity.magnitude == 0)
        {
            GetComponent<AudioSource>().Stop();
        }
#endif
    }
}
