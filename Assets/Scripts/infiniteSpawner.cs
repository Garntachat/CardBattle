using UnityEngine;
using System.Collections;
using TMPro;

public class infiniteSpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject enemyMeleePrefab; 
    public GameObject enemyWeaponPrefab; 
    public GameObject bossPrefab; 

    [Header("Spawn Points")]
    public Transform[] spawnPoints; 

    public float spawnRate = 0.2f;
    [Header("UI")]
    public TMP_Text waveText; 

    void Start()
    {
        if (enemyMeleePrefab == null || enemyWeaponPrefab == null || bossPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("ลาก Prefabsให้ครบ");
            return;
        }

        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        int wave = 1;
        
        while (true)
        {   
            waveText.text = "wave: "+ wave.ToString();
            for (int i = 0; i < wave*2; i++)
            {
            GameObject selected = (i % 2 == 0) ? enemyMeleePrefab : enemyWeaponPrefab;
            SpawnEnemy(selected, Random.Range(0, spawnPoints.Length));
            yield return new WaitForSeconds(spawnRate);
            }
            if (wave % 4 == 0)
        {
            SpawnEnemy(bossPrefab, Random.Range(0, spawnPoints.Length));
        }
            yield return new WaitUntil(() => AllEnemiesDead());
            wave +=1;
            
        }
    }

    void SpawnEnemy(GameObject prefab, int spawnPointIndex)
    {
        if (prefab == null) return;
        int index = spawnPointIndex % spawnPoints.Length;
        Vector3 spawnPos = spawnPoints[index].position;
        if (prefab == enemyMeleePrefab)
        {
            spawnPos.y = 0.9f;
        }
        else
        {
            spawnPos.y = 0.0f;
        }
        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);
        enemy.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    bool AllEnemiesDead()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }
}