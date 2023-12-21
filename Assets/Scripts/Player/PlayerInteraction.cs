using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public Camera mainCam;
    public float interactionDistance = 4f;
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI keyCodeText;
    public KeyCode interactKey = KeyCode.E;

    private void Update()
    {
        InteractionRay();
    }
    private void InteractionRay()
    {
        /* https://docs.unity3d.com/ScriptReference/Camera.ViewportPointToRay.html
         * In Unity, a "viewport" typically refers to the portion of the game world that is currently visible to the camera.
         * The camera's viewport defines the area of the game world that will be rendered to the screen. 
         * It is essentially the region in 3D space that the camera "sees" and captures to display on the screen.
         */
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
        RaycastHit hit;
        bool hitSomething = false;
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
        {
            interactionUI.SetActive(true);
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable)) 
            {
                hitSomething = true;
                keyCodeText.text = interactKey.ToString();
                interactionText.text = interactable.GetDescription();
                if (Input.GetKeyDown(interactKey))
                    interactable.Interact();
            }
                interactionUI.SetActive(hitSomething);
        }
    }
}
