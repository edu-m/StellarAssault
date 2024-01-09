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
    public List<GameObject> tutorialSlideList;
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

        scores= new List<Score>();
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("ActualLevel"));
        }

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
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
        Debug.Log("Player name is " + PlayerPrefs.GetString("PlayerName"));
        StartGame();
    }

    public void SetPlayerName()
    {
        PlayerPrefs.SetString("PlayerName", playerName.text);
    }

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
            
            if (!score.playerScoreMode)
                scoreText.text = scoreText.text + score.playerName + " " + score.playerScore + " Level " +
                score.playerLevel + " Difficulty " + score.playerDifficulty + " Stealth Mode" + "\n";
            else
                scoreText.text = scoreText.text + score.playerName + " " + score.playerScore + " Level " +
                 score.playerLevel + " Difficulty " + score.playerDifficulty + " Direct Mode" + "\n";
        }
    }
    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        credits.SetActive(false);
        tutorialSlideList.ForEach(t => { t.SetActive(false); });
    }

    public void GoToTutorial()
    {
        tutorialSlideList[0].SetActive(true);
        mainMenu.SetActive(false);
    }

    public void NextSlide(int index)
    {
        tutorialSlideList[index - 1].SetActive(false);
        tutorialSlideList[index].SetActive(true);
    }
    public void PreviousSlide(int index)
    {
        tutorialSlideList[index + 1].SetActive(false);
        tutorialSlideList[index].SetActive(true);
    }

    public void GoToDestination(GameObject destination)
    {
        destination.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
