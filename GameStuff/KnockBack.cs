using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;


   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<pot>().Smash(); // calls smashh method on the pot
        }
        if (other.gameObject.CompareTag("enemy")|| other.gameObject.CompareTag("Player")) //makes knockback for enemy or player
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position; 
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger; //only for enemy
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage); //passes damage and hit detection to enemy
                }
               
                    //enemy.bodyType=RigidbodyType2D.Dynamic;
                    // enemy.isKinematic = false;
                    if (other.gameObject.CompareTag("Player") && other.isTrigger)
                {
                    if(other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponent<PlayerMovement>().Knock(knockTime,damage);
                }
               
                //StartCoroutine(KnockCo(hit));
                
            }
        }
    }
  private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if(enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }


}
