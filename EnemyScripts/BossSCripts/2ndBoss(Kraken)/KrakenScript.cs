using System.Collections;
using System.Collections.Generic;
using UnityEngine;












public class KrakenScript : Log
{
    [Header("WaveProjectileStuff")]
    public Projectile shot;
   // public WaveBullet waveShot;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public Transform bossPos;
  
    [Header("Movement")]
   
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;
    [Header("Timer")]
    public float gameTimer;
    public int seconds = 0;
   

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    
        private void FixedUpdate()
        {
       
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
        
        Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);// mothing towards point path in current point
       
     //   changeAnim(temp - transform.position);
        myRigidbody.MovePosition(temp);

        if (transform.position == path[currentPoint].position)
        {
            ChangeGoal();
        }
        //  Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.fixedDeltaTime);
        if (seconds == 3)
        {
            // possibly have diff seconds correlate to amount of waves that come out, 2 sec 2 waves, 4 sec 4 waves

            
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
            Projectile currentWave = Instantiate(shot, bossPos.position, lookRotation);
            for (int fireAngle = 30; fireAngle < 360; fireAngle += 30)
            {
                currentWave = Instantiate(shot, bossPos.position,lookRotation); // this makes the all the shots come from bossPos
                                                          // just doing currentWave=Instantiate(shot) will make then shots come from where the Projectile in shot variable transform is
                currentWave.position = bossPos.position;
                currentWave.transform.eulerAngles = new Vector3(0, 0, fireAngle);
       

             }
            
            seconds = 0;
            // moving towards point path in current point
            //  TakeDamage(1);                                                                                                        //anim.SetBool("WakeUp", false);
            // changeAnim(temp - transform.position);
            // myRigidbody.MovePosition(temp);
        }
        if (seconds == 5)
        {
            //   ChangeGoal();
          
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
            Projectile currentWave = Instantiate(shot, bossPos.position, lookRotation);
            for (int fireAngle = 30; fireAngle < 180; fireAngle += 30)
            {
                currentWave = Instantiate(shot, bossPos); // this makes the all the shots come from bossPos
                                                          // just doing currentWave=Instantiate(shot) will make then shots come from where the Projectile in shot variable transform is
                currentWave.position = bossPos.position;
                currentWave.transform.eulerAngles = new Vector3(0, 0, fireAngle);

              //  seconds = 0;
            }
        }
        

    }
    // get rid of check distance, just us the fireangle loop and lookRotation lines, in order to make circle of waves attack
    /*

   // have to find way to access position of projectiles were instantiating, possibly adjust projectile script of create new script
    Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
 // WaveBullet currentWave = Instantiate(waveShot, transform.position, lookRotation);
  //  Projectile currentWave = Instantiate(shot, bossPos.position, lookRotation);
          /*
     for(int fireAngle = 30; fireAngle <360; fireAngle += 30)
         {
currentWave = Instantiate(shot,bossPos); // this makes the all the shots come from bossPos
 // just doing currentWave=Instantiate(shot) will make then shots come from where the Projectile in shot variable transform is
                        currentWave.position = bossPos.position;
                        currentWave.transform.eulerAngles = new Vector3(0, 0, fireAngle);
                    }
                                                      
                    Vector3 cone1 = lookRotation.eulerAngles + new Vector3(0, 0, 30);
                    Vector3 cone2 = lookRotation.eulerAngles + new Vector3(0, 0, -30);
                    currentWave = Instantiate(shot);
                    //currentWave.position = bossPos.position;
                    currentWave.transform.eulerAngles = cone1;
                    currentWave = Instantiate(shot);
                   // currentWave.position = bossPos.position;
                    currentWave.transform.eulerAngles = cone2;
                    
                
        */
    // current have boss moving to 3 diff points and can shoot out his waves whenever
    /*
    void TakeDamage(float damage)
    {
        health -= damage;
      
        if (health <= 0)
        {
            DeathEffect();
            MakeLoot();
            if (roomSignal != null)
            {
                roomSignal.Raise();
            }
            this.gameObject.SetActive(false);

        }
    }
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           // TakeDamage(1);
        }
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
