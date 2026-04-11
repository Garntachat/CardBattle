using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
	public float detectionRadius = 5f;
	public Transform enemy;

	public ThinkingRate thinkingRate;
	private bool enemyInRange = false;

	void Start() {
	}

	void Update() {
    	Debug.Log("TimeScale: " + Time.timeScale + " EnemyInRange: " + enemyInRange);

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

        if (closest != null)
            enemy = closest.transform;

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

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}
}
