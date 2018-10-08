using System.Collections;
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
    }

    private void Update()
    {
        moveSmoothly();
        DestroyIfAtEnd();
    }

    private void moveSmoothly()
    {
        float step = speed * Time.deltaTime;

        Vector3 target = new Vector3(
                wayPoint.transform.position.x,
                12f,  // Height of enemy !Important!
                wayPoint.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, step);

        float distance = Vector3.Distance(transform.position, target);
        if (distance <= 0.1)
        {
            // Jump to next position in list
            if (wayPointPosition < path.Count - 1)
            {
                wayPointPosition += 1;
                wayPoint = path[wayPointPosition];
            }
        }
    }

    private void DestroyIfAtEnd()
    {
        Vector3 enemy = transform.position;
        Vector3 end = path[path.Count - 1].transform.position;
        enemy.y = 0;
        end.y = 0;

        float distanceFromEnd = Vector3.Distance(end, enemy);

        if (distanceFromEnd < 1f)
        {
            Destroy(gameObject, 0.25f);
        }
    }

 
}
