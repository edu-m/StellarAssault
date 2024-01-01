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
    }
    // Start is called before the first frame update
    void Start()
    {
        BackToMenu();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
