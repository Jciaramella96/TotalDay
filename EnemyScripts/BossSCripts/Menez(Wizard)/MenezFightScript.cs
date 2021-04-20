using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenezFightScript : Log
{
    public int currentPoint;
    public Transform currentGoal;
    [Header("WaveProjectileStuff")]
    public Projectile shot;
    public Log enemyLog;
    // public WaveBullet waveShot;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public Transform bossPos;
   // public Transform target;
    [Header("Timer")]
    public float gameTimer;
    public int seconds = 0;
   // public Animator anim;
    public Transform spwnPt;
    public Transform[] path;
    public Transform Pt;
    public Transform Pnt2;
    int currPt = 0;
    private int batCount;
    
    // Start is called before the first frame update
    void Start()
    {
             anim = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        CheckDistance();
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }

        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }
        if (health >= 5)
         //   Phase1();
        if (seconds == 2)
        {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);// mothing towards point path in current point
                                                                                                                                //anim.SetBool("WakeUp", false);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                
        }
        if (seconds == 4)
        {
            //   TripleShot();
            //    anim.SetBool("isCasting", true);//set wizard back to regular animations
            // have to do it in this order, 
            // bigShot, then Coroutrine TripleShot so animation of him casting spells doesn't get skipped.
            anim.SetBool("isCasting", true);
             bigShot();
           // SplitShot();
              StartCoroutine(TripleShot()); // just a delay
            ChangeGoal();
            if (batCount < 4)
            {
                SpawnBats();
                batCount++;
            }
            seconds = 0;
        }
        //doing this so we dont have to call coroutine in order for this transition to work. 
  
    }
    
    //multishot from boss, shoots 3 shots in triangle 
                                      //*
                                    //Boss
                        //        *         *
    void bigShot()
    {
        //anim.SetBool("isCasting", true);//make him look spellcastey
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
       //Projectile currentWave = Instantiate(shot, bossPos.position, lookRotation);
        for (int fireAngle = 0; fireAngle < 360; fireAngle += 120)
        {
            Projectile currentWave = Instantiate(shot, bossPos.position, lookRotation); // this makes the all the shots come from bossPos
                                                                             // just doing currentWave=Instantiate(shot) will make then shots come from where the Projectile in shot variable transform is
            currentWave.position = bossPos.position;
            currentWave.transform.eulerAngles = new Vector3(0, 0, fireAngle);
            if(fireAngle== 120)
            {
                Projectile currentWave2 = Instantiate(shot, bossPos.position, lookRotation); // this makes the all the shots come from bossPos
                                                                                            // just doing currentWave=Instantiate(shot) will make then shots come from where the Projectile in shot variable transform is
                currentWave2.position = bossPos.position;
                currentWave2.transform.eulerAngles = new Vector3(0, 0, 180);
            }

        }

    }

    void Phase1()
    {
        MoveBoss();
        StartCoroutine(TripleShot());
     //   MoveBoss2();
      
    }



    //going to modify big shot so it is like 3 projectiles firing towards player
    void SplitShot()
    {
        Vector3 playerPos = new Vector3(target.position.x, target.position.y + 1, target.position.z);

        // in player direction 
        Quaternion playerRot = Quaternion.LookRotation(playerPos);



        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
         Projectile currentWave = Instantiate(shot, bossPos.position, lookRotation); // fires right at player position
        for (int fireAngle = 30; fireAngle < 300; fireAngle += 10)
        {
            // Projectile 
            //    currentWave = Instantiate(shot, bossPos.position, lookRotation); // this makes the all the shots come from bossPos
            //  currentWave = Instantiate(shot, currentWave.position, playerRot);    
            currentWave = Instantiate(shot,  currentWave.position, playerRot); // make a huge ring of projectiles in the middle of the screen?
         



            // just doing currentWave=Instantiate(shot) will make then shots come from where the Projectile in shot variable transform is
            //currentWave.position = bossPos.position;
          currentWave.transform.eulerAngles = new Vector3(0, 0, fireAngle);


        }

    }
    private IEnumerator TripleShot()
    {
     
        
       
        yield return new WaitForSeconds(.5f);
        anim.SetBool("isCasting", false);
    }

    private IEnumerator PosMove()
    {
        int num = Random.Range(0, 1);
        Vector3 temp = Vector3.MoveTowards(transform.position, path[0].position, (moveSpeed*moveSpeed) * Time.deltaTime);
        changeAnim(temp - transform.position);
        Debug.Log(currPt);
        myRigidbody.MovePosition(temp);
        yield return new WaitForSeconds(.6f);
        
    }
    void MoveBoss()
    {
        // as of rn he walks to the position, when out of chase radius of log, so will be a none issue once his pathing for the fight is sorted out

        StartCoroutine(PosMove());
        
    }
    // just copying n pasting above method, as i dont feel like figuring out looping thru points right now
    private IEnumerator PosMove2()
    {
        int num = Random.Range(0, 1);
        Vector3 temp = Vector3.MoveTowards(transform.position, path[1].position, (moveSpeed * moveSpeed) * Time.deltaTime);
        changeAnim(temp - transform.position);
        Debug.Log(currPt);
        myRigidbody.MovePosition(temp);
        yield return new WaitForSeconds(.6f);

    }
    void MoveBoss2()
    {
        StartCoroutine(PosMove2());
    }
 
    void SpawnBats()
    {
        Vector3 bossPos = transform.position;
        Vector3 bossDir = transform.forward;
        Quaternion bossRotat = transform.rotation;
        float dist = 30;
        int num = Random.Range(0, 2);
        Vector3 spawnSpot = bossPos + bossDir * dist;
        //Log newBat = Instantiate(enemyLog,  transform.position- target.position, Quaternion.identity);  
        //Log newBat = Instantiate(enemyLog,  path[num].position, Quaternion.identity);
        Log newBat = Instantiate(enemyLog, Pt.position, Quaternion.identity);


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
}
