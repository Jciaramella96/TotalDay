using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum PlayerState    //enum is like a bool with extra values, has as many states as you want
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Vector3 change;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    //TODO Break off the health system into its own component
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    //TODO break off inventory into own compnent
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    //TODO player hit should be part of health system>?
    public Signal playerHit;
    //TODO break oof???
    public Signal reduceMagic;
    //TODO break off into with play ability system
    [Header("Projectile Stuff")]
    public GameObject projectile;
    public GameObject projectile2;
    public Item FireRing;
    public Item IceNecklace;
    //TODO Break off into own scripts
    [Header("IFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()  
    {
     //   currentState = PlayerState.walk;
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        anim.SetFloat("MoveX", 0);
        anim.SetFloat("MoveY", -1);
        transform.position = startingPosition.initialValue; // b tower change
       
    }

    // Update is called once per frame
    void Update()
    {
        //is the player in an interaction zone
        if (currentState == PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("attack")&& currentState != PlayerState.attack //sword attack if conditions
                                        && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if(Input.GetButtonDown("Fireball") && currentState != PlayerState.attack //projectile attack if conditions, keyboard buttom M
                                        && currentState != PlayerState.stagger)
        {
            if (playerInventory.CheckForItem(FireRing))
            {
                StartCoroutine(SecondAttackCo());
            }
        }
        else if(Input.GetButtonDown("Icebolt") && currentState != PlayerState.attack  // Keyboard button N, for IceBolt
             && currentState != PlayerState.stagger)
        {
            if (playerInventory.CheckForItem(IceNecklace))
            {   
               
                StartCoroutine(ThirdAttackCo());
            }
        }

       else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimAndMove(); // reference to method, easier and cleaner than including it all in original update method
        }
    }

    private IEnumerator AttackCo()
    {
        anim.SetBool("Attacking", true);
        currentState = PlayerState.attack; // changes state to attack
        yield return null; // waits 1 frame
        anim.SetBool("Attacking", false); //set attacking to false, so you dont go right back into attack animation
        yield return new WaitForSeconds(.1f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator SecondAttackCo() //projectile coroutine
    {
        //anim.SetBool("Attacking", true);
        if (playerInventory.currentMagic > 0)
        {
            currentState = PlayerState.attack; // changes state to attack
            yield return null; // waits 1 frame
            MakeFireball();
            // anim.SetBool("Attacking", false); //set attacking to false, so you dont go right back into attack animation
            yield return new WaitForSeconds(.3f);
            if (currentState != PlayerState.interact)
            {
                currentState = PlayerState.walk;
            }
        }
    }
    //TODO should be part of ability itself
    private void MakeFireball()
    {
       // if (playerInventory > 0) {                  //bracket everything in coroutine
          Vector2 temp = new Vector2(anim.GetFloat("MoveX"), anim.GetFloat("MoveY"));
            FireBall fireball = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<FireBall>();
            fireball.Setup(temp, ChooseFireBallDirection());
            playerInventory.ReduceMagic(fireball.magicCost);
            reduceMagic.Raise();
        // }
    }  //TODO should be part of ability itself
    Vector3 ChooseFireBallDirection()
    {
        float temp = Mathf.Atan2(anim.GetFloat("MoveY"), anim.GetFloat("MoveX")) * Mathf.Rad2Deg;  //arctan of angle, return degree measure in radians
        return new Vector3(0,0,temp);
    }                                                                               //^returns back from rad to degrees



    private IEnumerator ThirdAttackCo() //projectile coroutine
    {
        //anim.SetBool("Attacking", true);
        if (playerInventory.currentMagic > 0)
        {
            currentState = PlayerState.attack; // changes state to attack
            yield return null; // waits 1 frame
            MakeIceBolt();
            // anim.SetBool("Attacking", false); //set attacking to false, so you dont go right back into attack animation
            yield return new WaitForSeconds(.3f);
            if (currentState != PlayerState.interact)
            {
                currentState = PlayerState.walk;
            }
        }
    }
    //TODO should be part of ability itself
    private void MakeIceBolt()
    {
        //  if (playerInventory > 0) {                  //bracket everything in coroutine

        {
            Vector2 temp2 = new Vector2(anim.GetFloat("MoveX"), anim.GetFloat("MoveY"));
            IceBolt icebolt = Instantiate(projectile2, transform.position, Quaternion.identity).GetComponent<IceBolt>();
            icebolt.Setup(temp2, ChooseIceBoltDirection());
            playerInventory.ReduceMagic(icebolt.magicCost);
            reduceMagic.Raise();
            //   }
        }
    }
    //TODO should be part of ability itself
    Vector3 ChooseIceBoltDirection()
    {
        float temp2 = Mathf.Atan2(anim.GetFloat("MoveY"), anim.GetFloat("MoveX")) * Mathf.Rad2Deg;  //arctan of angle, return degree measure in radians
        return new Vector3(0, 0, temp2);
    }
    
    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                          anim.SetBool("receiveitem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                anim.SetBool("receiveitem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x); //rounds the change value so we dont decimals for movement 
            change.y= Mathf.Round(change.y);// and donthave two hitboxes active at once
            anim.SetFloat("MoveX", change.x);
            anim.SetFloat("MoveY", change.y);
            anim.SetBool("Moving", true);
        }
       else
        {
            anim.SetBool("Moving", false);
        }
    }
    //function to give access to player movex and movey
    public float  returnX()
    {

        return change.x;
    }
    public float returnY()
    {
        return change.y;
    }

    void MoveCharacter()
    {
        
        myRigidbody.MovePosition( transform.position + change.normalized * speed * Time.fixedDeltaTime);
    }


    //TODO MOve the knockback to its own  script
    public void Knock(float knockTime, float damage)
    {
        playerHit.Raise();
        currentHealth.RunTimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RunTimeValue > 0)
        {
          
                        StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
      
    }
    //TODO MOve the knockback to its own  script
    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
           currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;

        }
    }
    //TODO MOve the flashing to its own  script
    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }

}
