 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;

    void Update()
    {
        if(_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }

    }
    void MoveDown()
    {
        transform.Translate(Vector3.down * 16f  * Time.deltaTime);
       // Debug.Break();

        if (transform.position.y <= -7)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }
    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 7)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }
    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = collider.GetComponent<Player>();
            if(player != null)
            {
                //DO NOT DO THIS YOU EVIL BASTARD
               // for(int i = 0; i < 3; i++) {
                    player.Damage();
                player.AddLife();
               // }
                
            }
        }
    }
}
