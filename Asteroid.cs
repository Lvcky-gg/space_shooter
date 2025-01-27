using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 5.0f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    
    void Start()
    {
    
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null )
        {
            Debug.Log("Spawn Manager is null");
        }
    }

    
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Player player = collider.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }


        }
        else if(collider.tag == "Laser")
        {
            Destroy(collider.gameObject);
            _spawnManager.ResetFrequency();
        }

        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 0.25f);
        _spawnManager.StartSpawning();
    }
}
