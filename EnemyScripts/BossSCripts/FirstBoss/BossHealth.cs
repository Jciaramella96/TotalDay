using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    public Slider bossSlider;
    public FloatValue bossHealth;
    public Signal decreaseHP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DecreaseHealth()
    {
        //  magicSlider.value -= 1;
        // playerInventory.currentMagic -= 1;
        bossSlider.value = bossHealth.RunTimeValue;
        bossHealth.RunTimeValue -= bossHealth.RunTimeValue;
        decreaseHP.Raise();
        if (bossSlider.value < 0)
        {
            bossHealth.RunTimeValue = 0;
        }
    }
}
