using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveZombie : MonoBehaviour {
    public GameObject player;
    public RoundManager roundManager;
    public NavMeshAgent agent;

    public double attackTimer;
    public double attackSpeed;
    public bool playerInRange;
    private void Awake() {
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        roundManager = RoundManager.instance;
    }
    private void OnEnable() {
        agent.enabled = false;
        agent.enabled = true;
    }
    void Update() {
        //transform.Translate(speed * Vector3.forward);
        agent.speed = roundManager.currentSpeed;
        agent.SetDestination(player.transform.position);


        if(playerInRange) {
            attackTimer += Time.deltaTime;

        }
        else { attackTimer = Mathf.Lerp((float) attackTimer, 0, Time.deltaTime); }

        if (attackTimer > attackSpeed) {
            PlayerHealth playerHit = player.GetComponent<PlayerHealth>();
            playerHit.TakeDamage();
            attackTimer = 0;
        }

    }

    private void UpdateStats() {
        agent.speed = roundManager.currentSpeed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerInRange = false;
        }
    }


}
