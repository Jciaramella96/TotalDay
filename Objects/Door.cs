using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}
public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    
    private void Update()
    {
        if (Input.GetButtonDown("attack"))
        {
            if (playerInRange&& thisDoorType == DoorType.key) //does plaer ahve key
            {
                //does the player have a key
                if (playerInventory.numberOfKeys > 0)
                {
                    //remove key
                    playerInventory.numberOfKeys--;
                    //if so call the open method
                    Open();
                }

            }
        }
    }
    public void Open()
    {
        //turn off doors sprite renderer
        doorSprite.enabled = false;
        //set open to true
        open = true;
        //turnn off doors box collidor
        physicsCollider.enabled = false;
    }
    public void Close()
    {
        //turn off doors sprite renderer
        doorSprite.enabled = true;
        //set open to true
        open = false;
        //turnn off doors box collidor
        physicsCollider.enabled = true;
    }
}
