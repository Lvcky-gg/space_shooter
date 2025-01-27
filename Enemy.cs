using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
        
            Destroy(this.gameObject);
        }
        
        if(other.tag == "Laser")
        {
            if (_player != null)
            {
                _player.AddToScore(10);
            }
  
            Destroy(other.gameObject);
            Destroy(this.gameObject);
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
