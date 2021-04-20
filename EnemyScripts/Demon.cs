using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Log
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            Knock(myRigidbody,.5f,0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
