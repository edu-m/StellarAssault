using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        agent.destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
