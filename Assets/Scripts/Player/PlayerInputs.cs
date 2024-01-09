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
    public static KeyCode interactKey;

    [SerializeField] TMP_Text outputJumpKey;
    [SerializeField] TMP_Text outputSprintKey;
    [SerializeField] TMP_Text outputCrouchKey;
    [SerializeField] TMP_Text outputRollKey;
    [SerializeField] TMP_Text outputInteractKey;

    // Start is called before the first frame update

    private static PlayerInputs _instance;
    public static PlayerInputs Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerInputs>();
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (FirstTime())
        {
            ResetDefaultControls();
        }
        else
            SetControls();

    }

    public void ResetDefaultControls()
    {
     PlayerPrefs.SetString("Jump", "Space");
     PlayerPrefs.SetString("Sprint", "LeftShift");
     PlayerPrefs.SetString("Crouch", "LeftControl");
     PlayerPrefs.SetString("Roll", "LeftAlt");
     PlayerPrefs.SetString("Interact", "E");

        SetControls();

    }

    public bool FirstTime()
    {
        if (!PlayerPrefs.HasKey("Jump"))
        {
            return true;
        }
        return false;
    }
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
       interactKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact"));
       outputJumpKey.text= jumpKey.ToString();
       outputSprintKey.text = sprintKey.ToString();
       outputCrouchKey.text = crouchKey.ToString();
       //outputRollKey.text = rollKey.ToString();
       outputInteractKey.text = interactKey.ToString();
    }
}
