using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare : MonoBehaviour {

    FloorGen floorGen;
    Pathfinding pathFinder;
    Grid grid;
    GameObject gameOver;

    public bool angry = false;
    public bool alerted = false;
    bool countingDown = false;
    bool paused = false;
    int step = 0;
    public Vector3 direction = new Vector3(0, -1, 0);
    public Vector3 targetPosition;
    Vector2 nextPosition;
    List<Vector2> path = new List<Vector2>();

	AudioSource source;
	public AudioClip[] monsterSounds;
	int rand;

	// Use this for initialization
	void Awake () {
        floorGen = GameObject.Find("FloorGen").GetComponent<FloorGen>();
        pathFinder = GameObject.Find("Grid").GetComponent<Pathfinding>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        gameOver = GameObject.Find("GameOver");

		source = this.GetComponent<AudioSource> ();

        targetPosition = transform.position;

		//Values may need to be tweaked
		InvokeRepeating ("PlaySound", 3, 15);
			
	}

    // Update is called once per frame
    void Update()
    {
        if (!alerted && Vector3.Distance(targetPosition, transform.position) < 0.1)
        {
            if (!paused)
            {
                StartCoroutine(Pause());
                paused = true;
            }

        }
            if (path != null && Vector2.Distance(transform.position, path[step]) > 0f)
            {
                //Debug.Log("I will move to the next step.");
                if (alerted)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[step], Time.deltaTime * 0.6f);

            }
            transform.position = Vector3.MoveTowards(transform.position, path[step], Time.deltaTime * 0.5f);

            }
            else if (path != null)
            {
                //Debug.Log("I have reached my next step. Next step!");
                if (step < path.Count)
                step++;
            }

    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        //Debug.Log("Hit: " + hit.collider);
        if (hit.collider.tag == "Player" || angry)
        {
            Debug.Log("I see player!");
            StartCoroutine(GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().Alert(true));
            alerted = true;
            countingDown = false;
        }
        else if (alerted && !countingDown)
        {
            StartCoroutine(CountDown());
            countingDown = true;
        }

        if (alerted)
        {
            targetPosition = new Vector3(Mathf.Round(GameObject.Find("Player(Clone)").transform.position.x), Mathf.Round(GameObject.Find("Player(Clone)").transform.position.y), 0);
            path.Clear();
            pathFinder.FindPath(transform.position, targetPosition);
            foreach (Node n in grid.path)
            {
                path.Add(n.worldPos);
            }
            step = 0;
        }
    }

    void Wander()
    {
        Vector2 newTargetPos;

        newTargetPos = new Vector2(Mathf.Clamp(Mathf.RoundToInt(transform.position.x + Random.Range(-4, 4)), 0, floorGen.gridSize * floorGen.roomSize - 1), Mathf.Clamp(Mathf.RoundToInt(transform.position.y + Random.Range(-4, 4)), -floorGen.gridSize * floorGen.roomSize + 1, 0));

        if (!Physics2D.CircleCast(newTargetPos, 0.1f, Vector2.zero))
        {
            //Debug.DrawLine(transform.position, newTargetPos, Color.cyan, 60f);
            targetPosition = newTargetPos;
        }

    }


    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(8);
        StartCoroutine(GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().Alert(false));
        alerted = false;
        countingDown = false;
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(2);
        path.Clear();
        //Debug.Log("My target position is empty or I am close enough to the target position.");
        Wander();
        pathFinder.FindPath(transform.position, targetPosition);
        //Debug.Log("grid.Path: " + grid.path.Count);
        foreach (Node n in grid.path)
        {
            path.Add(n.worldPos);
        }
        step = 0;
        paused = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            //Do Something
            gameOver.GetComponent<GameOver>().Begin();
            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().clip = null;
            Destroy(collision.gameObject);
            Destroy(this);
        }
    }

	void PlaySound()
	{
		rand = Random.Range (0, 2);
		source.PlayOneShot (monsterSounds [rand]);
	}

    public void ToggleEyes(bool toggle)
    {
        foreach (Transform child in GetComponentInChildren<Transform>(true))
        {
            child.gameObject.SetActive(toggle);
        }
    }
}
