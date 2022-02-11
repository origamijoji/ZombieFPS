using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public Transform player;

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("test");
            player.position = gameObject.transform.position;
            player.rotation = gameObject.transform.rotation;
        }
    }
}
