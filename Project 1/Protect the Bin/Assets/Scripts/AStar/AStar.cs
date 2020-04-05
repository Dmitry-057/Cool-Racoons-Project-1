﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{

    private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();

        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }


    public static void GetPath( Point start )
    {
        if ( nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();

        Node currentNode = nodes[start];

        openList.Add(currentNode);

        //Nested for loops used to examine the squares around the current node
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y<= 1; y++)
            {
                
                Point neighbourPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);
                
                
                if (LevelManager.Instance.InBounds(neighbourPos) &&  LevelManager.Instance.Tiles[neighbourPos].WalkAble && neighbourPos != currentNode.GridPosition)
                {
                    Node neighbour = nodes[neighbourPos];

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }

                    neighbour.CalcValues(currentNode);
                }
                
                
            }
        }

        //This is only for debugging needs to be removed later
        GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList);
    }
}
