/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Handles player health, inherits from Health. Handles updating the UI when the player takes damage and applying a damage filter on the screen
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : Health
{
    public TextMeshProUGUI healthText;
    public Image damageFilter;

    private void Start()
    {
        damageFilter.enabled = false;
        UpdateHealthText();
    }

    public override void TakeDamage(int damage, Health source)
    {
        if (!GetComponent<Dash>().isDashing) // i-frames on dash
        {
            GetComponentInParent<AudioSource>().PlayOneShot(AudioManager.Instance.playerSounds[4]);
            base.TakeDamage(damage, source);

            UpdateHealthText();

            // reset plasma to zero on taking damage
            if (ScoreController.Instance.plasma > 0)
            {
                ScoreController.Instance.plasma = 0;
            }
        }
    }

    public override IEnumerator Flicker()
    {
        StartCoroutine(base.Flicker());
        damageFilter.enabled = true;
        yield return new WaitForSeconds(dmgCooldownDuration - 0.1f);

        /*
        float startTime2 = Time.time;
        while ((Time.time - startTime2) < dmgCooldownDuration)
        {
            damageFilter.enabled = true;
            yield return new WaitForSeconds(flickerSpeed);
            damageFilter.enabled = false;
            yield return new WaitForSeconds(flickerSpeed);
        }
        */

        if (!isDead) { damageFilter.enabled = false; }
        yield break;
    }

    public override void Heal(int amount) // dev tool for testing heal
    {
        base.Heal(amount);
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        // adds full squares for every current health, and empty squares for max health
        healthText.text = "";
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                healthText.text += "■";
            }
            else
            {
                healthText.text += "□";
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(1, this);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(1);
        }
    }

    protected override void Die() // stop all audio and enable death screen when you die
    {
        AudioManager.Instance.audioSource1.Stop();
        AudioManager.Instance.audioSource2.Stop();
        GetComponentInParent<AudioSource>().PlayOneShot(AudioManager.Instance.playerSounds[5]);

        base.Die();
        DeathScreen.Instance.EndGame();
    }
}
