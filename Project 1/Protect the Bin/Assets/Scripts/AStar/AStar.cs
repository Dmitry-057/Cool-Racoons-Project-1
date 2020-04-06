﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.Linq;

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


    public static void GetPath( Point start, Point goal )
    {
        if ( nodes == null)
        {
            CreateNodes();
        }

        //creates open list for AStar Algor
        HashSet<Node> openList = new HashSet<Node>();

        //creates closed list for AStar Algor
        HashSet<Node> closedList = new HashSet<Node>();

        Stack<Node> finalPath = new Stack<Node>();



        Node currentNode = nodes[start];

        //Step 1 Adds start node to open list
        openList.Add(currentNode);

        while ( openList.Count > 0 )
        {
            //Step 2 Nested for loops used to examine the squares around the current node
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y<= 1; y++)
                {
                    
                    Point neighbourPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);
                    
                    
                    if (LevelManager.Instance.InBounds(neighbourPos) &&  LevelManager.Instance.Tiles[neighbourPos].WalkAble && neighbourPos != currentNode.GridPosition)
                    {
                        int gCost = 0;

                        if ( Math.Abs( x - y ) == 1)
                        {
                            gCost = 10;
                        }
                        else 
                        {
                            gCost = 14;
                        }
                        //Step 3 adds neighbors to open list
                        Node neighbour = nodes[neighbourPos];



                        if ( openList.Contains(neighbour))
                        {
                            if ( currentNode.G + gCost < neighbour.G )//Step 9.4
                            {
                                neighbour.CalcValues( currentNode, nodes[goal], gCost );
                            }
                        }
                        else if ( !closedList.Contains(neighbour) )//9.1
                        {
                            openList.Add(neighbour); //9.2
                            neighbour.CalcValues(currentNode, nodes[goal], gCost);//9.3
                        }
                    } 
                }
            }

            //Step 5 & 8
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if ( openList.Count > 0 )//Step 7
            {
                //Sorts list by F value and selects first one
                currentNode = openList.OrderBy( n => n.F).First();
            }

            if ( currentNode == nodes[goal] )
            {

                while (currentNode.GridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }

                break;
            }

        }

        
        //This is only for debugging needs to be removed later
        GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList, closedList, finalPath);
    }
}
