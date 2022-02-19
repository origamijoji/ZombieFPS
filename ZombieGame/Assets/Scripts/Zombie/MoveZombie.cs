using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveZombie : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public float speed;
    private void Awake() {
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //transform.Translate(speed * Vector3.forward);
        agent.SetDestination(player.transform.position);

    }
}
