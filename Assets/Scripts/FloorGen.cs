using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGen : MonoBehaviour {

    public GameObject[] rooms;
    public GameObject borderWall;
    public int gridSize;
    public int roomSize;

    public GameObject player;
    public GameObject nightmare;
    public GameObject exit;
    public GameObject lantern;
    public GameObject key;
    public GameObject stairs;

	public GameObject darkness;
    public GameObject musicPlayer;
	// Use this for initialization
	void Start () {
        Generate(gridSize);
	}
	
    public void Generate(int dimension)
    {
        //Clear the existing layout
        Reset();

        Vector2 borderPos = new Vector2(-1, 1);
        Vector2 position = new Vector2(0, 0);

        //Generate rooms at random, left to right, up to down
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                Instantiate(rooms[Random.Range(0, rooms.Length)], position, Quaternion.identity);
                position.x = position.x + roomSize;
            }
            position.x = 0;
            position.y = position.y - roomSize;
        }

        //Generate top wall
        int borderLength = roomSize * gridSize + 1;

		Instantiate (darkness, new Vector2(-5, 5), Quaternion.identity);
		darkness.GetComponent<SpriteRenderer>().size = new Vector2 (gridSize * roomSize + 5, gridSize * roomSize + 5);

        for (int i = 0; i < borderLength; i++)
        {
            Instantiate(borderWall, borderPos, Quaternion.identity);
            borderPos.x++;
        }
        //Generate right wall
        for (int i = 0; i < borderLength; i++)
        {
            Instantiate(borderWall, borderPos, Quaternion.identity);
            borderPos.y--;
        }
        //Generate bottom wall
        for (int i = 0; i < borderLength; i++)
        {
            Instantiate(borderWall, borderPos, Quaternion.identity);
            borderPos.x--;
        }
        //Generate left wall
        for (int i = 0; i < borderLength; i++)
        {
            Instantiate(borderWall, borderPos, Quaternion.identity);
            borderPos.y++;
        }

		//Generate darkness over grid
		//Instantiate(darkness,new Vector2(0,0), Quaternion.identity);

        //Generate player start
        int playerX;
        int playerY;
        Vector2 playerSpawn = new Vector2(0, 0);
        bool openSpace = false;

        for (int i= 0; i < 100; i++)
        {
            if (openSpace == false)
            {
                playerX = Random.Range(0, gridSize * roomSize);
                playerY = Random.Range(0, gridSize * roomSize);
                playerSpawn = new Vector2(playerX, -playerY);

                //Debug.Log("PlayerSpawn: " + playerSpawn.ToString());
                //Debug.DrawLine(playerSpawn, playerSpawn + Vector2.up, Color.blue, 10f);

                if (!Physics2D.CircleCast(playerSpawn, 0.2f, Vector2.zero))
                {
                    Instantiate(player, playerSpawn, Quaternion.identity);
                    Instantiate(stairs, playerSpawn, Quaternion.identity);
                    openSpace = true;
                }
            }
            else
            {
                if (i == 99)
                {
                    Generate(gridSize);
                    return;
                }
                else
                {
                    break;
                }
            }

        }

        //Generate exit start
        int exitX;
        int exitY;
        Vector2 exitSpawn = new Vector2(0, 0);
        openSpace = false;

        for (int i = 0; i < 100; i++)
        {

            if (openSpace == false)
            {

                exitX = Random.Range(0, gridSize * roomSize);
                exitY = Random.Range(0, gridSize * roomSize);
                exitSpawn = new Vector2(exitX, -exitY);

                //Debug.Log("ExitSpawn: " + exitSpawn.ToString());
                //Debug.DrawLine(exitSpawn, exitSpawn + Vector2.up, Color.green, 10f);

                if (!Physics2D.CircleCast(exitSpawn, 0.2f, Vector2.zero) && Vector2.Distance(exitSpawn, playerSpawn) > 5f)
                {
                    Instantiate(exit, exitSpawn, Quaternion.identity);
                    openSpace = true;
                }
            }
            else
            {
                if (i == 99)
                {
                    Generate(gridSize);
                    return;
                }
                else
                {
                    break;

                }
            }
            
        }

        //Generate Nightmare start
        int nightmareX;
        int nightmareY;
        Vector2 nightmareSpawn = new Vector2(0, 0);
        openSpace = false;

        for (int i = 0; i < 100; i++)
        {
            if (openSpace == false)
            {
                nightmareX = Random.Range(0, gridSize * roomSize);
                nightmareY = Random.Range(0, gridSize * roomSize);
                nightmareSpawn = new Vector2(nightmareX, -nightmareY);

                //Debug.Log("NightmareSpawn: " + nightmareSpawn.ToString());
                //Debug.DrawLine(nightmareSpawn, nightmareSpawn + Vector2.up, Color.red, 10f);


                if (!Physics2D.CircleCast(nightmareSpawn, 0.2f, Vector2.zero) && Vector2.Distance(nightmareSpawn, playerSpawn) > 5f)
                {
                    Instantiate(nightmare, nightmareSpawn, Quaternion.identity);
                    Instantiate(key, nightmareSpawn, Quaternion.identity);
                    openSpace = true;
                }
            }
            else
            {
                if (i == 99)
                {
                    Generate(gridSize);
                    return;
                }
                else
                {
                    break;
                }
            }

        }

        //Generate Items
        int itemX;
        int itemY;
        Vector2 itemSpawn = new Vector2(0, 0);

        for (int i = 0; i < Mathf.FloorToInt(gridSize * gridSize / 3); i++)
        {
            openSpace = false;
            for (int j = 0; j < 100; j++)
            {
                if (openSpace == false)
                {
                    itemX = Random.Range(0, gridSize * roomSize);
                    itemY = Random.Range(0, gridSize * roomSize);
                    itemSpawn = new Vector2(itemX, -itemY);

                    //Debug.Log("ItemSpawn: " + itemSpawn.ToString());
                    //Debug.DrawLine(itemSpawn, itemSpawn + Vector2.up, Color.yellow, 10f);


                    if (!Physics2D.CircleCast(itemSpawn, 0.2f, Vector2.zero) && Vector2.Distance(itemSpawn, playerSpawn) > 4f)
                    {
                        Instantiate(lantern, itemSpawn, Quaternion.identity);
                        openSpace = true;
                    }
                }
                else
                {
                    if (j == 99)
                    {
                        Generate(gridSize);
                        return;
                    }
                    else
                    {
                        break;
                    }
                }

            }
        }


	


        GameObject.Find("Grid").GetComponent<Grid>().CreateGrid();
        StartCoroutine(musicPlayer.GetComponent<MusicPlayer>().Alert(false));
    }

    //Clears any objects in the scene related to the map layout
    void Reset()
    {
        List<GameObject> toDestroy = new List<GameObject>();
        toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Block"));
        toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Room"));
        toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Item"));
        toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Nightmare"));
		toDestroy.AddRange (GameObject.FindGameObjectsWithTag ("Darkness"));

        foreach(GameObject item in toDestroy)
        {
            Destroy(item);
        }
    }

    //Button to regenerate from inspector
    [InspectorButton("OnButtonClicked", ButtonWidth = 100)]
    public bool GenerateFloor;

    private void OnButtonClicked()
    {
        Generate(gridSize);
    }
}
