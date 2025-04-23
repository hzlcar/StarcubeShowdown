/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * UFO enemy script, inherits from Enemy. Implements movement and has a special function for instantiating projectiles, plus cannon rotation
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUFO : Enemy
{
    private List<Projectile> projectiles;

    public float fireRate;
    public float predictionTime;

    private float rotationSpeed;
    public float minRotationSpeed;
    public float maxRotationSpeed;

    public Projectile projectile;

    public Cannon cannon;

    // Start is called before the first frame update
    void Start()
    {
        base.BaseStart();
        InvokeRepeating("CreateProjectile", 1f, fireRate);
        InvokeRepeating("UpdateRotation", 0f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        base.BaseUpdate();
        RotateCannonTowardsPlayer();
    }

    public override void UpdateMovement()
    {
        // Create a Vector3 variable, and assign it to the difference between player position and enemy position, normalized
        movement = ((player.transform.position) - (this.transform.position));
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        // basically stops moving if within close range
        if (CloseRange())
        {
            rb.velocity = movement * speed / 100f;
        }
        else
        {
            rb.velocity = movement * speed / 10f;
        }
    }

    private void CreateProjectile() // shoot projectiles on an interval
    {
        Vector3 projectilePosition = transform.position;

        // predicts where the player will be in a certain time and shoots there
        Vector3 predictedPlayerPosition = player.transform.position + player.GetComponent<Rigidbody>().velocity * predictionTime;
        Vector3 bulletDirection = (predictedPlayerPosition - projectilePosition).normalized * projectile.projectileSpeed;

        projectile.SetVelocity(bulletDirection);

        if (!GetComponent<EnemyHealth>().isDead)
        {
            Instantiate(projectile, projectilePosition, Quaternion.identity);
        }

        rotationSpeed = minRotationSpeed;
    }

    public override bool CloseRange()
    {
        // returns true if within 18 units of player. (larger distance than Ghost)
        if (distance < 18)
        {
            return true;
        }
        return false;
    }

    private void UpdateRotation()
    {
        rotationSpeed = maxRotationSpeed;
    }

    private void RotateCannonTowardsPlayer()
    {
        // Calculate the direction from the UFO to the player
        Vector3 lookDirection = new Vector3(movement.x, movement.y, movement.z) + player.GetComponent<Rigidbody>().velocity * predictionTime;

        // Calculate the rotation to face the player along the y-axis
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, new Vector3(0, 1, 1));
        Quaternion cannonRotation = targetRotation * Quaternion.Euler(0f, -90f, 0f);

        cannon.gameObject.transform.rotation = Quaternion.Slerp(cannon.gameObject.transform.rotation, cannonRotation, rotationSpeed * Time.deltaTime);
    }

}
