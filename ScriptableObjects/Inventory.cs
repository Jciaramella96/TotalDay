using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
[System.Serializable]
public class Inventory :ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public float maxMagic = 15;
    public float currentMagic;
    public void OnEnable()
    {
        currentMagic = maxMagic;
    }
    public void ReduceMagic(float magicCost)
    {
        currentMagic -= magicCost;
    }
    public void AddItem(Item itemToAdd)
    {

        //istheitema key
        if (itemToAdd.isKey)
        {
            numberOfKeys++;

        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

    public bool CheckForItem(Item item)
    {
        if (items.Contains(item))
        {
            return true;
        }else
        {
            return false;
        }
    }
        //possible clear inventory method, start game with all your stuff, find babur, lose, lose memory, have to regain everything in order to fight him again.
}
