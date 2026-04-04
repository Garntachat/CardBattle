using UnityEngine;
using UnityEngine.UI;

public class ThinkingRate : MonoBehaviour
{
    public float maxGauge = 100f;
	public float drainRate = 20f;
	public float rechargeRate = 10f;
	public float slowScale = 0.25f;

	public Slider gaugeSlider;

	private float currentGauge;
	private bool isActive = false;

	void Start() {
		currentGauge = maxGauge;
	}

	void Update() {
		if (isActive) {
			currentGauge += rechargeRate * Time.unscaledDeltaTime;
			if (currentGauge >= maxGauge) {
				currentGauge = 0f;
			}
		}

		if (gaugeSlider != null)
			gaugeSlider.value = currentGauge;

		Debug.Log("Gauge: " + currentGauge.ToString("F1"));
	}

	void Activate() {
		isActive = true;
		Time.timeScale = slowScale;
	}

	void Deactivate() {
		isActive = false;
		Time.timeScale = 1f;
	}

	public void OnEnemyEnterRange() {
		Activate();
	}

	public void OnEnemyExitRange() {
		Deactivate();
		currentGauge = 0;
	}

}
