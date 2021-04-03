using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjectsToSpawn;
    [SerializeField] bool spawnEnemies;
    [SerializeField] int spawnTimer = 5;

    bool corutineOn = false;

    private void Update()
    {
        if (spawnEnemies)
        {
            if (!corutineOn)
            {
                StartCoroutine(SpawnEnemies());
                corutineOn = true;
            }
        }
        else
        {
            if (corutineOn)
            {
                StopAllCoroutines();
                corutineOn = false;
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnEnemies)
        {
            yield return new WaitForSeconds(spawnTimer);
            Instantiate(gameObjectsToSpawn[0], GetRandomPos(), Quaternion.identity);
        }
    }
    private Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f), 0);
    }
}
