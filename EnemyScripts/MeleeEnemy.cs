﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius   // player position, logs position 
                            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, 
                                          target.position, 
                                         moveSpeed * Time.deltaTime); //Time.deltaTime = amount of time since last frame, essentially the movespeed per second

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("WakeUp", true);
            }
        }
        else if ((Vector3.Distance(target.position, transform.position) <= chaseRadius   // player position, logs position 
                            && Vector3.Distance(target.position, transform.position) <= attackRadius)) //within attack radius
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                StartCoroutine(AttackCo());
            }                                    
        }
       
    }
    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
        //yield return new WaitForSeconds(0.3f);   // use this for 2 attacks with slight pause
      //  currentState = EnemyState.walk;
    }
   
}
