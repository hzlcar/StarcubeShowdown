/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Controls enemy health, inherits from Health. Implements Die() and overrides other functions to add specific Enemy functionality
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    // Start is called before the first frame update
    public ParticleSystem dieParticle;

    public float plasmaReward;
    private SpriteRenderer face;
    private Cannon cannon;

    private void Start()
    {
        isDead = false;
    }

    protected override void Die()
    {
        base.Die();

        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // prevent enemy from spinning if dying
        dieParticle.Play();

        GetComponentInParent<AudioSource>().PlayOneShot(AudioManager.Instance.enemySounds[2]);

        ScoreController.Instance.AddPlasma(plasmaReward); // add plasma after kill

        // disable base mesh and face/cannon meshes
        mesh.enabled = false;
        if (face = GetComponentInChildren<SpriteRenderer>()) { face.enabled = false; }
        if (cannon = GetComponentInChildren<Cannon>()) { cannon.cannon.enabled = false; cannon.cannonHole.enabled = false; }
        GetComponent<SphereCollider>().enabled = false;

        Destroy(gameObject, 0.5f);
    }

    protected override void DieOnContact() // for projectiles that disappear after hitting something
    {
        base.DieOnContact();
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        mesh.enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        Destroy(gameObject, 0.5f);

    }

    public override IEnumerator Flicker() // flicker when taking damage
    {
        startTime = Time.time;

        // flicker base mesh and face/cannon until damage cooldown is over
        while ((Time.time - startTime) < dmgCooldownDuration)
        {
            mesh.enabled = false;
            if (face = GetComponentInChildren<SpriteRenderer>()) { face.enabled = false; }
            if (cannon = GetComponentInChildren<Cannon>()) { cannon.cannon.enabled = false; cannon.cannonHole.enabled = false; }
            yield return new WaitForSeconds(flickerSpeed);
            mesh.enabled = true;
            if (face = GetComponentInChildren<SpriteRenderer>()) { face.enabled = true; }
            if (cannon = GetComponentInChildren<Cannon>()) { cannon.cannon.enabled = true; cannon.cannonHole.enabled = true; }
            yield return new WaitForSeconds(flickerSpeed);
        }
        mesh.enabled = true;
        if (face = GetComponentInChildren<SpriteRenderer>()) { face.enabled = true; }
        if (cannon = GetComponentInChildren<Cannon>()) { cannon.cannon.enabled = true; cannon.cannonHole.enabled = true; }

        dmgCooldownIsActive = false;
        
    }

}
