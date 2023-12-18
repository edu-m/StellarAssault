using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    public Transform destinationTarget;
    public Transform goBackTarget;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Parto da " + goBackTarget + " verso " + destinationTarget);
        GoToTarget(destinationTarget);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FollowPath());
    }

    public void GoToTarget(Transform target)
    {
        agent.destination = target.position;
    }

    IEnumerator FollowPath()
    {
        yield return new WaitUntil(()=>destinationTarget.GetComponent<BoxCollider>().isTrigger);
        OnTriggerEnter(destinationTarget.GetComponent<Collider>());
        Debug.Log("Primo Arrivato a " + destinationTarget+" torno a "+goBackTarget);
        yield return new WaitUntil(() => goBackTarget.GetComponent<BoxCollider>().isTrigger);
        OnTriggerEnter(goBackTarget.GetComponent<Collider>());
        Debug.Log("Secondo Arrivo a " + goBackTarget+" torno a "+destinationTarget);
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("SpawnPoint"))
        {
            Debug.Log("Vai");
            GoToTarget(destinationTarget);
        }
        else if (other.tag.Equals("checkPoint"))
        {
            Debug.Log("Torna");
            GoToTarget(goBackTarget);
        }
    }
  
}
