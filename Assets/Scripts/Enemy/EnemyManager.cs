using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    List<GameObject> enemyList;
    [SerializeField] int enemyListCount;
    [SerializeField] List<Transform> spawnPointList;
    //[SerializeField] int spawnPointCount;
    [SerializeField] float timeToSpawn;
    // Start is called before the first frame update
    public void Start()
    {
        enemyList = new List<GameObject>(enemyListCount);
        //spawnPointList = new List<Transform>(spawnPointCount);

        for (int i = 0; i < enemyListCount; i++)
        {
            Transform randomSpawnPoint = spawnPointList[Random.Range(0, spawnPointList.Count)];
            GameObject tempEnemy = Instantiate(enemy, randomSpawnPoint.position, randomSpawnPoint.rotation);
            enemyList.Add(tempEnemy);
            enemyList[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemies()
    {
        if(enemyList.Count > 0)
            for (int i = 0; i < enemyListCount; i++)
                enemyList[i].SetActive(true);
        yield return new WaitForSeconds(timeToSpawn);
    }


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnEnemies());
    }
}
