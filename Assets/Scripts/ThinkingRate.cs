using UnityEngine;
using UnityEngine.UI;

public class ThinkingRate : MonoBehaviour
{
    public float maxGauge = 100f;
	public float drainRate = 20f;
	public float rechargeRate = 10f;
	public float slowScale = 0.25f;

	public Slider thinkingBar;

	public SlowmoEffect slowMoEffect;
	public SlowMoSound slowMoSound;
	public EnemyDetection enemyDetection; 

	private float currentGauge;
	private bool isActive = false;

	public CardManager cardManager; 

	public CardUIManager cardUIManager;

	private bool isWaitingForCard = false;
	void Start() {
		currentGauge = 0f;
	}

	void Update() {
		if (isActive && !isWaitingForCard) 
			{ 
			currentGauge += rechargeRate * Time.unscaledDeltaTime;
			if (currentGauge >= maxGauge) 
				{
					currentGauge = maxGauge;
					isWaitingForCard = true;  // เพิ่ม
					OnGaugeFull();
        		}
    	}

		if (thinkingBar != null)
			thinkingBar.value = currentGauge;

	}

	void Activate() {
		isActive = true;
		Time.timeScale = slowScale;
		if (slowMoEffect != null) slowMoEffect.OnSlowMoStart();
		if (slowMoSound != null) slowMoSound.OnSlowMoStart();
	}

	void Deactivate() {
		isActive = false;
		Time.timeScale = 1f;
		if (slowMoEffect != null) slowMoEffect.OnSlowMoEnd();
		if (slowMoSound != null) slowMoSound.OnSlowMoEnd();
	}

	public void OnEnemyEnterRange() {
		Activate();
	}

	public void OnEnemyExitRange() {
		Deactivate();
		// currentGauge = 0;
		// isWaitingForCard = false;
	}

	void OnGaugeFull()
	{
		Debug.Log("Gauge Full! Drawing cards...");
		cardManager.DrawCards();
		cardUIManager.ShowCards();

	}

	public void OnCardSelected()
	{
		currentGauge = 0f;
		isWaitingForCard = false;

		Time.timeScale = 1f;
		isActive = false;

		if (slowMoEffect != null) slowMoEffect.OnSlowMoEnd();
		if (slowMoSound != null) slowMoSound.OnSlowMoEnd();

		Invoke("RestartSlowMo", 0.1f); // to strat slow moton again

		
	}

	void RestartSlowMo()
	{
		// เช็คว่ามี enemy ใกล้ player มั้ย
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		bool anyInRange = false;
		
		foreach (GameObject e in enemies)
		{
			float dist = Vector3.Distance(transform.position, e.transform.position);
			if (dist <= enemyDetection.detectionRadius)  // ตัวเลขเดียวกับ detectionRadius
			{
				anyInRange = true;
				break;
			}
		}
		Debug.Log($"anyInRange: {anyInRange}");
		if (anyInRange)
			Activate();
	}
}
