using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{ // in our case, room is 20 above the original one, so y change is 20, in order to move camera up
    public Vector2 cameraChange; // if rooms are diff size, will need two variables, one for change in min one for change in max
    public Vector3 playerChange; // if rooms are all same size should be fine with one
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText; //reference to text on object^
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.isTrigger) // makes so that it only activates on the Collider that isTrigger
        {                                                  // instead of both colliders that are currently on Player
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()    //IENumberator,method that can run parallel to other processes and has a specified wait time
    {
        text.SetActive(true); // set text active
        placeText.text = placeName;
        yield return new WaitForSeconds(4f); // wait 4 seconds then text disappears
        text.SetActive(false);
    }
  

}
