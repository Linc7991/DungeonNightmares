using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {


    FloorGen floorGen;

    Node[,] grid;
    Vector2 gridSize;
    public float nodeRadius;
    public LayerMask unWalkableMask;

    float nodeDiameter;
    int tileSizeX, tileSizeY;

	// Use this for initialization
	void Awake () {
        floorGen = GameObject.Find("FloorGen").GetComponent<FloorGen>();
        gridSize = new Vector2(floorGen.gridSize * floorGen.roomSize, floorGen.gridSize * floorGen.roomSize);

        nodeDiameter = nodeRadius * 2;
        tileSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        tileSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);

        //Debug.Log(-7 / gridSize.y);
	}

    public void CreateGrid()
    {
        grid = new Node[tileSizeX, tileSizeY];


        for (int x = 0; x < tileSizeX; x++)
        {
            for (int y = 0; y < tileSizeY; y++)
            {
                Vector2 worldPoint = transform.position + (Vector3.right * (x * nodeDiameter + nodeRadius)) + (Vector3.down * (y * nodeDiameter + nodeRadius));
                bool walkable = (!Physics2D.CircleCast(worldPoint, 0.1f, Vector2.zero, 0f, unWalkableMask));
                //Debug.Log("Walkable: " + walkable);
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public int MaxSize
    {
        get
        {
            return tileSizeX * tileSizeY;
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPos)
    {
        float percentX = (worldPos.x) / (gridSize.x - 1);
        float percentY = Mathf.Abs((worldPos.y) / (gridSize.y - 1));
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((tileSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((tileSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> GetNeighbors(Node node)
    {
        //Debug.Log("Finding Neighbors...");
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0)
                {
                    //Debug.Log("X and Y are Zero.");
                    continue;
                }
                if (x != 0 && y != 0)
                {
                    //Debug.Log("X is not zero, or Y is not Zero");
                    continue;
                }
                //Debug.Log("Valid Neighbor");
                    int checkX = node.tileX + x;
                    int checkY = node.tileY + y;

                    if (checkX >= 0 && checkX < tileSizeX && checkY >= 0 && checkY < tileSizeY)
                    {
                    //Debug.Log("Adding to neighbors");
                        neighbors.Add(grid[checkX, checkY]);
                    }

            }

        }
        return neighbors;

    }


    public List<Node> path;
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(transform.position + new Vector3(gridSize.x / 2, -gridSize.y / 2, 0), new Vector3(gridSize.x, gridSize.y, 1));

    //    if (grid != null)
    //    {
    //        if (path != null)
    //        {
    //            foreach (Node n in path)
    //            {
    //                Gizmos.color = Color.cyan;
    //                Gizmos.DrawCube(n.worldPos, Vector3.one * 0.2f);
    //            }
    //        }
    //        foreach (Node n in grid)
    //        {
    //            Gizmos.color = (n.walkable) ? Color.green : Color.red;
    //            if (path != null)
    //            {
    //                if (path.Contains(n))
    //                {
    //                    Gizmos.color = Color.cyan;
    //                }
    //            }
    //            Gizmos.DrawCube(n.worldPos, Vector3.one * 0.2f);

    //        }
    //    }
    //}
}
