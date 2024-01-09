using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour, IInteractable
{
    [SerializeField] bool shouldHaveObject;
    public string GetDescription()
    {
        return "Interact";
    }
    public void Interact()
    {
        GameManager.Instance.NextLevel();
        if (PlayerData.HasObject() == shouldHaveObject)
            InteractWithConstraint();
    }

    public void InteractWithConstraint()
    {
        SaveToList();
        GameManager.Instance.NextLevel();
    }
    public void SaveToList()
    {
        ScoreManager.Instance.scores = FileHandler.ReadListFromJSON<Score>("scoreBox");
        ScoreManager.Instance.scores.Add(new Score(PlayerPrefs.GetString("PlayerName"), 
        ScoreManager.Instance.GetGlobalActualScore(), PlayerPrefs.GetInt("ActualLevel"), 
        PlayerPrefs.GetInt("Difficulty"), Move.seeAndSeekPlayer));

        ScoreManager.Instance.scores.Sort();
        FileHandler.SaveToJSON<Score>(ScoreManager.Instance.scores, "scoreBox");
        Debug.Log("Saved");

    }

    public void RemoveList()
    {   //You should clear the json file to do something useful
        ScoreManager.Instance.scores.Clear();
        Debug.Log("List removed");
    }
}
