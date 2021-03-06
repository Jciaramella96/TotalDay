using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class AI : MonoBehaviour
{
    public bool switchState = false;
    public float gameTimer;
    public int seconds = 0;
    public Animator animator;
    public FloatValue maxHealth;
    public float currentHP;
    public StateMachine<AI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(FirstState.Instance);
        gameTimer = Time.time;
        currentHP = maxHealth.RunTimeValue;
    }

    private void Update()
    {
        if (currentHP <= 4)
        {
            animator.SetBool("Attack", true);
            
        }
       
        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }



        if (seconds == 2)
        {

            // seconds = 0;
            animator.SetBool("Attack", true);

           
        }
        if (seconds == 5)
        {

            seconds = 0;
            animator.SetBool("Attack", false);

            switchState = !switchState;
        }
        stateMachine.Update();
        //animator.SetBool("Attack", false);
        
    }
}
