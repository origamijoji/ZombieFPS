using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Moves camera to player's head location
    public GameObject playerHead;

    void Update()
    {
        transform.position = playerHead.transform.position;
    }
}
