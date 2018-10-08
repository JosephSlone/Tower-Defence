using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] int maxTowers = 3;
    [SerializeField] Tower towerPrefab;

    List<Tower> towerList = new List<Tower>();
    List<Waypoint> wayPointList = new List<Waypoint>();
    int oldestTower = 0;

	// Use this for initialization
	void Start () {

    }

    public void InstantiateTower(Vector3 spawnPosition, Waypoint cube)
    {        
        if (towerList.Count < maxTowers)
        {
            Tower newTower = Instantiate(towerPrefab, spawnPosition, Quaternion.identity);
            newTower.transform.parent = gameObject.transform;
            towerList.Add(newTower);
            wayPointList.Add(cube);
            cube.clearIsPlacable();
        }
        else
        {
            towerList[oldestTower].transform.position = spawnPosition;
            wayPointList[oldestTower].setIsPlacable();
            wayPointList[oldestTower] = cube;
            cube.clearIsPlacable();

            oldestTower += 1;
            if (oldestTower >= maxTowers)
            {
                oldestTower = 0;
            }
        }
    }
	
	
}
