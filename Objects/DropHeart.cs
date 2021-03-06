using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHeart : MonoBehaviour
{
    private Animator anim;
    public GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

  //  public void Smash()
  //  {
      //  anim.SetBool("smash", true);
      //  StartCoroutine(breakCo());
  //  }

   // IEnumerator breakCo()
   // {
      //  yield return new WaitForSeconds(.3f);
      //  this.gameObject.SetActive(false);
   // }

    public  void dropHeart()
    {
        if (heart != null)
        {
            GameObject heartDrop = Instantiate(heart, transform.position, Quaternion.identity);
            Destroy(heart, 5f);
        }
    }
}

