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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    public void GoToSettings()
    {
        //Time.timeScale = 0f;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void GoToSounds()
    {
        //Time.timeScale = 0f;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        soundsMenu.SetActive(true);
    }

    public void GoToControls()
    {
        //Time.timeScale = 0f;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Back()
    {
        if (settingsMenu.activeInHierarchy==true){        
                pauseMenu.SetActive(true);
                settingsMenu.SetActive(false);
        }
        else if (soundsMenu.activeInHierarchy == true)
            {
               settingsMenu.SetActive(true);
               soundsMenu.SetActive(false);
            }
        else if (controlsMenu.activeInHierarchy == true)
        {
            settingsMenu.SetActive(true);
            controlsMenu.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
