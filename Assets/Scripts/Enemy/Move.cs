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
        agent.destination = destinationTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        OnTriggerEnter(destinationTarget.GetComponent<Collider>());
        yield return OnTriggerEnter()
    }

    public void GoToTarget(Transform target)
    {
        agent.destination = target.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("checkPoint"))
        {
            GoToTarget(other.transform);
        }
    }
    IEnumerator GoBack()
    {
        yield return OnTriggerEnter();
    }
}
