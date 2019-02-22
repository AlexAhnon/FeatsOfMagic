using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public enum State {
        Idle,
        Chase,
        Dead
    }

    public State activeState;

    private NavMeshAgent agent;
    private Animator anim;
    private EnemyHealth health;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        health = gameObject.GetComponent<EnemyHealth>();

        player = GameObject.FindGameObjectWithTag("Player");

        activeState = State.Idle;
        StateEnter();
    }

    private void StateUpdate() {
        if (activeState == State.Idle) {
            // Idle-state
            agent.destination = transform.position;
        } else if (activeState == State.Chase) {
            // Chase-state
            agent.destination = player.transform.position;
        } else if (activeState == State.Dead) {
            // Dead-state
        }
    }

    private void StateTransition() {
        State oldState = activeState;

        // 180 degree angle FoV for the enemy
        Vector3 targetDir = player.transform.position - transform.position;
        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

        if (activeState == State.Idle) {
            // Idle-state
            if (health.isDead) {
                activeState = State.Dead;
            } else if (angleToPlayer >= -90 && angleToPlayer <= 90) {
                activeState = State.Chase;
            } 
        } else if (activeState == State.Chase) {
            // Chase-state
            if (health.isDead) {
                activeState = State.Dead;
            } else if (angleToPlayer >= -90 && angleToPlayer <= 90) {
                // Keep chasing
            } else {
                // If player is no longer in FoV, go back to idling
                activeState = State.Idle;
            }
        } else if (activeState == State.Dead) {
            // Dead-state
        }

        if (activeState != oldState) {
            StateEnter();
        }
    }

    // Do stuff when entering a state
    private void StateEnter() {
        if (activeState == State.Idle) {
            // Idle-state
            anim.SetBool("isChasing", false);
        } else if (activeState == State.Chase) {
            // Chase-state
            anim.SetBool("isChasing", true);
        } else if (activeState == State.Dead) {
            // Dead-state
            anim.SetBool("isDead", true);
            agent.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StateTransition();
        StateUpdate();
    }
}
