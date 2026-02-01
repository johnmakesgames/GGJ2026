using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject creditsPanel;
    GameObject settingsPanel;

    private void Start()
    {
        creditsPanel = GameObject.Find("CreditsPanel");
        if (creditsPanel == null )
        {
            creditsPanel.SetActive(false);
        }

        settingsPanel = GameObject.Find("SettingsPanel");
        if (settingsPanel != null )
        {
            settingsPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
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
