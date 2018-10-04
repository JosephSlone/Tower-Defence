using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] Transform neutralTarget;
    [SerializeField] float attackRange = 75f;
    [SerializeField] GameObject laser;
	
	// Update is called once per frame
	void Update () {

        LookAtEnemy();
		
	}

    private void LookAtEnemy()
    {
        if(targetEnemy == null) {
            objectToPan.LookAt(neutralTarget);
            laser.SetActive(false);
            return;
        }

        float distance  = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);

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
