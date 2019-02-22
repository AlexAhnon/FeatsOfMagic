using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour
{
    private float attackDistance = 1.5f;
    public float attackRate = .5f;
    private float nextAttack;

    private NavMeshAgent navMeshAgent;
    private GameObject targetedEnemy;

    public bool walking;
    private bool enemyClicked;
    public bool isNearObject;

    private CharacterAttack attackScript;

    void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent> ();

        attackScript = gameObject.GetComponent<CharacterAttack> ();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0)) {
            // Make it so player can't click on objects behind the user interface.
            if (!EventSystem.current.IsPointerOverGameObject()) {
                if (Physics.Raycast(ray, out hit, 100)) {
                    if (hit.collider.CompareTag("Enemy")) {
                        targetedEnemy = hit.transform.gameObject;
                        enemyClicked = true;
                    } else {
                        if (isNearObject && !hit.collider.CompareTag("Terrain")) {
                            return;
                        }

                        walking = true;
                        navMeshAgent.destination = hit.point;
                        navMeshAgent.isStopped = false;
                    } 
                }
            }
        }

        if (enemyClicked) {
            Attack();
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
            if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon) {
                walking = false;
            } else {
                walking = true;
            }
        }
    }

    void Attack() {
        if (targetedEnemy == null) {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0)) {
            if (Physics.Raycast(ray, out hit, 100)) {
                if (!hit.collider.CompareTag("Enemy")) {
                    targetedEnemy = null;
                    enemyClicked = false;
                    return;
                }
            }
        }

        navMeshAgent.destination = targetedEnemy.transform.position;
        if (navMeshAgent.remainingDistance >= attackDistance) {
            navMeshAgent.isStopped = false;
            walking = true;
        }
        
        if (navMeshAgent.remainingDistance <= attackDistance) {
            transform.LookAt(targetedEnemy.transform);

            if (Time.time > nextAttack) {
                nextAttack = Time.time + attackRate;
                attackScript.AutoAttack(targetedEnemy);
            }

            navMeshAgent.isStopped = true;
            walking = false;

            enemyClicked = false;
        }
    }

    private void OnCollisionEnter(Collision collider) {
        navMeshAgent.isStopped = true;
        walking = false;
        isNearObject = true;
    }

    private void OnCollisionExit(Collision collider) {
        isNearObject = false;
    }
}
