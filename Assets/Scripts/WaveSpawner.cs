using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject enemyMeleePrefab; 
    public GameObject enemyWeaponPrefab; 
    public GameObject bossPrefab; 

    [Header("Spawn Points")]
    public Transform[] spawnPoints; 

    void Start()
    {
        if (enemyMeleePrefab == null || enemyWeaponPrefab == null || bossPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("ลาก Prefabsให้ครบ");
            return;
        }

        StartCoroutine(StartLevel1());
    }

    IEnumerator StartLevel1()
    {
        Debug.Log("Phase 1: Start!");
        for (int i = 0; i < 4; i++)
        {
            SpawnEnemy(enemyMeleePrefab, i);
            yield return new WaitForSeconds(2.0f);
        }
        
        yield return new WaitUntil(() => AllEnemiesDead());
        Debug.Log("Phase 1 Clear!");
        yield return new WaitForSeconds(2.0f); 

        Debug.Log("Phase 2: Start!");
        for (int i = 0; i < 8; i++)
        {
            GameObject selected = (i % 2 == 0) ? enemyMeleePrefab : enemyWeaponPrefab;
            SpawnEnemy(selected, Random.Range(0, spawnPoints.Length));
            yield return new WaitForSeconds(1.2f);
        }

        yield return new WaitUntil(() => AllEnemiesDead());
        Debug.Log("Phase 2 Clear!");
        yield return new WaitForSeconds(2.0f);

        Debug.Log("Phase 3: BOSS APPEARS!");
        
        SpawnEnemy(bossPrefab, 0); 
        
        for (int i = 0; i < 4; i++)
        {
            SpawnEnemy(enemyMeleePrefab, i);
        }

        yield return new WaitUntil(() => AllEnemiesDead());
        Debug.Log("Level 1 Completed! BOSS DEFEATED!");
    }

    void SpawnEnemy(GameObject prefab, int spawnPointIndex)
    {
        if (prefab == null) return;
        int index = spawnPointIndex % spawnPoints.Length;
        Instantiate(prefab, spawnPoints[index].position, Quaternion.identity);
    }

    bool AllEnemiesDead()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }
}