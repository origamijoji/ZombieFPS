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
    private bool roundActive;
    [SerializeField] private float inbetweenRoundTime;
    [SerializeField] private float spawnTime;
    public float currentHealth;
    public float currentSpeed;
    public int pity;
    public int currentPity;

    [SerializeField] private float zombieRate = 0.15f;
    [SerializeField] private float healthRate = 100;
    [SerializeField] private float speedRate = 0.2f;
    [SerializeField] private float spawnRate = 0.8f;

    [System.Serializable]
    public class Zombies {
        public string tag = "Zombie";
        public GameObject prefab;
        public int size;
    }
    public List<Zombies> zombies;
    public Queue<GameObject> zombieQueue = new Queue<GameObject>();

    [System.Serializable]
    public class Powerups {
        public string type;
        public int position;
        public bool active;
        public GameObject obj;
    }
    public List<Powerups> powerups;

    public float timeToNextSpawn;
    public GameObject[] spawnLocations;
    public GameObject previousSpawnLocation;

    public delegate void UpdateZombies();
    public static event UpdateZombies NewRound;

    [Tooltip("Fraction of 100")]
    public int powerupChance;
    public int powerupsLeft;
    public int powerupsMax = 4;

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

        int powerupPosition = 1;
        foreach (Powerups powerup in powerups) {
            powerup.position = powerupPosition;
            powerupPosition++;
        }

        currentSpeed = 1;
        if (!disableSpawn) { StartCoroutine(StartNextRound()); }

    }

    private void Update() {
        if (timeToNextSpawn > 0) {
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
        powerupsLeft = powerupsMax;
        pity = (int) Mathf.Pow(currentRound, 2)/2 + 5;
        foreach (Powerups powerup in powerups) {
            powerup.active = false;
        }
        StartCoroutine(SpawnHorde());
    }

    public GameObject SpawnZombie() {

        GameObject objectToSpawn = zombieQueue.Dequeue();
        previousSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetPositionAndRotation(previousSpawnLocation.transform.position, previousSpawnLocation.transform.rotation);

        return objectToSpawn;
    }

    public void QueueZombie(GameObject thisObject) {
        zombieQueue.Enqueue(thisObject);
    }

    public GameObject SpawnPowerup(Vector3 position, Quaternion rotation) {
        int chance = Random.Range(1, 101);
        Debug.Log("Roll: " + chance);
        if (chance < powerupChance || pity <= currentPity) {
            currentPity = 0;
            int powerupType = Random.Range(1, powerups.Count + 1);
            Powerups powerup = powerups.Find(e => e.position.Equals(powerupType));

            if (powerupsLeft > 0 && powerup.active == false) {

                powerup.active = true;
                GameObject objectToSpawn = powerup.obj;

                powerupsLeft--;

                objectToSpawn.SetActive(true);
                objectToSpawn.transform.SetPositionAndRotation(position, rotation);

                return objectToSpawn;
            }
        }
        else { currentPity++; }

        return null;
    }

    public void DisablePowerup(string powerupName) {
        Powerups powerup = powerups.Find(p => p.type.Equals(powerupName));
        powerup.active = false;
    }

}
