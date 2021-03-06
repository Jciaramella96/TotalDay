using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// keep time dependant stuff out of FixedUpdate, 
//put it in Update, put movement stuff in FixedUpdate
public class FrogScript : MeleeEnemy
{
    
    [Header("ProjectileSTUFF")]
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public Transform bossPos;
    public Projectile shot;
    public float gameTimer;
    private bool tShotBool;
    public int seconds = 0;
    [Header("Movement")]
    
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;
    public ChangeColors blue; // color changer script 
    // Start is called before the first frame update
    void Start()
    {
        blue = GetComponent<ChangeColors>();
    }

     void FixedUpdate()
    {
        //AttackPlayer();
          CheckDistance();

        // Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);// mothing towards point path in current point

        //changeAnim(temp - transform.position);
        // myRigidbody.MovePosition(temp);

        
     
        //  CheckDistance();
        if (health <= 0)
        {
            StopAllCoroutines();
            Destroy(this.gameObject);

        }

    }
    private void Update()
    {
        PhaseChange();

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

    }
   
    void ChasePlayer()
    {
       
            Vector3 temp = Vector3.MoveTowards(transform.position,
                                      target.position,
                                     moveSpeed * Time.deltaTime); //Time.deltaTime = amount of time since last frame, essentially the movespeed per second

            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);

        
    }
    void AttackPlayer()
    {
        if ((Vector3.Distance(target.position, transform.position) <= chaseRadius   // player position, logs position 
                           && Vector3.Distance(target.position, transform.position) <= attackRadius)) //within attack radius
        {
            
                StartCoroutine(AttackCo());
            
        }
    }
 

   public void fireMode()
    {
        StartCoroutine(RedMode());


    }
    public  void icemode()
    {
        StartCoroutine(BlueMode());
    
    }
    public void targetMove()
    {
          Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);// mothing towards point path in current point
         changeAnim(temp - transform.position);
          myRigidbody.MovePosition(temp);
    }
    IEnumerator RedMode()
    {

        blue.ChangeRed();
        Vector3 Rtemp = Vector3.MoveTowards(transform.position, path[2].position, moveSpeed * Time.deltaTime);
        moveSpeed = moveSpeed * 2;
        changeAnim(Rtemp - transform.position);

        myRigidbody.MovePosition(Rtemp);
        yield return new WaitForSeconds(3);
     
        StopAllCoroutines(); //maybe useful lets see if works like we think

    }
    IEnumerator BlueMode()
    {
        blue.ChangeBlue();
        Vector3 temp = Vector3.MoveTowards(transform.position, path[3].position, moveSpeed * Time.deltaTime);
        changeAnim(temp - transform.position);

        myRigidbody.MovePosition(temp);
        yield return new WaitForSeconds(3);
        blue.ChangeBack();
    }
    // PUT THE mode he gets into after hit by fireball/icebolt into a method to call after such a collision
    IEnumerator multiShotCo()
    {
        yield return new WaitForSeconds(1);

        MultiShot();
        
        yield break;

    }


    public  void OnTriggerEnter2D(Collider2D other)
    {
        // move to frogscript
        if (other.gameObject.CompareTag("BlackFire") && other.isTrigger)
        {
            Debug.Log("ICEBOLT HIT");
            //icemode();
        }
        //move to frogscript
        if (other.gameObject.CompareTag("Burnable") && other.isTrigger)
        {
            Debug.Log("FireBOLT HIT");
           // fireMode();
            targetMove();
        }
    }

    void TripleShot()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);

        Projectile tripleShot = Instantiate(shot, bossPos.position, lookRotation);
        tripleShot.position = bossPos.position;
        tripleShot.transform.eulerAngles = new Vector3(0, 0, 180);
        Projectile tripleShot2 = Instantiate(shot, bossPos.position, lookRotation);
        tripleShot2.position = bossPos.position;
        tripleShot2.transform.eulerAngles = new Vector3(0, 0, 210);
        Projectile tripleShot3 = Instantiate(shot, bossPos.position, lookRotation);
        tripleShot3.position = bossPos.position;
        tripleShot3.transform.eulerAngles = new Vector3(0, 0, 240);

    }

    void MultiShot() // remember to set canFire to false at end of for loop so bullets dont create infininetly
    {
        if (canFire)
        {
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
            
            Projectile currentWave = Instantiate(shot, bossPos.position, lookRotation);
            for (int fireAngle = 0; fireAngle< 180; fireAngle += 60)
            {
                currentWave = Instantiate(shot, bossPos.position, lookRotation); // this makes the all the shots come from bossPos
                                                                                 // just doing currentWave=Instantiate(shot) will make then shots come from where the Projectile in shot variable transform is
        currentWave.position = bossPos.position;
                currentWave.transform.eulerAngles = new Vector3(0, 0, fireAngle);
            
            canFire = false;
            }
           // canFire = false;
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
    void PhaseChange()
    {
        if (health <= 5)
        {
             fireMode();
      
        }

        if (health>= 6)
        {
            if (!tShotBool)
            {
                TripleShot();
                tShotBool = true;
            }
            StopCoroutine(RedMode());
            //   icemode();

        }
        if (seconds >= 9)
        {
            blue.ChangeBack();

        }

        if (seconds >= 15)
        {
            //  ChasePlayer();

        }
        if (seconds >= 20)
        {
            seconds = 0;
        }
    }
}
