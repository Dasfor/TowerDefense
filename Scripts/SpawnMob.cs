using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMob : MonoBehaviour
{
    LevelManagerScr LMS;
    GameController gcs;
    public GameObject enemyPref, bossPref, cloudPref1, cloudPref2;
    public float TimeToSpawn = 4;
    public float TimeToSpawnCloud = 0;
    int spawnCount = 0;

    void Start()
    {
        gcs = FindObjectOfType<GameController>();
        LMS = FindObjectOfType<LevelManagerScr>();
    }

    void Update()
    {
        if (TimeToSpawnCloud <= 0)
        {
            StartCoroutine(SpawnCloud());
            TimeToSpawnCloud = 12f;
        }
        if (TimeToSpawn <= 0 && spawnCount <= 8)
        {
            StartCoroutine(SpawnEnemy(spawnCount + 1));
            TimeToSpawn = Random.Range(4f, 8f);
        }
        if (TimeToSpawn <= 0 && spawnCount > 8)
        {
            StartCoroutine(SpawnBoss(spawnCount = 1));
            TimeToSpawn = Random.Range(20f, 25f);
        }
        else
        {
            TimeToSpawn -= Time.deltaTime;
            TimeToSpawnCloud -= Time.deltaTime;
        }
    }

    IEnumerator SpawnEnemy(int enemyCount)
    {
        spawnCount++;
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject tmpEnemy = Instantiate(enemyPref);
            tmpEnemy.transform.SetParent(gameObject.transform, false);
            tmpEnemy.GetComponent<Enemy>().selfEnemy = gcs.AllEnemies[Random.Range(0, gcs.AllEnemies.Count)];
            Transform startCellPos = LMS.wayPoints[0].transform;
            Vector3 startPos = new Vector3(startCellPos.position.x + startCellPos.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                           startCellPos.position.y - startCellPos.GetComponent<SpriteRenderer>().bounds.size.y / 2);
            tmpEnemy.transform.position = startPos;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpawnBoss(int enemyCount)
    {
        GameObject tmpBoss = Instantiate(bossPref);
        tmpBoss.transform.SetParent(gameObject.transform, false);
        tmpBoss.GetComponent<Enemy>().selfEnemy = gcs.AllBoss[0];
        Transform startCellPos = LMS.wayPoints[0].transform;
        Vector3 startPos = new Vector3(startCellPos.position.x + startCellPos.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                       startCellPos.position.y - startCellPos.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        tmpBoss.transform.position = startPos;
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator SpawnCloud()
    {
        var pos = transform.position;
        pos = new Vector2(10.5f, Random.Range(-4f, 2.5f));
        Instantiate(cloudPref1, pos, cloudPref1.transform.rotation);
        var pos1 = transform.position;
        pos1 = new Vector2(11f, Random.Range(-4f, 2.5f));
        Instantiate(cloudPref2, pos1, cloudPref2.transform.rotation);
        yield return new WaitForSeconds(0.5f);
    }
}
