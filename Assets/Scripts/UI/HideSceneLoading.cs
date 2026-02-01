using UnityEngine;
using UnityEngine.SceneManagement;

public class HideSceneLoading : MonoBehaviour
{
    public GameObject m_HideTransition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i =0; i < SceneManager.sceneCount; ++i)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded == false && scene.name != "InventoryScene")
            {
                m_HideTransition.SetActive(true);
                return;
            }
        }

        m_HideTransition.SetActive(false);
    }
}
