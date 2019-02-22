using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public float playerDamage = 20f;

    private EnemyHealth enemyHealthScript;

    public void AutoAttack(GameObject enemy) {
        enemyHealthScript = enemy.GetComponent<EnemyHealth>();

        enemyHealthScript.TakeDamage(playerDamage);
    }
}
