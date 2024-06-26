using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int initialEnemies;
    [SerializeField] private int actualEnemies;
    [SerializeField] private int maxEnemies;
    [SerializeField] private int enemiesLeft;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float timeBetweenEnemies;

    [SerializeField] private int enemyChoosed;

    [SerializeField] private GameObject spawnPositionLeft;
    [SerializeField] private GameObject spawnPositionRight;

    [SerializeField] private int spawn;

    private void Start()
    {
        actualEnemies = 0;
    }

    private void SpawnEnemies()
    {
        if(actualEnemies <= maxEnemies)
        {
            StartCoroutine(Spawn());
        }
        else
        {
            StopCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        enemyChoosed = Random.Range(0, 3);
        yield return new WaitForSeconds(timeBetweenEnemies);
        switch(enemyChoosed)
        {
            case 0:
                spawn = Random.Range(0, 2);
                switch(spawn)
                {
                    case 0:
                        Instantiate(enemies[0], spawnPositionLeft.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(enemies[0], spawnPositionRight.transform.position, Quaternion.identity);
                        break;
                }
                
                break;
            case 1:
                spawn = Random.Range(0, 2);
                switch (spawn)
                {
                    case 0:
                        Instantiate(enemies[1], spawnPositionLeft.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(enemies[1], spawnPositionRight.transform.position, Quaternion.identity);
                        break;
                }

                break;
            case 2:
                spawn = Random.Range(0, 2);
                switch (spawn)
                {
                    case 0:
                        Instantiate(enemies[2], spawnPositionLeft.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(enemies[2], spawnPositionRight.transform.position, Quaternion.identity);
                        break;
                }

                break;
        }
    }

}
