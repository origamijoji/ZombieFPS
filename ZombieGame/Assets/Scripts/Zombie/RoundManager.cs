using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {
    public int currentRound;
    public int zombiesThisRound;
    public int zombiesSpawned;
    public int zombiesRemaining;
    public float spawnTime;
    public float currentHealth;
    public float currentSpeed;
    public float nextHealth;
    public float nextSpeed;
    public float zombieRate = 0.15f;
    public float healthRate = 100;
    public float speedRate = 0.2f;
    public float spawnRate;

    public float timeToNextSpawn;
    public GameObject[] spawnLocations;
    public GameObject previousSpawnLocation;
    private PoolManager poolManager;

    private void Awake() {
        spawnLocations = GameObject.FindGameObjectsWithTag("Window");
        poolManager = PoolManager.instance;
    }
    private void Start() {
        NextRound();
        StartCoroutine(SpawnNextZombie());
    }
    public void ZombieDeath() {
        zombiesRemaining--;
        if (zombiesRemaining == 0) {
            NextRound();
        }
    }

    public void ZombieSpawned() {
        zombiesSpawned++;
    }

    private void NextRound() {
        currentRound++;
        zombiesThisRound = (int)(currentRound * zombieRate * 24);
        zombiesRemaining = zombiesThisRound;
        currentHealth = nextHealth;
        nextHealth = currentHealth + healthRate;
        currentSpeed = nextSpeed;
        nextSpeed = currentSpeed * speedRate + currentSpeed;
        spawnTime = spawnRate * spawnTime + spawnTime;
    }

    void SpawnZombie() {
        previousSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
        poolManager.SpawnZombie(previousSpawnLocation.transform.position, previousSpawnLocation.transform.rotation);
    }

    public IEnumerator SpawnNextZombie() {

        for (int zombiesLeft = zombiesThisRound; zombiesLeft > 0; zombiesLeft--) {
            Debug.Log("reiterated spawner");
            timeToNextSpawn = spawnTime;
            yield return new WaitForSeconds(spawnTime);
            SpawnZombie();
            yield return null;
        }
    }
}
