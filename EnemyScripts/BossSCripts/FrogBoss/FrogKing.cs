using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogKing : Boss
{
    
    private Collider2D Fcollider;
    public FrogScript FScript;
     private FlashingStuff flash;
    // Start is called before the first frame update
    void Start()
    {
        flash = GetComponent<FlashingStuff>();
        FScript = GetComponent<FrogScript>();
       //move to frog script
         currentHealth = maxHp;
        healthBar.SetMaxHealth(maxHp);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword") && other.isTrigger) // did this so boss wont take damage when his arm hits player...,  
        {
            TakeDamage(1);
            flash.StartFlash();
        }
        if (other.gameObject.CompareTag("Sword") && other.isTrigger && other.tag==("Bounce")) 
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            Vector2 difference = hit.transform.position - transform.position;
            difference = difference.normalized * 4;
            hit.AddForce(difference, ForceMode2D.Impulse);
        }


    }
    // Update is called once per frame


    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth((int)currentHealth);
        //speed up after half health gone, maybe activate additional points.
        if (currentHealth < 5)
        {

            if (currentHealth <= 0)
            {
            
                Destroy(this.gameObject);
            }
        }
    }
 
}
