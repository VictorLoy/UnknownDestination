//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//09 Dec 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>{

    public bool movable;
    public Vector3 worldPosition;
    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;
    public Node parent;
    int heapIndex;

    public Node(bool _movable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        movable = _movable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    //fCost is always g + h therefore this can be done
    public int fCost
    {
        get { return gCost + hCost; }
    }

    //Allows our fCost to be the index
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

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if(compare == 0)
        {
            //Then compare the hCost
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
