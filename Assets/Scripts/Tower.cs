using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {


    // Parameters of each tower

    [SerializeField] Transform objectToPan;    
    [SerializeField] Transform neutralTarget;
    [SerializeField] float attackRange = 75f;
    [SerializeField] GameObject laser;


    // State of each tower
    Transform targetEnemy;

    // Update is called once per frame
    void Update () {

        SetTargetEnemy();

        LookAtEnemy();
		
	}

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<Enemy>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (Enemy testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;

    }

    private Transform GetClosest(Transform closestEnemy, Transform testEnemy)
    {
        float distanceToClosest = Vector3.Distance(gameObject.transform.position, closestEnemy.transform.position);
        float distanceToOther = Vector3.Distance(gameObject.transform.position, testEnemy.transform.position);

        if (distanceToClosest < distanceToOther)
        {
            return closestEnemy;
        }
        else
        {
            return testEnemy;
        }
    }

    private void LookAtEnemy()
    {
        if (targetEnemy == null)
        {
            objectToPan.LookAt(neutralTarget);
            laser.SetActive(false);
            return;
        }

        FireAtEnemy();

    }

    private void FireAtEnemy()
    {
        float distance = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);

        if (distance <= attackRange)
        {
            objectToPan.LookAt(targetEnemy);
            laser.SetActive(true);
        }
        else
        {
            objectToPan.LookAt(neutralTarget);
            laser.SetActive(false);
        }
    }


}
