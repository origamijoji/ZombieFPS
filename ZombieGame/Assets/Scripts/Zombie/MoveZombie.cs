using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveZombie : MonoBehaviour
{
    public GameObject player;
    public RoundManager roundManager;
    public NavMeshAgent agent;
    private void Awake() {
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        roundManager = RoundManager.instance;
    }
    void Update()
    {
        //transform.Translate(speed * Vector3.forward);
        agent.speed = roundManager.currentSpeed;
        agent.SetDestination(player.transform.position);

    }

    private void UpdateStats() {
        agent.speed = roundManager.currentSpeed;
    }

    private void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.CompareTag("Player")) {
            PlayerHealth playerHit = collision.gameObject.GetComponent<PlayerHealth>();
            playerHit.TakeDamage(25f);
        }
    }
}
