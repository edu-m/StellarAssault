using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;
    public Wave[] waves;
    private int currentWaveIndex = 0;
    public Transform spawnPoint;
    public float timeToNextWave;
    public float relaxTime;
    public float waveScore = 0;
    float timeElapsed = 0;
    float timeRemainedToNextWave;
    bool waveFinished;
    bool waveScoreSetted;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //waveFinished = false;
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length; //The number of enemies left of a wave
            //is initially equal to the size of the array of enemies of the wave
        }

        StartCoroutine(SpawnWaveRoutine());
    }

    private void Update()
    {
        if (!waveFinished) //Time elapsed logic
        {
            timeElapsed += Time.deltaTime;
        }
        else
            timeElapsed = 0f;

        Debug.Log("Time elapsed " + timeElapsed);
        if (waves[currentWaveIndex].enemiesLeft <= 0 && !waveScoreSetted) //End level and score logic
        {
            timeRemainedToNextWave = timeToNextWave - timeElapsed;
            waveScore += timeRemainedToNextWave * 1000;
            waveScoreSetted = true;
            Debug.Log("Actual wave score " + waveScore);
            
        }
        /*else
        {
            if(waveFinished) //Wave time is elapsed but there's some enemy alive
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }*/
    }

    public int GetCurrentWaveIndex() => currentWaveIndex;

    private IEnumerator SpawnWaveRoutine()
    {
        while (true)
        {
            waveFinished = false;
            waveScoreSetted = false;

            if (currentWaveIndex < waves.Length)
            {   
                for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
                {
                    ThirdLevelEnemyData enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.position, spawnPoint.rotation);
                    enemy.transform.SetParent(transform); //WaveSpawner object is the parent of the enemy
                    yield return new WaitForSeconds(waves[currentWaveIndex].spawnWaveInterval);
                }
              yield return new WaitForSeconds(timeToNextWave - (waves[currentWaveIndex].spawnWaveInterval * waves[currentWaveIndex].enemies.Length));
                waveFinished = true;
              yield return new WaitForSeconds(relaxTime);
                currentWaveIndex++;
            }
            else
            {
                Debug.Log("You survived to every wave!");
                
                if(waveScore <= 0)
                {
                    waveScore = 1000;
                }
                Debug.Log("Your third level score is " + waveScore);
                SaveToList();
                yield break;
            }
        }
    }

    public void SaveToList()
    {
        ScoreManager.Instance.scores = FileHandler.ReadListFromJSON<Score>("scoreBox");
        ScoreManager.Instance.scores.Add(new Score(PlayerPrefs.GetString("PlayerName"),
        (int)waveScore, PlayerPrefs.GetInt("ActualLevel"),
        -1, Move.seeAndSeekPlayer));

        ScoreManager.Instance.scores.Sort();
        FileHandler.SaveToJSON<Score>(ScoreManager.Instance.scores, "scoreBox");
        Debug.Log("Saved");

    }
}

    [System.Serializable]
    public class Wave
    {
        public ThirdLevelEnemyData[] enemies;
        [HideInInspector] public int enemiesLeft;
        //public float timeToNextWave;
        public float spawnWaveInterval;


    }