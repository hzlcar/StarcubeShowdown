/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Base Health class that implements health systems for players and enemies. Handles taking damage, death, and also storing variables of health and damage
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    // how much damage this object deals to others
    public int attackDamage1;
    public int attackDamage2;

    public bool isPlayer;
    public bool diesOnContact;

    public float flickerSpeed;
    public float dmgCooldownDuration;

    protected bool dmgCooldownIsActive;
    public bool isDead;

    public MeshRenderer mesh;

    protected float startTime;
    protected float time;

    private void Start()
    {
        currentHealth = maxHealth;
        dmgCooldownIsActive = false;
        isDead = false;
    }

    public virtual void TakeDamage(int damage, Health source)
    {
        if (!dmgCooldownIsActive)
        {
            currentHealth -= damage;

            if (currentHealth <= 0) // die if at 0 or less health
            {
                currentHealth = 0;
                Die();
            }
            if (source.diesOnContact) // mainly for projectiles
            {
                source.DieOnContact();
            }

            // flicker & immune to damage for duration
            dmgCooldownIsActive = true;
            if (!isDead)
            {
                StartCoroutine(Flicker());
            }
        }
    }

    public virtual IEnumerator Flicker()
    {
        // flicker mesh until damage cooldown is over
        startTime = Time.time;
        while ((Time.time - startTime) < dmgCooldownDuration)
        {
            mesh.enabled = false;
            yield return new WaitForSeconds(flickerSpeed);
            mesh.enabled = true;
            yield return new WaitForSeconds(flickerSpeed);
        }
        dmgCooldownIsActive = false;
    }

    public virtual void Heal(int amount) // dev tool for testing heal
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    protected virtual void DieOnContact()
    {
        isDead = true;
    }

    public bool isDmgCooldownActive()
    {
        return dmgCooldownIsActive;
    }

}
