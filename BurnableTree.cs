using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableTree : MonoBehaviour
{
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Burnable"))
        {
            DeathEffect();
            Destroy(this.gameObject);
        }
    }
}
