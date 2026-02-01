using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseExitZone : MonoBehaviour
{
    [SerializeField]
    private string targetScene;
    [SerializeField]
    private Vector3 spawnPointInTargetScene;

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
                //StartCoroutine(LoadAsyncScene(collider.gameObject));
                LoadScene(collider.gameObject);
            }
        }
    }

    void LoadScene(GameObject player)
    {
        player.transform.position = spawnPointInTargetScene;
        SceneManager.LoadScene(targetScene);
    }

    IEnumerator LoadAsyncScene(GameObject player)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        ;
        player.transform.position = spawnPointInTargetScene;
    }
}
