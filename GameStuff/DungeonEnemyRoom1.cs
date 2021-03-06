using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom1 : DungeonRoom
{
    public Door[] door;
    // Start is called before the first frame update
    void Start()
    {
        OpenDoors();
    }

    public int EnemiesActive()
    {
        int activeEnemies = 0;
        for(int i=0; i<enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy)
            {
                activeEnemies++;
            }
        }
        return activeEnemies;
    }

    public void CheckEnemies()
    {
        if (EnemiesActive() == 1)
        {
            OpenDoors();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            CloseDoors();
            virtualCamera.SetActive(true);
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {// deactivate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            virtualCamera.SetActive(false);
        }
    }

    public void CloseDoors()
    {
        for(int i=0; i<door.Length; i++)
        {
            door[i].Close();
        }
        Debug.Log("Close Doors");
    }
    public void OpenDoors()
    {
        for (int i = 0; i < door.Length; i++)
        {
            door[i].Open();
        }
        Debug.Log("Close Doors");
    }
}
