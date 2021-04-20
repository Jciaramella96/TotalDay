using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private float angle = 0;
    private int radius = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Circle();
    }
    void Circle()
    {
        float x = -20;
        float y = 20;
        Vector2 direction = Vector2.zero;
        x = radius * Mathf.Cos(angle);
        y = radius * Mathf.Sin(angle);
       
        transform.position = new Vector2(x, y);
        angle += 25 * Mathf.Deg2Rad * Time.deltaTime; // increase integer at front in order to adjust speed
    }
}
