/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * This script handles the sword block/deflect move and makes sure projectiles only deflect while in front of the player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectHitbox : MonoBehaviour
{
    public Sword sword;
    public Camera playerCamera;

    // Deflection zone hitbox
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Projectile>() && sword.state == 2 && !IsEnemyEnteringFromSide(other))
        {
            Projectile p = other.GetComponent<Projectile>();

            GetComponentInParent<AudioSource>().PlayOneShot(AudioManager.Instance.playerSounds[1]);
            GetComponentInParent<AudioSource>().PlayOneShot(AudioManager.Instance.playerSounds[3], 0.6f);

            p.isDeflected = true;
            p.transform.position = playerCamera.transform.position + new Vector3(0,0,0); 
            p.SetVelocity(playerCamera.transform.forward * p.projectileSpeed * 1.5f); // set velocity away from player toward crosshair
        }
    }

    // Check if the enemy is entering from the side (not the front face)
    private bool IsEnemyEnteringFromSide(Collider enemy)
    {
        Vector3 directionToTarget = enemy.transform.position - this.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, directionToTarget);
        if (Mathf.Abs(angle) < 90) // enemies & projectiles in 90 degree view can be blocked
        {
            return false;
        }
        return true;
    }
}
