using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
	private EnemyFist enemyFist;

	void Start()
	{
		enemyFist = GetComponentInParent<EnemyFist>();
	}

	public void DealDamage()
	{
		if (enemyFist != null) enemyFist.DealDamage();
	}
}
