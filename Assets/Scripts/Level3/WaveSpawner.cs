using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;
    public Wave[] waves;
    private int currentWaveIndex = 0;
    public Transform spawnPoint;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void Start()
    {
        for(int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;

        }
    }

    


    // Update is called once per frame
    void Update()
    {
       
    }

    public void SpawnWave()
    {
        for(int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
        {
            Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i],spawnPoint.position,spawnPoint.rotation);
            enemy.transform.SetParent(transform); //WaveSpawner object is the parent of the enemy

        }
    }
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float timeToNextWave;
    [HideInInspector] public int enemiesLeft;

    
}