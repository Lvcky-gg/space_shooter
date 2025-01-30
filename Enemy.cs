using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    private Player _player;
    private Animator _anim;
    [SerializeField]
    private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 2.0f;
    private float _canFire = -1;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null )
        {
            Debug.Log("Player is null");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.Log("Animator is null");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Audio Manager on enemy is null");
        }
        else
        {
            _audioSource.clip = _explosionSoundClip;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {

        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject,2.3f);
            _audioSource?.Play();
        }
        
        if(other.tag == "Laser")
        {
            if (_player != null)
            {
                _player.AddToScore(10);
            }
  
            Destroy(other.gameObject);
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject,2.3f);
            _audioSource?.Play();
            Destroy(GetComponent<Collider2D>());
        }
    }

    void Update()
    {
        CalculateMovement();
        if (Time.time > _canFire)
        {
            float rate = GetComponentInParent<SpawnManager>().GetFrequency();
            _fireRate = Random.Range(rate, rate*2);
            _canFire = Time.time + _fireRate;
            GameObject lasers = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] laserArr = lasers.GetComponentsInChildren<Laser>();
            
            for(int i = 0;i < laserArr.Length; i++)
            {
                laserArr[i].AssignEnemyLaser();
            }
        }

    }
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5f)
        {
            GetComponentInParent<SpawnManager>().ModFrequency();
            //every two time this happens, add a shield to the one that came through

            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
}
