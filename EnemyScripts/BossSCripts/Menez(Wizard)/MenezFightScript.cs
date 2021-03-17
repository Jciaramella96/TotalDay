using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenezFightScript : Log
{
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
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 8)
        {
            MoveBoss();
        }
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
        if (seconds == 3)
        {
            //   TripleShot();
            //    anim.SetBool("isCasting", true);//set wizard back to regular animations
            anim.SetBool("isCasting", true);
            bigShot();
              StartCoroutine(TripleShot());
            SpawnLogs();
            seconds = 0;
        }
        //doing this so we dont have to call coroutine in order for this transition to work. 
        if (seconds == 6)
        {
          //  anim.SetBool("isCasting", false);
            
        }
    }
    
    //multishot from boss
    void bigShot()
    {
        //anim.SetBool("isCasting", true);//make him look spellcastey
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
       // Projectile currentWave = Instantiate(shot, bossPos.position, lookRotation);
        for (int fireAngle = 30; fireAngle < 300; fireAngle += 30)
        {
            Projectile currentWave = Instantiate(shot, bossPos.position, lookRotation); // this makes the all the shots come from bossPos
                                                                             // just doing currentWave=Instantiate(shot) will make then shots come from where the Projectile in shot variable transform is
            currentWave.position = bossPos.position;
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
        Vector3 temp = Vector3.MoveTowards(transform.position, Pt.position, moveSpeed * Time.deltaTime);
        changeAnim(temp - transform.position);

        myRigidbody.MovePosition(temp);
        yield return new WaitForSeconds(.3f);
    }
    void MoveBoss()
    {
        // as of rn he walks to the position, when out of chase radius of log, so will be a none issue once his pathing for the fight is sorted out

        StartCoroutine(PosMove());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            // TakeDamage(1);
            Debug.Log("HITHITHIT");
            MoveBoss();
        }
    }

    void SpawnLogs()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);

        //make a for loop instanting eniemies at pointers on map
        Log newLog = Instantiate(enemyLog, spwnPt.position, Quaternion.identity);

    }
}
