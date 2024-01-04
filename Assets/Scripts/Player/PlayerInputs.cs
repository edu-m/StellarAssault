using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputs : MonoBehaviour
{   

    public static KeyCode jumpKey;
    public static KeyCode sprintKey;
    public static KeyCode crouchKey;
    public static KeyCode rollKey;

    [SerializeField] TMP_Text outputJumpKey;
    [SerializeField] TMP_Text outputSprintKey;
    [SerializeField] TMP_Text outputCrouchKey;
    [SerializeField] TMP_Text outputRollKey;

    // Start is called before the first frame update

    void Start()
    {
        if (FirstTime())
            ResetDefaultControls();
        else
            SetControls();
    }

    public void ResetDefaultControls()
    {
     PlayerPrefs.SetString("Jump", "Space");
     PlayerPrefs.SetString("Sprint", "LeftShift");
     PlayerPrefs.SetString("Crouch", "LeftControl");
     PlayerPrefs.SetString("Roll", "LeftAlt");

        SetControls();

    }

    public bool FirstTime() => !PlayerPrefs.HasKey("Jump");
    public void SaveControl(Button control)
    {
        PlayerPrefs.SetString(control.transform.parent.name.ToString(), control.tag.ToString());
        SetControls();
    }

    public void SetControls()
    {
      
       jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump"));
       sprintKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint"));
       crouchKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch"));
       rollKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Roll"));

       outputJumpKey.text= jumpKey.ToString();
       outputSprintKey.text = sprintKey.ToString();
       outputCrouchKey.text = crouchKey.ToString();
       outputRollKey.text = rollKey.ToString();
        
    }


}
