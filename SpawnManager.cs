using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _asteroid;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _frequency = 5f;
    private bool _stopSpawning = false;
 

    void Start()
    {
        //StartCoroutine(SpawnAsteroidRoutine());
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
    }


    void Update()
    {
        
    }
    public void ModFrequency()
    {
        if(_frequency > 1)
        {
            _frequency-=0.25f;
        }
        
    }
    public void ResetFrequency()
    {
        _frequency = 5f;
    }
  //  IEnumerator SpawnAsteroidRoutine()
   // {
        
      //  while (_stopSpawning == false)
       // {
       //     yield return new WaitForSeconds(Random.Range(2,5));
       //     Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 3, 0);
       //       Instantiate(_asteroid, posToSpawn, Quaternion.identity);
       // }
   // }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2f); 
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1f,_frequency));
        }
    }

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(2f);
        while (_stopSpawning == false)
        {

            yield return new WaitForSeconds(Random.Range(5f, 15f));
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newTriple = Instantiate(powerups[Random.Range(0,powerups.Length)], posToSpawn, Quaternion.identity);
            newTriple.transform.parent = _enemyContainer.transform;
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        foreach(Transform child in _enemyContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
