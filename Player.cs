using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _triple_shot = false;

    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager> ();

        if(_spawnManager == null )
        {
            Debug.LogError("Spawn Manager is null");
        }

    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Shoot();
        }

    }
    void Shoot()
    {
           _canFire = Time.time + _fireRate;
        if (_triple_shot)
        {
            Instantiate(_tripleShotPrefab, transform.position - new Vector3(0.5f, 0, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        
    }
    public void Damage()
    {
        _lives--;

        if(_lives < 1 )
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void DoubleSpeed()
    {
        _speed = 8.5f;
        StartCoroutine(SpeedRoutine());
    }
    public void TripleShot()
    {
        _triple_shot = true;
        StartCoroutine(TripleShotRoutine());

    }
    IEnumerator SpeedRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        _speed = 3.5f;
    }
    IEnumerator TripleShotRoutine()
    {
         yield return new WaitForSeconds(5.0f);
 
        _triple_shot = false;
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

}
