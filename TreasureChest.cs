using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;
    [Header("Contents")]
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    [Header("Contents")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if (isOpen)
        {
            anim.SetBool("OpenedAlready", true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("attack") && playerInRange)
        {
            if (!isOpen)
            {
                //openthechest
                OpenChest();
            }
            else
            {
                //Chest is openalready
                ChestIsOpen();
            }
        }
    }

    public void OpenChest()
    {
        //dialog window on
        dialogBox.SetActive(true);
        //dialogtext=content text
        dialogText.text = contents.itemDescription;
        //add contents to inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        //Raise the signal to the player to animate
        raiseItem.Raise();
        //set chest open
        isOpen = true;
        anim.SetBool("Opened", true);

        //raise the context clue
        context.Raise();
        storedOpen.RuntimeValue = isOpen;
    }

    public void ChestIsOpen()
    {
       
            //dialog off 
            dialogBox.SetActive(false);
            //set thecurrent item to empty
          //  playerInventory.currentItem = null;
            //raie the signal to the player to stop animating
            raiseItem.Raise();
           
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;

        }
    }

}
