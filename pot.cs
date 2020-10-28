using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    private Animator anim;
    public LootTable thisLoot;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash()
    {
        anim.SetBool("smash", true);
        StartCoroutine(breakCo());
        
        
    }
    public void MakeLoot() //if loot table exists, create variable and fill it with whatever loottable says should be  in it
    {
        if (thisLoot != null)
        {
            PowerUp current = thisLoot.LootPowerUp();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity); // create game object on enemy position
            }
        }
    }
    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);
        
        this.gameObject.SetActive(false);
        MakeLoot();
    }

    
   

}
