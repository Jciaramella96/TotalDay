using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy

{
    public Rigidbody2D myRigidbody;
    [Header("Target Variables")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
   // public Transform homePosition;
    [Header("Animator")]
    public Animator anim;
    
              // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        anim.SetBool("WakeUp", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }
  public virtual void CheckDistance()
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
                ChangeState(EnemyState.walk);
                anim.SetBool("WakeUp", true);
            }
        } else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("WakeUp", false);
        }
    }
    //long way of doing anim for enemy
    /*
    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
    }
    */
   public void changeAnim(Vector2 direction)
    {
        //short way of doing anim for enemy
        direction = direction.normalized;
        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);
        //long way of doing anim for enemy
        /*
        if(Mathf.Abs(direction.x)> Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }else if(direction.x<0)
                {
                SetAnimFloat(Vector2.left);
            }
        }else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
        */
    }
   public void ChangeState(EnemyState newState) //method to change state and check state
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}
