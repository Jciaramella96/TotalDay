using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSpider : Log
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

   
    // Update is called once per frame
  //check distanec is called from logs fixed update which means it will be called from patrolspider's fixed update
   public override void CheckDistance() //inherits info from log class and override is used to replace the base class in the log script
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius   // player position, logs position 
                            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); //Time.deltaTime = amount of time since last frame, essentially the movespeed per second

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
               // ChangeState(EnemyState.walk);
                anim.SetBool("WakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position) >roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);// mothing towards point path in current point
                                                                                                                                //anim.SetBool("WakeUp", false);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }
    }


    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1){//if on end of path then reset it
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++; //go to next point, incre pt
            currentGoal = path[currentPoint];
        }
    }
}
