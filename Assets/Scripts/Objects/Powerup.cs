using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Powerup : MonoBehaviour, IInteractable
{
    [SerializeField] protected int boostDuration;
    [SerializeField] protected string imageName;
    protected GameObject image;
    public string GetDescription() => "Use";
    protected void Awake() => image = GameObject.Find(imageName);
    protected abstract void PowerupOperationBeforeTimerWait();

    /* PowerupOperationAfterTimerWait is virtual with an empty implementation.
     * The reason is that some concrete classes might not need to do anything
     * after waiting (i.e. Heal). This means that the wait will only influence how 
     * long the Powerup icon will stay active on screen.
     */
    protected virtual void PowerupOperationAfterTimerWait() { }
    private void BeforeTimerWait()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false; // only disables visually, to avoid abrupt stop of coroutine
        gameObject.GetComponent<SphereCollider>().enabled = false;
        image.GetComponent<RawImage>().enabled = true;
        PowerupOperationBeforeTimerWait();
    }
    private void AfterTimerWait()
    {
        PowerupOperationAfterTimerWait();
        image.GetComponent<RawImage>().enabled = false;
        Destroy(gameObject); // actually calls deconstructor here, after all operation have terminated
    }
    private IEnumerator ExecutePowerup(int seconds)
    {
        BeforeTimerWait();
        yield return new WaitForSeconds(seconds);
        AfterTimerWait();
    }
    public void Interact() => StartCoroutine(ExecutePowerup(boostDuration));
}
