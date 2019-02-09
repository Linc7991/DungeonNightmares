using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    static GameObject player;
    FloorGen floorGen;

    Vector3 origin = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
        floorGen = GameObject.Find("FloorGen").GetComponent<FloorGen>();
        FindCameraTarget();
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, 2f, (floorGen.gridSize * floorGen.roomSize) - 3), Mathf.Clamp(player.transform.position.y, -(floorGen.gridSize * floorGen.roomSize) + 3, -2f), -10);

        }
        else
        {
            FindCameraTarget();
        }

    }
    
    public static void FindCameraTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
