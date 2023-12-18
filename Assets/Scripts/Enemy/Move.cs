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
        yield return new WaitForSeconds(2);
        OnTriggerEnter(destinationTarget.GetComponent<Collider>());
        Debug.Log("Arrivato a " + destinationTarget+" torno a "+goBackTarget);
        yield return new WaitForSeconds(2);
        OnTriggerEnter(goBackTarget.GetComponent<Collider>());
        Debug.Log("Arrivo a " + goBackTarget+" torno a "+destinationTarget);
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("SpawnPoint"))
        {
            GoToTarget(destinationTarget);
        }
        else if (other.tag.Equals("checkPoint"))
        {
            GoToTarget(goBackTarget);
        }
    }
  
}
