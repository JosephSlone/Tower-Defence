using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    [Range(0.1f, 120f)]
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] GameObject spawnObject;
    [SerializeField] Text scoreText;

    int enemiesSpawned = 0;
    
	// Use this for initialization
	void Start () {
        StartCoroutine(RepeatedlySpawnEnemies());
        scoreText.text = enemiesSpawned.ToString();
	}

    IEnumerator RepeatedlySpawnEnemies()
    {
        while(true)
        {
            Vector3 spawnPosition = spawnObject.transform.position;
            spawnPosition.y = 10f;

            var newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemiesSpawned += 1;
            scoreText.text = enemiesSpawned.ToString();
           
            newEnemy.transform.parent = enemyParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
		
}
