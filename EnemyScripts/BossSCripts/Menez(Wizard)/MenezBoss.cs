using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenezBoss : Boss
{
    private FlashingStuff flash;
    public MenezFightScript Mscript;
    void Start()
    {
        currentHealth = maxHp;
        healthBar.SetMaxHealth(maxHp);
        flash = GetComponent<FlashingStuff>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword") && other.isTrigger) // did this so boss wont take damage when his arm hits player...,  
        {
            TakeDamage(1);
            flash.StartFlash();
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth((int)currentHealth);
        //speed up after half health gone, maybe activate additional points.
        if (currentHealth < 5)
        {
            Mscript.moveSpeed = Mscript.moveSpeed * 2;
        }
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
