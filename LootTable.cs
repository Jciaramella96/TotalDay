using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tell monobehavior to serialize
[System.Serializable]
//custom class
public class Loot
{
    public PowerUp thisLoot;
    public int lootChance;

}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

   public PowerUp LootPowerUp()
    {
        //cumulative probability quick n dirty
        int cumProb = 0;
        int currentProb = Random.Range(0, 100); // random prob between 0 and 100
        for(int i=0; i<loots.Length; i++)
        {
            cumProb += loots[i].lootChance;
            if(currentProb <= cumProb)
            {
                return loots[i].thisLoot;
            }
        }
        return null;
    }
}
