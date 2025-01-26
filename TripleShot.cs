using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
  
        if(transform.position.y >= 7)
        {
            Destroy(this.gameObject);
        }
    }
}
