using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager _instance;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject credits;
    public GameObject difficultyMenu;
    public static MainMenuManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MainMenuManager>();

            }
            if (_instance == null)
            {
                Debug.LogError("No MainMenuManager in scene");

            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        BackToMenu();
    }

    public void StartGame()
    {
        if (!FirstTime())
        {
            if (PlayerPrefs.GetInt("GameCompleted") == 1)//If I have completed the game
            {
                PlayerPrefs.SetInt("ActualLevel", 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("ActualLevel"));//Then I will consult
                //an integer with a different key, like "Difficulty", to have different settings
            }
            else //If I have not completed the game, I will start from the scene I abandoned before
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("ActualLevel"));
            }
        }
        else //If it's your first time, set every PlayerPrefs key 
        {
            PlayerPrefs.SetInt("GameCompleted", 0);
            PlayerPrefs.SetInt("ActualLevel", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("ActualLevel"));
        }    
        
            
            
    }

    private bool FirstTime()
    {
        if (!PlayerPrefs.HasKey("GameCompleted"))
            return true;
        return false;
    }



    public void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
        StartGame();
    }

    public void GoToSettings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void GoToCredits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        credits.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
