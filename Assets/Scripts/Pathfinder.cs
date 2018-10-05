using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();   // todo make private

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left        
    };

    public List<Waypoint> GetPath()
    {

        if (path.Count == 0)
        {
            LoadBocks();
            BreadthFirstSearch();
            CreatePath();
        }

        return path;
    }


    private void CreatePath()
    {
        SetAsPath(endWaypoint);
        Waypoint previous = endWaypoint;        

        while (previous != startWaypoint)
        {
            previous = previous.exploredFrom;
            SetAsPath(previous);
        }

        SetAsPath(startWaypoint);
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbors();
        }

    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbors()
    {

        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = searchCenter.GetGridPos() + direction;

            if(grid.ContainsKey(neighborCoordinates))
            {
                QueueNewNeighbours(neighborCoordinates);
            }

        }
    }

    private void QueueNewNeighbours(Vector2Int neighborCoordinates)
    {
        Waypoint neighbor = grid[neighborCoordinates];
        if(neighbor.isExplored || queue.Contains(neighbor))
        {
            // do nothing
        }
        else
        {            
            queue.Enqueue(neighbor);
            neighbor.exploredFrom = searchCenter;
        }
    }

    private void LoadBocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            
            if (grid.ContainsKey(gridPos))
            {

            }
            else
            {
                grid.Add(gridPos, waypoint);
            }        
        }
    }
}
