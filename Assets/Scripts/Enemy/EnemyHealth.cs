using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;
    public float sinkSpeed = 5f;

    public bool isDead;
    private bool isSinking;

    private CapsuleCollider capsuleCollider;

    public GameObject itemObject;

    void Awake() {
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking) {
            //transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    // Damage calculation.
    public void TakeDamage(float amount) {
        if (isDead) {
            return;
        }

        currentHealth -= amount;

        if (currentHealth <= 0) {
            Death();
        }
    }

    // What happens whenever the enemy dies.
    void Death() {
        isDead = true;

        capsuleCollider.isTrigger = true;

        int randomNum = Random.Range(1, 6);

        for (int i = 0; i < randomNum; i++) {
            GameObject itemDrop = Instantiate(itemObject, transform.position + (Vector3.right * Random.Range(-2f, 2f) + Vector3.forward * Random.Range(-2f, 2f)), Quaternion.identity);
            itemDrop.SetActive(true);
        }

        StartSinking();
    }

    // Make the object sink into the ground before being destroyed.
    void StartSinking() {
        GetComponent<Rigidbody>().isKinematic = false;

        isSinking = true;

        Destroy(gameObject, 3f);
    }
}
