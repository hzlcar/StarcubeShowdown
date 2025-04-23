/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * This is the parent class for all Enemies and defines a function for movement and several variables
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Create public variables for player speed, and for the Text UI game objects
    public float speed;
    public float readjustRate;
    
    // Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
    protected Rigidbody rb;
    public FirstPersonMovement player;
    public ParticleSystem spawnParticle;

    protected Vector3 movement;
    protected float distance;

    public bool tookDamageThisAttack;

    // Start is called before the first frame update
    protected void BaseStart()
    {
        tookDamageThisAttack = false;
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<FirstPersonMovement>();
        //spawnParticle.Play();

        GetComponentInParent<AudioSource>().PlayOneShot(AudioManager.Instance.enemySounds[1]);

        InvokeRepeating("UpdateMovement", 0.7f, readjustRate);
    }

    protected void BaseUpdate()
    {

    }

    public virtual void UpdateMovement() { }

    public virtual bool CloseRange() {
        // returns true if enemy is within 10 units
        if (distance < 10)
        {
            return true;
        }
        return false;
    }
}
