using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalHandler : MonoBehaviour
{
    PoolManager poolManager;
    // Start is called before the first frame update
    void Start()
    {
        poolManager = PoolManager.instance;
    }


}
