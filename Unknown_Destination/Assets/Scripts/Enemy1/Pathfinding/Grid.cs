//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//09 Dec 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public bool DrawGizmos;
    public Vector2 gridWorldSize;
    public Node[,] grid;
    public float nodeRadius;
    private float nodeDiameter;
    private int gridSizeX, gridSizeY;
    public LayerMask unMovable;

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }

    //Creates the grid to be used with A* pathfinding algorithm. 
    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        //Find grid starting from the bottom left of the drawn worldGrid
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up* (y* nodeDiameter + nodeRadius);
                //If a point is touching the unmovable layer it is marked as so
                bool movable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unMovable));
                //Populate grid with walkable positions
                grid[x, y] = new Node(movable, worldPoint, x, y);
            }
        }
    }

    //Looks at the surrounding nodes and returns a list with their x and y values
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //Skip beacause this is the center node and not a neighbour
                if(x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    //Returns the cooridinates of a node to world position
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    //Used to help visualize the grid that will be used to implement the A* algorithm
    private void OnDrawGizmos()
    {
        if(DrawGizmos)
        {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));
           if (grid != null)
           {
           foreach (Node n in grid)
           {
              if (n.movable)
              {
                 Gizmos.color = Color.white;
              }
              else
              {
                Gizmos.color = Color.red;
              }
              Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));

           }
        }
    }
}
}
