/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Manages spawners of different enemies. Uses Prototype design pattern. Creates a ghostSpawner and ufoSpawner and constructs them
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public EnemyGhost ghostPrototype;
    public float ghostSpawnRate;
    public float ghostStartTime;

    public EnemyUFO ufoPrototype;
    public float ufoSpawnRate;
    public float ufoStartTime;

    // Start is called before the first frame update
    void Start()
    {
        Spawner ghostSpawner = gameObject.AddComponent<Spawner>();
        ghostSpawner.Constructor(ghostPrototype, ghostStartTime, ghostSpawnRate);

        Spawner ufoSpawner = gameObject.AddComponent<Spawner>();
        ufoSpawner.Constructor(ufoPrototype, ufoStartTime, ufoSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
