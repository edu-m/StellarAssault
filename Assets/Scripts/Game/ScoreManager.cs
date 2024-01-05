using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //PlayerData playerData;
    float stealthModeMaxTime;
    float stealthModeTimeElapsed = 0f;
    float stealthModeRemainingTime;

    int seconds;
    int minutes;
    int hours;
    [SerializeField] TMP_Text outputTimer;
    [SerializeField] TMP_Text outputNumberOfDeadEnemies;
    [SerializeField] TMP_Text outputHealth;

    private void Awake()
    {
       //playerData=GetComponent<PlayerData>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stealthModeTimeElapsed += Time.deltaTime;

        seconds = (int)stealthModeTimeElapsed % 60;
        minutes = (int)(stealthModeTimeElapsed / 60) % 60;
        hours = (int)(stealthModeTimeElapsed / 3600) % 24;

        outputTimer.text = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);

        if (Move.DirectMode())
        {
            outputTimer.text = "";
            outputHealth.text = System.Convert.ToString(PlayerData.GetHealth());
            outputNumberOfDeadEnemies.text= System.Convert.ToString(EnemyData.GetNumberOfDeadEnemies());

        }
           
    }

    public void StealthModeScore()
    {

    }

    public void DirectModeScore() 
    {

    }
}
