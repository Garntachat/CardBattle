using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner3 : MonoBehaviour // ชื่อ Class ต้องตรงกับชื่อไฟล์
{
    [Header("Enemy Prefabs")]
    public GameObject enemyMeleePrefab; 
    public GameObject enemyWeaponPrefab; 
    public GameObject bossPrefab; 

    [Header("Spawn Points")]
    public Transform[] spawnPoints; 

    [Header("UI")]
    public TMP_Text waveText;

    void Start()
    {
        if (enemyMeleePrefab == null || enemyWeaponPrefab == null || bossPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("ลาก Prefabs ให้ครบ (Stage 3)");
            return;
        }

        StartCoroutine(StartLevel3()); // เปลี่ยนชื่อเรียก Coroutine
    }

    IEnumerator StartLevel3()
    {   
        waveText.text = "FINAL STAGE: Phase 1";
        for (int i = 0; i < 12; i++) // เพิ่มเป็น 12 ตัว
        {
            SpawnEnemy(enemyWeaponPrefab, i);
            yield return new WaitForSeconds(1.0f); // เร็วขึ้นเหลือ 1 วิ
        }
        
        yield return new WaitUntil(() => AllEnemiesDead());
        waveText.text = "Phase 1 Clear!";
        yield return new WaitForSeconds(1.5f); 

        waveText.text = "FINAL STAGE: Phase 2";
        for (int i = 0; i < 20; i++) // ศัตรูถาโถม 20 ตัว
        {
            GameObject selected = (Random.value > 0.5f) ? enemyMeleePrefab : enemyWeaponPrefab;
            SpawnEnemy(selected, Random.Range(0, spawnPoints.Length));
            yield return new WaitForSeconds(0.5f); // เกิดรัวๆ ทุก 0.5 วิ
        }

        yield return new WaitUntil(() => AllEnemiesDead());
        waveText.text = "Phase 2 Clear!";
        yield return new WaitForSeconds(2.0f);
        
        waveText.text = "FINAL BOSS APPEARS!";
        SpawnEnemy(bossPrefab, 0); 
        for (int i = 0; i < 10; i++) // บอสพาลูกน้องมา 10 ตัว
        {
            SpawnEnemy(enemyWeaponPrefab, Random.Range(0, spawnPoints.Length));
        }

        yield return new WaitUntil(() => AllEnemiesDead());
        waveText.text = "ALL LEVELS CLEARED! YOU ARE THE CHAMPION!";
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