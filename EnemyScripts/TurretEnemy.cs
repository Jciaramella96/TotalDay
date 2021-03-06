using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Log
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
        
    }
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius     // player position, logs position, if player is in enemies chase radius
                           && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;    
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity); //creating the proejctile
                    current.GetComponent<Projectile>().Launch(tempVector);
                    canFire = false;
                    ChangeState(EnemyState.walk);
                    anim.SetBool("WakeUp", true);
                }
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("WakeUp", false);
        }
    }
}
 