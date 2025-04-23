/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Projectile specific functions and variables. Handles deflect kills onto enemies, and also sets velocity after fired
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Rigidbody rb;
    public Vector3 projVelocity;
    public float disappearTime = 5f;

    public float projectileSpeed;
    public int damage;
    public float bonusPlasma;

    public bool isDeflected;

    private PlayerHealth player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        isDeflected = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = projVelocity;
        Destroy(this.gameObject, disappearTime);
    }

    public void SetVelocity(Vector3 velocity)
    {
        projVelocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isDeflected)
        {
            other.GetComponent<EnemyHealth>().TakeDamage(player.attackDamage2, this.GetComponent<EnemyHealth>());
            ScoreController.Instance.AddPlasma(bonusPlasma);
            print("deflect kill!");
            Destroy(this.gameObject);
        }
    }
}
