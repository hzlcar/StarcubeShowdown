/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Ghost enemy script, inherits from Enemy. implements movement function and has a function for rotating its face toward the player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : Enemy
{
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        base.BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
        base.BaseUpdate();
    }

    public override void UpdateMovement()
    {
        // Create a Vector3 variable, and assign it to the difference between player position and enemy position, normalized
        movement = ((player.transform.position) - (this.transform.position));
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        RotateFaceTowardsPlayer();

        // moves slightly faster if in close range
        if (CloseRange())
        {
            rb.velocity = movement * speed / 4f;
        }
        else
        {
            rb.velocity = movement * speed / 10f;
        }
    }

    private void RotateFaceTowardsPlayer()
    {
        Vector3 lookDirection = new Vector3(movement.x, 0f, movement.z);

        // Calculate the rotation to face the player along the y-axis
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        // math stuff
        Quaternion deltaRotation = targetRotation * Quaternion.Inverse(rb.rotation);
        Vector3 torque = new Vector3(deltaRotation.x, deltaRotation.y, deltaRotation.z) * Mathf.Rad2Deg;

        // add rotation to player
        rb.angularVelocity = new Vector3(rb.angularVelocity.x, 0f, rb.angularVelocity.z);
        rb.AddTorque(torque * rotationSpeed * Time.deltaTime);
    }
}
