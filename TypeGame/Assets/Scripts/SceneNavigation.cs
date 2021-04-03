using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public InputField username;
    public Slider fallSpeed;

    public void StartGame()
    {
        SceneManager.LoadScene("Main");

        string user = username.GetComponent<InputField>().text;
        PlayerPrefs.SetString("Username", user);

        PlayerPrefs.SetFloat("FallSpeed", fallSpeed.value);

        PauseMenu.wordCount = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void Highscores()
    {
        SceneManager.LoadScene("Highscores");
    }
}
