using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{   

    public static KeyCode jumpKey;
    public static KeyCode sprintKey;
    public static KeyCode crouchKey;
    public static KeyCode rollKey;

    [SerializeField] TMP_InputField inputJumpKey;
    [SerializeField] TMP_InputField inputSprintKey;
    [SerializeField] TMP_InputField inputCrouchKey;
    [SerializeField] TMP_InputField inputRollKey;

    // Start is called before the first frame update

    void Start()
    {
        SaveControls();

    }

    public void LoadControlsSettings()
    {
     jumpKey = KeyCode.Space;
     sprintKey = KeyCode.LeftShift;
     crouchKey = KeyCode.LeftControl;
     rollKey = KeyCode.LeftAlt;

    }

    public void ResetDefaultControls()
    {
        LoadControlsSettings();
        Debug.Log(jumpKey.ToString() + " " + sprintKey.ToString() + " " + crouchKey.ToString() + " " + rollKey.ToString());
    }
    public void SaveControls()
    {
        PlayerPrefs.SetString("jumpKey", inputJumpKey.text.ToString());
        PlayerPrefs.SetString("sprintKey", inputSprintKey.text.ToString());
        PlayerPrefs.SetString("crouchKey", inputCrouchKey.text.ToString());
        PlayerPrefs.SetString("rollKey", inputRollKey.text.ToString());


        if (inputJumpKey.text.ToString().Equals(""))
        {
            
            PlayerPrefs.SetString("jumpKey", "Space");
        }

        if (inputSprintKey.text.ToString().Equals(""))
        {
            PlayerPrefs.SetString("sprintKey", "LeftShift");
        }

        if (inputCrouchKey.text.ToString().Equals(""))
        {
            PlayerPrefs.SetString("crouchKey", "LeftControl");
        }

        if (inputRollKey.text.ToString().Equals(""))
        {
            PlayerPrefs.SetString("rollKey", "LeftAlt");
        }

        SetControls();
    }

    public void SetControls()
    {
      
       jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey"));
       sprintKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("sprintKey"));
       crouchKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouchKey"));
       rollKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rollKey"));
        Debug.Log(jumpKey.ToString() + " " + sprintKey.ToString() + " " + crouchKey.ToString() + " " + rollKey.ToString());
    }


}
