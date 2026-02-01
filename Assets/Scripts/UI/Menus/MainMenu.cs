using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject creditsPanel;
    GameObject settingsPanel;
    GameObject player;
    GameObject playerUI;

    private void Start()
    {
        creditsPanel = GameObject.Find("CreditsPanel");
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
        }

        settingsPanel = GameObject.Find("SettingsPanel");
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        player = GameObject.Find("Player");
        if (player)
        {
            player.SetActive(false);
        }

        playerUI = GameObject.Find("UIHudCanvas");
        if (playerUI)
        {
            playerUI.SetActive(false);
        }
    }

    public void StartGame()
    {
        if (player && playerUI)
        {
            player.SetActive(true);
            playerUI.SetActive(true);
            //Don't ask.
            player.transform.position = new Vector3(5.65999985f, 3.31999993f, -6.21000004f);
        }
        else
        {
            Debug.Log("Failed to find the player prefab :(");
        }
        
        SceneManager.LoadScene("BaseScene");
    }

    public void GoToMainMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }

    public void Settings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
        }
    }

    public void Credits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(!creditsPanel.activeInHierarchy);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
