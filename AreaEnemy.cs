﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{
    public Collider2D boundary;

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius   // player position, logs position 
                           && Vector3.Distance(target.position, transform.position) > attackRadius
                           && boundary.bounds.Contains(target.transform.position))
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); //Time.deltaTime = amount of time since last frame, essentially the movespeed per second

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("WakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius
            || !boundary.bounds.Contains(target.transform.position))
        {
            anim.SetBool("WakeUp", false);
        }
    }

}
