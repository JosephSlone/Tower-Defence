using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour {


    // Parameters of each tower

    [SerializeField] Transform objectToPan;    
    [SerializeField] Transform neutralTarget;
    [SerializeField] float attackRange = 50f;
    [SerializeField] GameObject laser;
    [SerializeField] float coolDownTime = 5f;
    [SerializeField] float timeToOverheat = 5f;
    [SerializeField] Image healthBar;


    // State of each tower
    Transform targetEnemy;
    AudioSource audioSource;

    float fireTime = 0f;
    float coolDown = 0f;

    public Vector3 currentPos;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coolDown = coolDownTime;
        int gridSize = 10;

        currentPos = transform.position;

        transform.position = new Vector3(Mathf.Round(currentPos.x / gridSize) * gridSize,
                                      Mathf.Round(currentPos.y ),
                                      Mathf.Round(currentPos.z / gridSize) * gridSize);
    }

    // Update is called once per frame
    void Update () {
        SetTargetEnemy();
        LookAtEnemy();
        FireAtEnemy();
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
        }
        else
        {
            objectToPan.LookAt(targetEnemy);
        }
    }

    private void FireAtEnemy()
    {
        float distance = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);

        if (fireTime < timeToOverheat) {

            healthBar.fillAmount = fireTime/timeToOverheat; 

            if (distance <= attackRange)
            {
                laser.SetActive(true);
                if (!audioSource.isPlaying)
                {
                    audioSource.loop = true;
                    audioSource.Play();
                }
                fireTime += Time.deltaTime;
            }
            else
            {
                laser.SetActive(false);
                if (!audioSource.isPlaying)
                {
                    audioSource.loop = false;
                    audioSource.Stop();
                }
                // fireTime -= Time.deltaTime;
            }
        }
        else
        {
            laser.SetActive(false);
            if (!audioSource.isPlaying)
            {
                audioSource.loop = false;
                audioSource.Stop();
            }

            coolDown -= Time.deltaTime;
            if (coolDown <= 0)
            {
                fireTime = 0;
                coolDown = coolDownTime;
            }

            healthBar.fillAmount = coolDown / coolDownTime;
        }

        
    }


}
