using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColors : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private Color redColor;
    [SerializeField] private Color blueColor;


 //   [SerializeField] private Color flashColor;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private float flashDelay;
    private bool isFlashing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeBlue()
    {
        mySprite.color = blueColor;
    }
    public void ChangeRed()
    {
        mySprite.color = redColor;
    }

    public void ChangeBack()
    {
        mySprite.color = Color.white;
    }


    public void StartFlash()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashCo());
        }
    }
    public void StopFlash()
    {
        StopCoroutine(FlashCo());
    }

    public IEnumerator FlashCo()
    {
        isFlashing = true;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            if (mySprite)
            {
              //  mySprite.color = flashColor;
                yield return new WaitForSeconds(flashDelay);
                mySprite.color = Color.white;
                yield return new WaitForSeconds(flashDelay);
            }
        }
        isFlashing = false;
    }
}
