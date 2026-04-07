using UnityEngine;
using UnityEngine.UI;

public class ThinkingRate : MonoBehaviour
{
    public float maxGauge = 100f;
	public float drainRate = 20f;
	public float rechargeRate = 10f;
	public float slowScale = 0.25f;

	public Slider gaugeSlider;

	public SlowmoEffect slowMoEffect;
	public SlowMoSound slowMoSound;

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
		currentGauge = 0;
	}

}
