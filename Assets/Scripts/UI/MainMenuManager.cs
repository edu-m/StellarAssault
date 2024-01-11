using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager _instance;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject credits;
    public GameObject difficultyMenu;
    public GameObject scoreBox;
    public TextMeshProUGUI scoreText;
    public TMP_InputField playerName;

    List<Score> scores;
    public static MainMenuManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MainMenuManager>();
            if (_instance == null)
                Debug.LogError("No MainMenuManager in scene");
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
            Destroy(this.gameObject);
        }

        scores= new List<Score>();
    }
    // Start is called before the first frame update
    void Start() => ToMenu();

    public void StartGame()
    {
        if (!FirstTime())
        {
            if (PlayerPrefs.GetInt("GameCompleted") == 1)//If I have completed the game
            {
                PlayerPrefs.SetInt("ActualLevel", 1);
                PlayerPrefs.SetInt("GameCompleted", 0);
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
            SceneManager.LoadScene(PlayerPrefs.GetInt("ActualLevel"));
        }

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private bool FirstTime() => !PlayerPrefs.HasKey("GameCompleted");
    public void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
        //Debug.Log("Player name is " + PlayerPrefs.GetString("PlayerName"));
        StartGame();
    }

    public void SetPlayerName() => PlayerPrefs.SetString("PlayerName", playerName.text);

    public void SetPlayerNameText() 
    {
        if (PlayerPrefs.HasKey("PlayerName"))
            playerName.text = PlayerPrefs.GetString("PlayerName");
        else 
        {
            playerName.text = "None";
            PlayerPrefs.SetString("PlayerName", playerName.text);
        }
            
    }

    public void ShowScoreBox()
    {
        scores=FileHandler.ReadListFromJSON<Score>("scoreBox");
        foreach(Score score in scores)
        {
            if(score.playerDifficulty != -1)
            {
                if (!score.playerScoreMode)
                    scoreText.text = scoreText.text + score.playerName + " " + score.playerScore + " Level " +
                    score.playerLevel + " Difficulty " + score.playerDifficulty + " Stealth Mode" + "\n";
                else
                    scoreText.text = scoreText.text + score.playerName + " " + score.playerScore + " Level " +
                     score.playerLevel + " Difficulty " + score.playerDifficulty + " Direct Mode" + "\n";
            }
            else
                scoreText.text = scoreText.text + score.playerName + " " + score.playerScore + " Level " +
                     score.playerLevel + "\n";

        }
    }
    public void ToMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        credits.SetActive(false);
    }

    public void QuitGame() => Application.Quit();
}
