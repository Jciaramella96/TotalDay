using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    //NewSceneVariables
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
   public Vector2 cameraNewMax;
    public Vector2 cameraNewMin;
    public VectorValue cameraMin;
    public VectorValue cameraMax;
    //Transition variables
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    public Transform target;

    private void Awake() //gets called before start
    {
        if(fadeInPanel != null)
        {
            GameObject panel=Instantiate(fadeInPanel, Vector3.zero,Quaternion.identity) as GameObject;
            Destroy(panel, 1); //destroys the panel after 1 second
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            StartCoroutine(FadeCo());
         //   SceneManager.LoadScene(sceneToLoad);

        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        ResetCameraBound();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    
    public void ResetCameraBound()
    {
        cameraMax.initialValue = cameraNewMax;
        cameraMin.initialValue = cameraNewMin;
    }
}
