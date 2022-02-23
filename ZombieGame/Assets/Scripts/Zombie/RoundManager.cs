using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {

    static RoundManager _instance;

    public static RoundManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<RoundManager>();
            }
            return _instance;
        }
    }
    public bool disableSpawn;
    public int currentRound;
    public int zombiesThisRound;
    public int zombiesSpawned;
    public int zombiesRemaining;
    public bool roundActive;

    public float inbetweenRoundTime;
    public float spawnTime;
    public float currentHealth;
    public float currentSpeed;

    public float zombieRate = 0.15f;
    public float healthRate = 100;
    public float speedRate = 0.2f;
    public float spawnRate = 0.8f;

    [System.Serializable]
    public class Zombies {
        public string tag = "Zombie";
        public GameObject prefab;
        public int size;
    }

    public List<Zombies> zombies;
    public Queue<GameObject> zombieQueue = new Queue<GameObject>();

    public float timeToNextSpawn;
    public GameObject[] spawnLocations;
    public GameObject previousSpawnLocation;
    private Coroutine SpawnZombies;

    public delegate void UpdateZombies();
    public static event UpdateZombies NewRound;

    private void Awake() {
        spawnLocations = GameObject.FindGameObjectsWithTag("Window");
    }
    private void Start() {

        foreach (Zombies zombie in zombies) {
            Transform parent = GameObject.Find("_" + zombie.tag).transform;

            for (int i = 0; i < zombie.size; i++) {
                GameObject obj = Instantiate(zombie.prefab);
                obj.SetActive(false);
                zombieQueue.Enqueue(obj);
                obj.transform.parent = parent;
            }
        }
        if(!disableSpawn) { StartCoroutine(StartNextRound()); }

    }

    private void Update() {
        if(timeToNextSpawn > 0) {
            timeToNextSpawn -= Time.deltaTime;
        }
    }

    public void ZombieDeath() {
        zombiesRemaining--;
        if (zombiesRemaining == 0) {
            StartCoroutine(StartNextRound());
        }
    }

    public void ZombieSpawned() {
        zombiesSpawned++;
    }

    public IEnumerator SpawnHorde() {
        while (roundActive) {
            while (zombieQueue.Count > 0 && zombiesSpawned < zombiesThisRound) {
                timeToNextSpawn = spawnTime;
                yield return new WaitForSeconds(spawnTime);
                zombiesSpawned++;
                SpawnZombie();
                yield return null;
            }
            yield return null;
        }
    }

    public IEnumerator StartNextRound() {

        Debug.Log("Rounded ended");
        roundActive = false;
        yield return new WaitForSeconds(inbetweenRoundTime);
        Debug.Log("Round starts");
        roundActive = true;
        NextRound();
        yield break;
    }

    private void NextRound() {
        currentRound++;
        zombiesSpawned = 0;
        currentHealth += healthRate;
        currentSpeed = currentSpeed * speedRate + currentSpeed;
        NewRound?.Invoke();
        spawnTime = spawnRate * spawnTime;
        zombiesThisRound = (int)(currentRound * zombieRate * 24);
        zombiesRemaining = zombiesThisRound;
        SpawnZombies = StartCoroutine(SpawnHorde());
    }

    public GameObject SpawnZombie() {

        GameObject objectToSpawn = zombieQueue.Dequeue();
        previousSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = previousSpawnLocation.transform.position;
        objectToSpawn.transform.rotation = previousSpawnLocation.transform.rotation;

        return objectToSpawn;
    }

    public void QueueZombie(GameObject thisObject) {
        zombieQueue.Enqueue(thisObject);
    }

}
