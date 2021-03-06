using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [Header("Position Variables")]
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    [Header("Position Reset")]
    public VectorValue camMin;
    public VectorValue camMax;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // get animator component from object this is attached too
        maxPosition = camMax.initialValue;
        minPosition = camMin.initialValue;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
      //  Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x); //bounding camera x position
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);// bounding camera y position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

        }
    }

    public void BeginKick()
    {
        anim.SetBool("kick_active", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("kick_active", false);
    }
}
