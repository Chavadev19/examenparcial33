using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagerSpawner : MonoBehaviour
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

    [SerializeField] private TextMeshProUGUI enemiesLeftText;

    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        actualEnemies = initialEnemies;
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if(enemiesLeft <= 0)
        {
            gameManager.Victory();
        }

        enemiesLeftText.text = enemiesLeft.ToString(); 
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (actualEnemies < maxEnemies)
            {
                StartCoroutine(Spawn());
            }
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    private IEnumerator Spawn()
    {
        enemyChoosed = Random.Range(0, enemies.Length);
        yield return new WaitForSeconds(timeBetweenEnemies);

        spawn = Random.Range(0, 2);
        Vector3 spawnPosition = spawn == 0 ? spawnPositionLeft.transform.position : spawnPositionRight.transform.position;
        Instantiate(enemies[enemyChoosed], spawnPosition, Quaternion.identity);

        actualEnemies++;
    }

    // Método para reducir el contador de enemigos (debería llamarse cuando un enemigo es destruido)
    public void EnemyDestroyed()
    {
        actualEnemies--;
        enemiesLeft--;
    }
}
