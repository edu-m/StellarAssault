using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;
    public Wave[] waves;
    private int currentWaveIndex = 0;
    public Transform spawnPoint;
    private float countDown= 5f;
    private bool readyToCountDown;
    public float timeToNextWave;
    public float relaxTime;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void Start()
    {
        readyToCountDown = true;
        for(int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length; //The number of enemies left of a wave
            //is initially equal to the size of the array of enemies of the wave
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        if(currentWaveIndex >=  waves.Length)
        {
            Debug.Log("You survived to all the waves!");
            return;
        }
        if(readyToCountDown)
            countDown -= Time.deltaTime;

        if(countDown <= 0)
        {
            readyToCountDown = false;
            countDown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWaveRoutine());
        }

        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true; 
            currentWaveIndex++;
        }



    }

    public int GetCurrentWaveIndex() => currentWaveIndex;

    
    private IEnumerator SpawnWaveRoutine()
    {
        SpawnWave();
        yield break;
        
    }

    public void SpawnWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.position, spawnPoint.rotation);
                enemy.transform.SetParent(transform); //WaveSpawner object is the parent of the enemy
            }

        }
    }
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    [HideInInspector] public int enemiesLeft;
    public float timeToNextWave;

    
}