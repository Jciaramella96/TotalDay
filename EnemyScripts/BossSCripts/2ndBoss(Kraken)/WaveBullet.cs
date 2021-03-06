using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBullet : MonoBehaviour
{
    public float moveSpeed;
    public float lifetime;
    internal Vector3 position;          // added these two in order to solve problem within KrakenScript on line 50,51
    internal object Rotation;
    private float lifetimeSeconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.fixedDeltaTime);

        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
