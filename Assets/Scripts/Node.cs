using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>{
    public bool walkable;
    public Vector2 worldPos;
    public Node parent;
    int heapIndex;

    public int gCost;
    public int hCost;

    public int tileX;
    public int tileY;

    public Node (bool _walkable, Vector2 _worldPos, int _tileX, int _tileY)
    {
        walkable = _walkable;
        worldPos = _worldPos;
        tileX = _tileX;
        tileY = _tileY;
    }

    public int FCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node compareNode)
    {
        int compare = FCost.CompareTo(compareNode.FCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(compareNode.hCost);
        }
        return -compare;
    }

}
