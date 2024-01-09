using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();
            if (_instance == null)
                Debug.LogError("No GameManager in scene");
            return _instance;
        }
    }
    public GameObject crosshairCanvas;
    public GameObject pauseMenu;
    public static bool isPaused;
    public GameObject settingsMenu;
    public GameObject cameraHolder;
    public GameObject soundsMenu;
    public GameObject controlsMenu;

    private void Awake()
    {
        if (_instance==null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        soundsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        cameraHolder.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        } 
    }
    public void NextLevel()
    {
        if (PlayerPrefs.GetInt("ActualLevel") == 3)
        {
            PlayerPrefs.SetInt("GameCompleted", 1);
            PlayerPrefs.SetInt("ActualLevel", 1);
            SceneManager.LoadScene(0);
            return;
        }
        PlayerPrefs.SetInt("ActualLevel", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("ActualLevel"));
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None; // buttons don't work without this
        crosshairCanvas.SetActive(false);
        cameraHolder.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        crosshairCanvas.SetActive(true);
        cameraHolder.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
