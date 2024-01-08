using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour, IInteractable
{
    public string GetDescription() 
    {
        return "Interact";
    }

    public void Interact()
    {   
        ScoreManager.Instance.scores.Add(new Score(PlayerPrefs.GetString("PlayerName"),ScoreManager.Instance.GetGlobalActualScore(), PlayerPrefs.GetInt("ActualLevel"),Move.seeAndSeekPlayer));
        FileHandler.SaveToJSON<Score>(ScoreManager.Instance.scores, "scoreBox");
        GameManager.Instance.PlayGame();
    }
}
