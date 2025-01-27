using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
    }


    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
  
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1f,5f));
        }
    }

    IEnumerator SpawnPowerUp()
    {

        while (_stopSpawning == false)
        {

            yield return new WaitForSeconds(Random.Range(10f, 15f));
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newTriple = Instantiate(powerups[Random.Range(0,powerups.Length)], posToSpawn, Quaternion.identity);
            newTriple.transform.parent = _enemyContainer.transform;
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
