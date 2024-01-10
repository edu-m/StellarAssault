using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    private WaveSpawner waveSpawner;

    private void Start()
    {
        waveSpawner = GetComponentInParent<WaveSpawner>();
    }
}
