using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    List<GameObject> enemyList;
    [SerializeField] int enemyListCount;
    [SerializeField] List<Transform> spawnPointList;
    [SerializeField] int spawnPointCount;
    [SerializeField] float timeToSpawn;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyList = new List<GameObject>();
        spawnPointList = new List<Transform>();


        for (int i=0;i<enemyListCount;i++)
        {
            enemyList[i] = Instantiate(enemy, spawnPointList[Random.Range(0,spawnPointCount)].position, spawnPointList[Random.Range(0,spawnPointCount)].rotation);
            enemyList[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemies()
    {   
        for (int i=0; i<enemyListCount;i++) 
        {
            enemyList[i].SetActive(true);
        }
        yield return new WaitForSeconds(timeToSpawn);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnEnemies());
    }
}
