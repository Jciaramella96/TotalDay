using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmegolScript : Log
{
    //public PlayerMovement plyr;
    public PlayerMovement play;
       private Animator anim;
    private float eyeDircX;
    private float eyeDircY;// direction eye looks, should b following player

    [Header("Point Grps")]
    public GameObject Tgrp;
        public GameObject idleGrp;
    [Header("Movement")]
    public Transform[] Tpath;
    public Transform Tpoint;
    public int TcurrP;
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;
    [Header("Timer")]
    public float gameTimer;
    public int seconds = 0;
    private Vector3 TriTemp;
    // Start is called before the first frame update
    void Start()
    {
        //plyr = GetComponent<PlayerMovement>();
        play = GameObject.FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
        
    }
    private void FixedUpdate()
    {

        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }
        Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);// mothing towards point path in current point
        //Vector3 TriTemp = Vector3.MoveTowards(transform.position, Tpath[TcurrP].position, moveSpeed * 2 * Time.deltaTime);
        //   changeAnim(temp - transform.position);
        myRigidbody.MovePosition(temp);
        
            if (transform.position == path[currentPoint].position)
            {
                ChangeGoal();
            }
        
    /*
        if(seconds==10)
        {
            idleGrp.SetActive(false);
            Tgrp.SetActive(true);
            myRigidbody.MovePosition(TriTemp);
            if (transform.position == Tpath[TcurrP].position)
            {
                TGoal();
            }

        }
    */
      

    }
    // Update is called once per frame
    void Update()
    {
     

        Debug.Log(play.returnX());
        EyeBallFollowPlyr();
        eyeDircX = play.returnX();

        Debug.Log(eyeDircX);
        Debug.Log(eyeDircY);
    }

    public void EyeBallFollowPlyr()
    {
    //    eyeDirc.x = play.returnX();
      //  eyeDirc.y = play.returnY();
   //   anim.SetFloat("moveX",eyeDirc.x);
      //  anim.SetFloat("moveY", eyeDirc.y);
        return;
    }




    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {//if on end of path then reset it
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++; //go to next point, incre pt
            currentGoal = path[currentPoint];
        }



    }
    private void TGoal()
    {
    //    myRigidbody.MovePosition(TriTemp);

       // Tgrp.SetActive(true);
     //   idleGrp.SetActive(false);
        if (TcurrP == Tpath.Length - 1)
        {
            //reset if at end of path
            TcurrP = 0;
            Tpoint = Tpath[0];
        }
        else

        {

            TcurrP++;
            currentGoal = Tpath[TcurrP];

            
        }
    }
}
