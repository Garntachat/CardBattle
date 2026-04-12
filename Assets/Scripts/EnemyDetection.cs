using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRadius = 5f;
    public Transform enemy;
    public ThinkingRate thinkingRate;
    public bool enemyInRange = false;
    public bool manuallySelected = false;
    private Transform lastEnemy = null;

    void Update() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDist = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = e;
            }
        }

        if (!manuallySelected || enemy == null)
        {
            if (closest != null)
            {
                enemy = closest.transform;
            }
        }

        bool inRange = closestDist <= detectionRadius;

        if (inRange && !enemyInRange)
        {
            enemyInRange = true;
            thinkingRate.OnEnemyEnterRange();
        }
        else if (!inRange && enemyInRange)
        {
            enemyInRange = false;
            thinkingRate.OnEnemyExitRange();
        }
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

