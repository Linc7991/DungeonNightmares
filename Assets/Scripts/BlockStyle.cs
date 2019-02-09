using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStyle : MonoBehaviour {

    SpriteRenderer sprite;

    public Sprite frontFace;
    public Sprite hiddenFace;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();

        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, 0.1f);
        if (hit2D.collider != null && hit2D.collider.tag == "Block")
        {
            sprite.sprite = hiddenFace;
        }
        else
        {
            sprite.sprite = frontFace;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
