﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public List<Waypoint> path;

    Waypoint wayPoint;
    float speed = 10f;
    int wayPointPosition = 0;

	// Use this for initialization
	void Start ()
    {
        Pathfinder pathFinder = FindObjectOfType<Pathfinder>();
        path = pathFinder.GetPath();
        wayPoint = path[wayPointPosition];

        //StartCoroutine(FollowPath(path));

    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {

            transform.position = new Vector3(
                waypoint.transform.position.x,
                12f,
                waypoint.transform.position.z);

            // transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        
        Vector3 target = new Vector3(
                wayPoint.transform.position.x,
                12f,
                wayPoint.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, step);

        float distance = Vector3.Distance(transform.position, target);
        if (distance <= 0.1)
        {
            // Jump to next position in list
            if (wayPointPosition  < path.Count-1)
            {
                wayPointPosition += 1;
                wayPoint = path[wayPointPosition];
            }
        }

    }
}
