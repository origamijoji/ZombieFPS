using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    //The pool class stores a tag, a prefab GameObject, and the size of the pool
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    //static variables belong to the class rather than a particular instance
    //singleton pattern ensures there is only one instance of a class with global access
    static PoolManager _instance;

    public static PoolManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }

    //List of the different allowed pools
    public List<Pool> pools;
    //Dictionary takes in string key parameter
    //Queue is a collection of objects, it takes in a GameObject
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start() {
        //poolDictionary variable 
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //foreach Pool (class) named pool (variable) in pools (list)
        foreach (Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            //for loop to create objects on scene
            //iteration for loop, while i is less than the pool size, increase i
            for (int i = 0; i < pool.size; i++) {
                //Instantiate 
                GameObject obj = Instantiate(pool.prefab);
                //Set object by default to false
                obj.SetActive(false);
                //Add the object to the pool
                objectPool.Enqueue(obj);
            }


            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    //method to be called in other objects that allows access to the pooled objects
    //public return type GameObject takes in parameters, tag, position, and rotation


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        if (!poolDictionary.ContainsKey(tag)) {
            Debug.Log("error at" + tag);
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
