using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZombie : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //transform.Translate(speed * Vector3.forward);
        Vector3.MoveTowards(gameObject.transform.position, player.transform.position, Time.deltaTime * speed);
    }
}
