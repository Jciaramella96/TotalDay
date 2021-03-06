using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToIncrease;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger && playerHealth.RunTimeValue < playerHealth.initialValue)
        {

            playerHealth.RunTimeValue += amountToIncrease;
            if(playerHealth.initialValue > heartContainers.RunTimeValue * 2f || playerHealth.RunTimeValue > playerHealth.initialValue) //if health would be more than max, ie 5 plus 2 =7, than just set health equal to max health
            {
                playerHealth.RunTimeValue = playerHealth.initialValue;
                playerHealth.initialValue = heartContainers.RunTimeValue * 2f;
            }
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }


}
