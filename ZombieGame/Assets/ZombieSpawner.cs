using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject previousSpawnLocation;
    public float spawnRate;
    public float timeToNextSpawn;

    private PoolManager poolManager;
    private void Awake() {
        spawnLocations = GameObject.FindGameObjectsWithTag("Window");
        poolManager = PoolManager.instance;
    }
    void Update()
    {
        if(timeToNextSpawn >= 0) {
            timeToNextSpawn -= Time.deltaTime;
        }
        else { SpawnZombie(); }

    }

    void SpawnZombie() {
        timeToNextSpawn = spawnRate;
        previousSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
        poolManager.SpawnFromPool("Zombie", previousSpawnLocation.transform.position, previousSpawnLocation.transform.rotation);
    }
}
