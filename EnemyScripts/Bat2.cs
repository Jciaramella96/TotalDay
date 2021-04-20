using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat2 : MonoBehaviour
{
    public float radius = 10f;
    public float speed = 1f;
    public bool offsetIsCenter = true;
    public Vector3 offset;
    private float angle = 5;
 //   private int radius = 20; 
    // Start is called before the first frame update
    void Start()
    {
        if (offsetIsCenter)
        {
            offset = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DiffCircle();
    }

    void Circle()
    {
        float x = -60;
        float y = 10;
        Vector2 direction = Vector2.zero;
        x = radius * Mathf.Cos(angle);
        y = radius * Mathf.Sin(angle);

        transform.position = new Vector2(x, y);
        angle += 25 * Mathf.Deg2Rad * Time.deltaTime; // increase integer at front in order to adjust speed
    }
    void DiffCircle()
    {
        transform.position = new Vector3(
             (radius * Mathf.Cos(Time.time * speed)) + offset.x,
             (radius * Mathf.Sin(Time.time * speed)) + offset.y,
             offset.z);
    }
}
