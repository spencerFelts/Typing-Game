using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource music;
    public Toggle musicToggle;
    public static int wordCount;
    public static string username;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text usernameText;

    void Start()
    {
        //wordCount = 0;


        username = PlayerPrefs.GetString("Username");
        usernameText.text = username;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        scoreText.text = "Score: " + wordCount.ToString();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Main");
        Resume();
    }

    public void AudioSetting()
    {
        if (musicToggle.isOn)
        {
            music.Play();
        }
        else
        {
            music.Stop();
        }

    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.score = wordCount;
        save.username = username;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        wordCount = 0;
        username = "";

        Debug.Log("Game Saved");
    }
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            scoreText.text = "Score: " + save.score;
            usernameText.text = save.username;
            wordCount = save.score;
            username = save.username;

            Debug.Log("Game Loaded");

            Resume();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void SaveAsJson()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as Json: " + json);
    }
}
