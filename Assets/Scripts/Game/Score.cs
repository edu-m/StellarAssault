using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score
{
    public string playerName;
    public int playerScore;
    public int playerLevel;
    public bool playerScoreMode;

    public Score(string playerName, int playerScore, int playerLevel, bool playerScoreMode)
    {
        this.playerName = playerName;
        this.playerScore = playerScore;
        this.playerLevel = playerLevel;
        this.playerScoreMode = playerScoreMode;
    }

}
