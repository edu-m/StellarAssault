using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score : IComparable<Score>
{
    public string playerName;
    public int playerScore;
    public int playerLevel;
    public int playerDifficulty;
    public bool playerScoreMode;

    public Score(string playerName, int playerScore, int playerLevel, int playerDifficulty, bool playerScoreMode)
    {
        this.playerName = playerName;
        this.playerScore = playerScore;
        this.playerLevel = playerLevel;
        this.playerDifficulty = playerDifficulty;
        this.playerScoreMode = playerScoreMode;
    }

    public int CompareTo(Score other)
    {
        if(other==null)
            return -1;
        return other.playerScore - playerScore;
    }

}
