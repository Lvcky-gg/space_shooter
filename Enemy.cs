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
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -5f)
        {
            GetComponentInParent<SpawnManager>().ModFrequency();
            //every two time this happens, add a shield to the one that came through
     
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }

    }
}
