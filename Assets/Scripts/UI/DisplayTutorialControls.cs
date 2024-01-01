using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTutorialControl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sprintKey;
    [SerializeField] TextMeshProUGUI crouchKey;
    [SerializeField] TextMeshProUGUI jumpKey;
    [SerializeField] TextMeshProUGUI interactionKey;

    private void Start()
    {
        sprintKey.text = PlayerInputs.sprintKey.ToString();
        crouchKey.text = PlayerInputs.crouchKey.ToString();
        jumpKey.text = PlayerInputs.jumpKey.ToString();
        interactionKey.text = PlayerInputs.interactKey.ToString();
    }
}
