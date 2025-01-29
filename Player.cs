using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
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
    [SerializeField]
    private bool _shields = false;
    [SerializeField]
    private int _score = 0;
    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    void Start()
    {
  
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager> ();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManager == null )
        {
            Debug.LogError("Spawn Manager is null");
        }
        if (_uiManager == null )
        {
            Debug.LogError("UI Manager is null");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Audio Manager on player is null");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
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
        _audioSource?.Play();


    }
    public void Damage()
    {
        if (!_shields)
        {
            _lives--;
            if( _lives == 2 )
            {
                transform.Find("Right_Engine").gameObject.SetActive(true);
            }
            if(_lives == 1)
            {
                transform.Find("Left_Engine").gameObject.SetActive(true);
            }
            _uiManager.UpdateLives(_lives);
          
        }
        else
        {
    
            transform.Find("Shield").gameObject.SetActive(false);
            _shields = false;
        }
     

        if(_lives < 1 )
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void DoubleSpeed()
    {
        _speed = 10f;
        StartCoroutine(SpeedRoutine());
    }
    public void TripleShot()
    {
        _triple_shot = true;
        StartCoroutine(TripleShotRoutine());

    }
    public void Shield()
    {
   
        _shields = true;
        transform.Find("Shield").gameObject.SetActive(true);
        StartCoroutine(ShieldRoutine());
    }
    public void AddToScore(int score)
    {
        _score += score;
        _uiManager.SetScore(_score);
    }


    IEnumerator ShieldRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        transform.Find("Shield").gameObject.SetActive(false);
        _shields = false;
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
