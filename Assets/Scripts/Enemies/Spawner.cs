/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Spawner class using Prototype design pattern. Spawns a certain enemy on an interval
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Enemy enemyPrototype;
    private List<Enemy> enemies;

    private float baseSpawnRate;
    private float spawnRate;
    private float startTime;

    public float spawnRateIncrease = 6000f;

    private FirstPersonMovement player;

    private float time;

    public void Constructor(Enemy enemyPrototype, float spawnRate, float startTime)
    {
        this.enemyPrototype = enemyPrototype;
        this.spawnRate = spawnRate;
        this.startTime = startTime;
    }

    public void Start()
    {
        player = FindObjectOfType<FirstPersonMovement>();
        Random.InitState(1130); // set seed for spawn pattern
        baseSpawnRate = spawnRate;

        // InvokeRepeating("SpawnRateIncrease", 0, 1f);
    }

    private void Update()
    {
        HandleSpawning();
    }

    private void HandleSpawning()
    {
        // spawn enemy every spawnRate
        if (Time.time > startTime)
        {
            time += Time.deltaTime;

            if (time >= spawnRate)
            {
                time -= spawnRate;
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy() 
    {
        PlasmaMultiplication(); // change spawnRate based on plasma

        while (true) // keep looping until spawn position is 30 units from player
        {
            Vector3 enemyPosition = new Vector3(Random.Range(-47, 47), Random.Range(2, 96), Random.Range(46, -46)); // borders of cube
            float distance = Vector3.Distance(player.transform.position, enemyPosition);

            if (distance > 30f) // if enemy is 30 units from player
            {
                Instantiate(enemyPrototype, enemyPosition, Quaternion.identity);
                break;
            }
        }
    }

    private void PlasmaMultiplication() // handle plasma multiplication of spawn rate
    {
        float plasma = ScoreController.Instance.plasma;

        // increase spawn rate if plasma is higher than zero
        if (plasma >= 0)
        {
            spawnRate = baseSpawnRate * Mathf.Exp(-plasma / 100);
        }
        else
        {
            spawnRate = baseSpawnRate;
        }
    }

    private void SpawnRateIncrease() // unused function for increasing base spawn rate over time
    {
        print(baseSpawnRate);
        baseSpawnRate -= (baseSpawnRate/spawnRateIncrease);
    }
}
