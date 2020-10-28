﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite; // lets reference sprite 
    private SpriteRenderer mySprite; //references sprite renderer to shut on and off
    public Door thisDoor;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedValue.RuntimeValue;
        if (active)
        {
            ActivateSwitch();
        }
        
    }


    public void ActivateSwitch()
    {
        active = true;
        storedValue.RuntimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //is it the player
        if (other.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }
}