using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //PlayerData playerData;
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ScoreManager>();
            if (_instance == null)
                Debug.LogError("No GameManager in scene");
            return _instance;
        }
    }
    public GameObject scoreBox;
    const int stealthModeMaxTime = 2400;
    float stealthModeTimeElapsed = 0;
    int stealthModeRemainingTime;
    const int stealthModeMultiplyer = 1000;
    const int directModeEnemiesMultiplyer = 2000;
    const int directModePlayerHealthMultiplyer = 1000;
    int globalActualScore;

    int seconds;
    int minutes;
    int hours;
    [SerializeField] TMP_Text outputTimer;
    [SerializeField] TMP_Text outputActualScore;

    public List<Score> scores;

    private void Awake()
    {
        scores=new List<Score>();

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        stealthModeTimeElapsed += Time.deltaTime;

        seconds = (int)stealthModeTimeElapsed % 60;
        minutes = (int)(stealthModeTimeElapsed / 60) % 60;
        hours = (int)(stealthModeTimeElapsed / 3600) % 24;

        outputTimer.text = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);

        if (!Move.DirectMode() && !Move.seeAndSeekPlayer)
        {
            StealthModeScore();
        }
        else
        {
            outputTimer.text = "";
            DirectModeScore();
        }
    }

    public int GetGlobalActualScore()
    {
        return globalActualScore;
    }
    public void StealthModeScore()
    {
        stealthModeRemainingTime = stealthModeMaxTime - seconds;
        if (stealthModeRemainingTime > 0)
            globalActualScore = stealthModeRemainingTime * stealthModeMultiplyer;
        else
            globalActualScore = 3000;

        outputActualScore.text = System.Convert.ToString(globalActualScore);
    }

    public void DirectModeScore() 
    {
        globalActualScore = EnemyData.GetNumberOfDeadEnemies() * directModeEnemiesMultiplyer + PlayerData.Instance.GetHealth() * directModePlayerHealthMultiplyer;
        //Debug.Log("Direct Mode Score " + globalActualScore);
        outputActualScore.text = System.Convert.ToString(globalActualScore);
    }
}
