using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public GameObject[] obstaclePrefab1;
    private float startDalay = 3;
    private float spawnInterval = 1.7f;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomObstacle", startDalay, spawnInterval);
        InvokeRepeating("SpawnRandomObstacle1", startDalay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomObstacle()
    {
        int obstacleIndex = Random.Range(0, obstaclePrefab.Length);
        Instantiate(obstaclePrefab[obstacleIndex], new Vector3(4, 1.4f, 0), obstaclePrefab[obstacleIndex].transform.rotation);
    }

    void SpawnRandomObstacle1()
    {
        int obstacleIndex = Random.Range(0, obstaclePrefab1.Length);
        Instantiate(obstaclePrefab1[obstacleIndex], new Vector3(4, Random.Range(-1.4f, 3.2f), 0), obstaclePrefab1[obstacleIndex].transform.rotation);
    }
}
