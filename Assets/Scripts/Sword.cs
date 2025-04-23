/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Main class for the Sword. Contains a state machine to switch between neutral/attack/block. Contains functionality for hitting enemies and
 * dealing damage. Also handles playing animations and switching animations whenever state switches.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject player;
    public PauseScreen pauseScreen;

    public float attackCooldown;
    public float attackAnimationSpeed;
    public float attackAnimationTime;
    public float swordDamage;

    private bool isOnCooldown;

    [SerializeField] private bool attackDirection;

    private Animator animator;
    public Health playerHealth;

    private List<Enemy> hitEnemies;

    private enum SwordState
    {
        Neutral,
        Attacking,
        Blocking
    }

    private SwordState currentState = SwordState.Neutral;

    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        animator = GetComponent<Animator>();
        playerHealth = player.GetComponent<Health>();
        hitEnemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case SwordState.Neutral:
                // print("neutral");
                HandleNeutralState();
                break;

            case SwordState.Attacking:
                // print("attacking");
                HandleAttackingState();
                break;

            case SwordState.Blocking:
                // print("blocking");
                HandleBlockingState();
                break;
        }
    }

    private void HandleNeutralState()
    {
        // start attacking
        if (Input.GetKey(KeyCode.Mouse0) && !pauseScreen.IsGamePaused() && !isOnCooldown)
        {
            attackDirection = true;
            currentState = SwordState.Attacking;
            state = 1;
            StartCoroutine(AttackCoroutine());
        }
        // start blocking
        else if (Input.GetKey(KeyCode.Mouse1) && !pauseScreen.IsGamePaused())
        {
            currentState = SwordState.Blocking;
            state = 2;
            StartBlocking();
        }
    }

    private void HandleAttackingState()
    {
        // start blocking 
        if (Input.GetKey(KeyCode.Mouse1) && !pauseScreen.IsGamePaused())
        {
            currentState = SwordState.Blocking;
            state = 2;
            StartBlocking();
        }
        // stop attacking
        else if (Input.GetKeyUp(KeyCode.Mouse0) && !pauseScreen.IsGamePaused() && isOnCooldown)
        {
            currentState = SwordState.Neutral;
            state = 0;
            StopAttacking();
        }
    }

    private void HandleBlockingState()
    {
        // stop blocking
        if (Input.GetKeyUp(KeyCode.Mouse1) && !pauseScreen.IsGamePaused())
        {
            currentState = SwordState.Neutral;
            state = 0;
            StopBlocking();
        }
    }

    private void StartBlocking()
    {
        // Start blocking logic
        animator.Play("swordBlock");
    }

    private void StopBlocking()
    {
        // Stop blocking logic
        animator.Play("Neutral");
    }

    private void StopAttacking()
    {
        animator.Play("Neutral");
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (currentState != SwordState.Attacking) // break if already attacking
            {
                break;
            }

            // 2 different swing animations (left and right)
            if (attackDirection) { animator.Play("swordSwing1"); }
            else { animator.Play("swordSwing2"); }

            GetComponentInParent<AudioSource>().PlayOneShot(AudioManager.Instance.playerSounds[0]);

            animator.speed = attackAnimationSpeed; // set attack animation speed

            yield return new WaitForSeconds(attackAnimationTime / attackAnimationSpeed);

            // for every enemy hit this attack, reset their hit status & clear from array
            foreach (Enemy e in hitEnemies)
            {
                e.tookDamageThisAttack = false;
            }
            hitEnemies.Clear();

            if (currentState == SwordState.Attacking) // while still attacking, play brief neutral animation after finishing a swing
            {
                if (attackDirection) { animator.Play("NeutralLeft"); }
                else { animator.Play("NeutralRight"); }
            }

            // keep attacking if holding leftclick, but if not, then stop attacking and break loop
            if (!Input.GetKey(KeyCode.Mouse0) && currentState == SwordState.Attacking)
            {
                isOnCooldown = false;
                currentState = SwordState.Neutral;
                state = 0;
                StopAttacking();
                break;
            }

            isOnCooldown = true;
            yield return new WaitForSeconds(attackCooldown); // wait a bit before next swing
            isOnCooldown = false;

            attackDirection = !attackDirection; // swap direction for attack animation
        }
    }

    private void OnTriggerStay(Collider other) // handles enemies getting hit during an attack, and taking damage
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (other.CompareTag("Enemy") && (currentState == SwordState.Attacking) && !enemy.tookDamageThisAttack) 
        {
            GetComponentInParent<AudioSource>().PlayOneShot(AudioManager.Instance.playerSounds[1]);
            enemy.tookDamageThisAttack = true; // enemy can only take damage once per swing
            hitEnemies.Add(enemy); // add enemy to hit enemies array
            AttackDamage(other.GetComponent<Health>());
        }
    }

    private void AttackDamage(Health enemy)
    {
        enemy.TakeDamage(playerHealth.attackDamage1, enemy);
    }

    private void DeflectDamage(Health enemy) // unused deflect function
    {
        enemy.TakeDamage(playerHealth.attackDamage2, enemy);
    }
}