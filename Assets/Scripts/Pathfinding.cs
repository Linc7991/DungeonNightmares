using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pathfinding : MonoBehaviour {

    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {

    }

    public void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        //Debug.Log("Finding Path...");
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        //Debug.Log("Start Node: " + startNode.tileX + " " + startNode.tileY);
        //Debug.Log("Target Node: " + targetNode.tileX + " " + targetNode.tileY);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet.RemoveFirst();
            closedSet.Add(node);

            if (node == targetNode)
            {
                //Debug.Log("Found path!");
                Retrace(startNode, targetNode);
                return;
            }

            foreach (Node neighbor in grid.GetNeighbors(node))
            {
                //Debug.Log("Neighbor: " + neighbor.tileX + " " + neighbor.tileY);
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                {
                    continue;
                }
                int newMovementCostToNeighbor = node.gCost + GetDistance(node, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = node;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                        openSet.UpdateItem(neighbor);
                    }
                }
            }

        }

    }

    void Retrace(Node startNode, Node endNode)
    {
        //Debug.Log("Retracing");
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
        return;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        return Mathf.Abs(nodeA.tileX - nodeB.tileX) + Mathf.Abs(nodeA.tileY - nodeB.tileY);
    }
}
