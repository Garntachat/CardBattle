using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    public EnemyDetection enemyDetection;
    
    void Start()
    {
        enemyDetection.manuallySelected = true;
        SelectEnemyInFront();
    }
    void Update()
    {   
        if (enemyDetection.enemy == null)
            SelectEnemyInFront();
        if (Input.GetKeyDown(KeyCode.W)) { enemyDetection.manuallySelected = true; SnapRotation(0f); }
        else if (Input.GetKeyDown(KeyCode.D)) { enemyDetection.manuallySelected = true; SnapRotation(90f); }
        else if (Input.GetKeyDown(KeyCode.S)) { enemyDetection.manuallySelected = true; SnapRotation(180f); }
        else if (Input.GetKeyDown(KeyCode.A)) { enemyDetection.manuallySelected = true; SnapRotation(270f); }
    }

    void SnapRotation(float yAngle)
    {
        transform.rotation = Quaternion.Euler(0f, yAngle, 0f);
        SelectEnemyInFront();
    }

    void SelectEnemyInFront()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject best = null;
        float bestDist = Mathf.Infinity;

        foreach (GameObject e in enemies)
        {
            Vector3 toEnemy = (e.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, toEnemy);
            float dist = Vector3.Distance(transform.position, e.transform.position);

            if (angle < 90f && dist < bestDist)
            {
                bestDist = dist;
                best = e;
            }
        }

        if (best != null)
            enemyDetection.enemy = best.transform;
    }
}