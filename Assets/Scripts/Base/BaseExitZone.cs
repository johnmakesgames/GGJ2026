using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseExitZone : MonoBehaviour
{
    [SerializeField]
    private string targetScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject != null)
        {
            if (collider.gameObject.tag == "Player")
            {
                StartCoroutine(LoadAsyncScene());
            }
        }
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
