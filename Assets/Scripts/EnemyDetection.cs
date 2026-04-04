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
		if (enemy == null) return;

		float distance = Vector3.Distance(transform.position, enemy.position);
		bool inRange = distance <= detectionRadius;

		if (inRange && !enemyInRange) {
			enemyInRange = true;
			thinkingRate.OnEnemyEnterRange();
		} else if (!inRange && enemyInRange) {
			enemyInRange = false;
			thinkingRate.OnEnemyExitRange();
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}
}
