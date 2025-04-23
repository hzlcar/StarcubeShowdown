/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Handles the player hitbox and taking damage from enemies/projectiles
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public Sword sword;
    public Camera playerCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerHealth playerHealth = GetComponentInParent<PlayerHealth>();
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (!playerHealth.isDmgCooldownActive() && !enemyHealth.isDead)
            {
                if (sword.state != 2 || IsEnemyEnteringFromSide(other)) // deal damage if not blocking
                {
                    print("took damage");
                    playerHealth.TakeDamage(enemyHealth.attackDamage1, enemyHealth);
                }
            }
        }
    }

    // Check if the enemy is entering from the side (not the front face)
    private bool IsEnemyEnteringFromSide(Collider enemy)
    {
        Vector3 directionToTarget = enemy.transform.position - this.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, directionToTarget);
        if (Mathf.Abs(angle) < 90) {
            return false;
        }
        return true;
    }
}
