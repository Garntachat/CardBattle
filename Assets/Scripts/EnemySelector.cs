using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    public EnemyDetection enemyDetection;
    
    private float currentAngle = 0f;
    void Start()
    {
        enemyDetection.manuallySelected = true;
        SelectEnemyInFront();
    }
    void Update()
    {   
        if (enemyDetection.enemy == null)
            SelectEnemyInFront();

        if (Input.GetKeyDown(KeyCode.E)) 
        { 
            enemyDetection.manuallySelected = true; 
            currentAngle += 90f;
            SnapRotation(currentAngle);
        }
        else if (Input.GetKeyDown(KeyCode.Q)) 
        { 
            enemyDetection.manuallySelected = true; 
            currentAngle -= 90f;
            SnapRotation(currentAngle);
        }
    }

    void SnapRotation(float yAngle)
    {
        transform.rotation = Quaternion.Euler(0f, yAngle, 0f);
        SelectEnemyInFront();
    }
    
    // void SelectEnemyInFront()
    // {
    //     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //     GameObject best = null;
    //     float bestDist = Mathf.Infinity;

    //     foreach (GameObject e in enemies)
    //     {
    //         Vector3 toEnemy = (e.transform.position - transform.position).normalized;
    //         float angle = Vector3.Angle(transform.forward, toEnemy);
    //         float dist = Vector3.Distance(transform.position, e.transform.position);

    //         if (angle < 90f && dist < bestDist)
    //         {
    //             bestDist = dist;
    //             best = e;
    //         }
    //     }

    //     if (best != null)
    //         enemyDetection.enemy = best.transform;
    // }
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

        // Only enemies roughly in the same lane (narrower angle = stricter lane)
        if (angle < 45f && dist < bestDist)
        {
            bestDist = dist;
            best = e;
        }
    }

    if (best != null)
    {
        enemyDetection.enemy = best.transform;
    }
    else
    {
        // 🔴 IMPORTANT: remove target if no enemy in this lane
        enemyDetection.enemy = null;
    }
}
}