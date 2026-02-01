using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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

    }

    public void Credits()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
